using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommercePedidos.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Metadata;
using RabbitMQ.Client;

namespace ECommercePedidos.Infrastructure.Messaging
{
    public class MessageBus : IMessageBus, IAsyncDisposable
    {
        private IConnection? _connection;
        private IChannel? _channel;

        private const string QueueName = "pedido-criado";

        private async Task EnsureConnectionAsync()
        {
            if (_connection == null)
                return;

            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };

            _connection = await factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();

            await _channel.QueueDeclareAsync(
                queue: QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        public async Task PublicarPedidoCriadoAsync(Guid pedidoId)
        {
            await EnsureConnectionAsync();

            var message = new
            {
                PedidoId = pedidoId,
                DataCriacao = DateTime.UtcNow
            };

            var body = Encoding.UTF8.GetBytes(System.Text.Json.JsonSerializer.Serialize(message));

            await _channel!.BasicPublishAsync(
                exchange: string.Empty,
                routingKey: QueueName,
                body: body);
        }

        public async ValueTask DisposeAsync()
        {
            if (_channel != null)
                await _channel.DisposeAsync();
            if (_connection != null)
                await _connection.DisposeAsync();
        }
    }
}
