using ECommercePedidos.Domain.Entities;
using ECommercePedidos.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommercePedidos.Domain.Interfaces.Repositories
{
    public interface IPedidoRepository
    {
        Task AdicionarPedidoAsync(Pedido pedido);
        Task<Pedido?> ObterPedidoPorIdAsync(Guid id);
        Task<IEnumerable<Pedido>> ObterTodosPedidosAsync(int page, int pageSize, PedidoStatus? status);
        Task AtualizarPedidoAsync(Pedido pedido);
        Task ExcluirPedidoAsync(Guid id);
    }
}
