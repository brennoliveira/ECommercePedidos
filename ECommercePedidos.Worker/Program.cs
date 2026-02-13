using ECommercePedidos.Application.UseCases.ProcessarPedido;
using ECommercePedidos.Domain.Interfaces.Repositories;
using ECommercePedidos.Infrastructure;
using ECommercePedidos.Infrastructure.Data;
using ECommercePedidos.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();

builder.Services.AddScoped<IProcessarPedidoUseCase, ProcessarPedidoUseCase>();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
