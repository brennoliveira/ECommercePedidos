using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommercePedidos.Domain.Entities;

namespace ECommercePedidos.Application.UseCases.ObterPedidoPorId
{
    public interface IObterPedidoPorIdUseCase
    {
        Task<Pedido?> ExecutarAsync(Guid id);
    }
}
