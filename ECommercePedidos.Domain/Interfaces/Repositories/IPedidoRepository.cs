using ECommercePedidos.Domain.Entities;
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
        Task<Pedido> ObterPedidoPorIdAsync(Guid id);
        Task<IEnumerable<Pedido>> ObterTodosPedidosAsync();
        Task AtualizarPedidoAsync(Pedido pedido);
        Task ExcluirPedidoAsync(Guid id);
    }
}
