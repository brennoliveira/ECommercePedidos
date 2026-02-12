using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommercePedidos.Domain.Interfaces.Repositories;

namespace ECommercePedidos.Application.UseCases.ObterTodosPedidos
{
    public class ObterTodosPedidosUseCase(IPedidoRepository pedidoRepository) : IObterTodosPedidosUseCase
    {
        private readonly IPedidoRepository _pedidoRepository = pedidoRepository;

        public async Task<IEnumerable<Domain.Entities.Pedido>> ExecutarAsync(int page, int pageSize, Domain.Enums.PedidoStatus? status)
        {
            if (page <= 0)
                page = 1;

            if (pageSize <= 0 || pageSize > 100)
                pageSize = 10;

            return await _pedidoRepository.ObterTodosPedidosAsync(page, pageSize, status);
        }
    }
}
