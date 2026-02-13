using ECommercePedidos.Application.UseCases.ProcessarPedido;
using ECommercePedidos.Worker.DTOs;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

public class Worker(IServiceScopeFactory scopeFactory) : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
    private IConnection? _connection;
    private IChannel? _channel;

    private const string QueueName = "pedido-criado";

    private async Task EnsureConnectionAsync()
    {
        if (_connection is { IsOpen: true } && _channel is not null)
            return;

        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest"
        };

        _connection = await factory.CreateConnectionAsync();
        _channel = await _connection.CreateChannelAsync();

        await _channel.QueueDeclareAsync(
            queue: QueueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await EnsureConnectionAsync();

        await _channel!.QueueDeclareAsync("pedido-criado", false, false, false, null);

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            
            var json = Encoding.UTF8.GetString(body);

            var evento = JsonSerializer.Deserialize<PedidoCriadoEvent>(json);

            if (evento == null)
                return;

            var pedidoId = evento.PedidoId;

            using var scope = _scopeFactory.CreateScope();

            var processarPedidoUseCase =
                scope.ServiceProvider.GetRequiredService<IProcessarPedidoUseCase>();

            await processarPedidoUseCase.ExecutarAsync(pedidoId);

            await _channel.BasicAckAsync(ea.DeliveryTag, false);
        };

        await _channel.BasicConsumeAsync("pedido-criado", false, consumer);

        await Task.CompletedTask;
    }
}
