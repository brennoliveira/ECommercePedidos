using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommercePedidos.Domain.Interfaces.Repositories;

namespace ECommercePedidos.Application.UseCases.DeletarPedido
{
    public class DeletarPedidoUseCase(IPedidoRepository pedidoRepository) : IDeletarPedidoUseCase
    {
        private readonly IPedidoRepository _pedidoRepository = pedidoRepository;

        public async Task<bool> ExecutarAsync(Guid id)
        {
            var pedido = await _pedidoRepository.ObterPedidoPorIdAsync(id);

            if (pedido == null)
                return false;

            pedido.Cancelar();

            await _pedidoRepository.AtualizarPedidoAsync(pedido);

            return true;
        }
    }
}
