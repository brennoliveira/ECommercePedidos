using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommercePedidos.Domain.Entities;
using ECommercePedidos.Domain.Interfaces.Repositories;

namespace ECommercePedidos.Application.UseCases.ObterPedidoPorId
{
    public class ObterPedidoUseCase : IObterPedidoPorIdUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;

        public ObterPedidoUseCase(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<Pedido?> ExecutarAsync(Guid id)
        {
            return await _pedidoRepository.ObterPedidoPorIdAsync(id);
        }
    }
}
