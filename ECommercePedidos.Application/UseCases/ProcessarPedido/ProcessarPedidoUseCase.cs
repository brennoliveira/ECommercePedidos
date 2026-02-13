using ECommercePedidos.Domain.Enums;
using ECommercePedidos.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommercePedidos.Application.UseCases.ProcessarPedido
{
    public class ProcessarPedidoUseCase(IPedidoRepository pedidoRepository) : IProcessarPedidoUseCase
    {
        private readonly IPedidoRepository _pedidoRepository = pedidoRepository;

        public async Task ExecutarAsync(Guid pedidoId)
        {
            var pedido = await _pedidoRepository.ObterPedidoPorIdAsync(pedidoId);
            if (pedido == null)
            {
                return;
            }
         
            await _pedidoRepository.AtualizarPedidoAsync(pedido);

            await Task.Delay(2000);

            pedido.MarcarComoProcessado();
            await _pedidoRepository.AtualizarPedidoAsync(pedido);
        }
    }
}
