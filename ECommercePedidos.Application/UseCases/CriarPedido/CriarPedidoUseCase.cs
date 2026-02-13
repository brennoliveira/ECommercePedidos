using ECommercePedidos.Domain.Entities;
using ECommercePedidos.Domain.Interfaces.Messaging;
using ECommercePedidos.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommercePedidos.Application.UseCases.CriarPedido
{
    public class CriarPedidoUseCase(
        IPedidoRepository pedidoRepository,
        IMessageBus messageBus) : ICriarPedidoUseCase
    {
        private readonly IPedidoRepository _pedidoRepository = pedidoRepository;
        private readonly IMessageBus _messageBus = messageBus;

        public async Task<Guid> ExecutarAsync(CriarPedidoInput input)
        {
            var pedido = new Pedido(input.Cliente);

            foreach (var item in input.Itens)
            {
                pedido.AdicionarItem(item.Produto, item.Quantidade, item.PrecoUnitario);
            }

            await _pedidoRepository.AdicionarPedidoAsync(pedido);

            await _messageBus.PublicarPedidoCriadoAsync(pedido.Id);

            return pedido.Id;
        }
    }
}
