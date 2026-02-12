using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommercePedidos.Domain.Enums;
using ECommercePedidos.Domain.Interfaces.Repositories;

namespace ECommercePedidos.Application.UseCases.AtualizarPedido
{
    public class AtualizarPedidoUseCase(IPedidoRepository pedidoRepository) : IAtualizarPedidoUseCase
    {
        private readonly IPedidoRepository _pedidoRepository = pedidoRepository;

        public async Task<bool> ExecutarAsync(Guid id, AtualizarPedidoInput input)
        {
            var pedido = await _pedidoRepository.ObterPedidoPorIdAsync(id);

            if (pedido == null || !pedido.Ativo)
                return false;

            if (pedido.Status == PedidoStatus.Processado)
                throw new InvalidOperationException("Pedido não pode ser alterado após ser Processado.");

            pedido.AtualizarCliente(input.NomeCliente);
            pedido.AtualizarItens(input.Itens);

            await _pedidoRepository.AtualizarPedidoAsync(pedido);

            return true;
        }
    }
}
