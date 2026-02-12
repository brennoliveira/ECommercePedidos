using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommercePedidos.Domain.Entities;
using ECommercePedidos.Domain.Enums;

namespace ECommercePedidos.Application.UseCases.ObterTodosPedidos
{
    public interface IObterTodosPedidosUseCase
    {
        Task<IEnumerable<Pedido>> ExecutarAsync(int page, int pageSize, PedidoStatus? status);
    }
}
