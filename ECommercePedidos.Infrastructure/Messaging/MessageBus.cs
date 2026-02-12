using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommercePedidos.Domain.Interfaces.Repositories;

namespace ECommercePedidos.Infrastructure.Messaging
{
    public class MessageBus : IMessageBus
    {
        public Task PublicarPedidoCriadoAsync(Guid pedidoId)
        {
            Console.WriteLine($"Pedido publicado: {pedidoId}");
            return Task.CompletedTask;
        }
    }
}
