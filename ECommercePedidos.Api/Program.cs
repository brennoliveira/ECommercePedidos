using ECommercePedidos.Application.UseCases.AtualizarPedido;
using ECommercePedidos.Application.UseCases.CriarPedido;
using ECommercePedidos.Application.UseCases.DeletarPedido;
using ECommercePedidos.Application.UseCases.ObterPedidoPorId;
using ECommercePedidos.Application.UseCases.ObterTodosPedidos;
using ECommercePedidos.Domain.Interfaces.Messaging;
using ECommercePedidos.Domain.Interfaces.Repositories;
using ECommercePedidos.Infrastructure.Data;
using ECommercePedidos.Infrastructure.Messaging;
using ECommercePedidos.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"))
);
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();

builder.Services.AddScoped<ICriarPedidoUseCase, CriarPedidoUseCase>();
builder.Services.AddScoped<IObterPedidoPorIdUseCase, ObterPedidoUseCase>();
builder.Services.AddScoped<IObterTodosPedidosUseCase, ObterTodosPedidosUseCase>();
builder.Services.AddScoped<IAtualizarPedidoUseCase, AtualizarPedidoUseCase>();
builder.Services.AddScoped<IDeletarPedidoUseCase, DeletarPedidoUseCase>();

builder.Services.AddScoped<IMessageBus, MessageBus>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
