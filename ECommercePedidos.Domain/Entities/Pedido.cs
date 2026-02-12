using ECommercePedidos.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommercePedidos.Domain.Entities
{
    public class Pedido
    {
        public Guid Id { get; private set; }
        public string Cliente { get; private set; }
        public decimal ValorTotal { get; private set; }
        public PedidoStatus Status { get; private set; }
        public bool Ativo { get; private set; }

        private readonly List<ItemPedido> _itens = [];
        public IReadOnlyCollection<ItemPedido> Itens => _itens.AsReadOnly();

        protected Pedido() { } 

        public Pedido(string cliente)
        {
            Id = Guid.NewGuid();
            Cliente = cliente;
            ValorTotal = 0m;
            Status = PedidoStatus.Recebido;
            Ativo = true;
        }

        public void RecalcularValorTotal()
        {
            ValorTotal = _itens.Sum(i => i.SubTotal);
        }
        public void AdicionarItem(string produto, int quantidade, decimal precoUnitario)
        {
            if (Status != PedidoStatus.Recebido)
                throw new InvalidOperationException("Não é possível alterar um pedido já processado.");

            if (quantidade <= 0)
                throw new ArgumentException("Quantidade deve ser maior que zero.");

            if (precoUnitario <= 0)
                throw new ArgumentException("Preço unitário deve ser maior que zero.");

            var item = new ItemPedido(produto, quantidade, precoUnitario);
            _itens.Add(item);

            RecalcularValorTotal();
        }

        public void AtualizarItens(List<ItemPedido> itens)
        {
            if (Status != PedidoStatus.Recebido)
                throw new InvalidOperationException("Não é possível alterar um pedido já processado.");

            _itens.Clear();
            _itens.AddRange(itens);

            RecalcularValorTotal();
        }

        public void MarcarComoProcessado()
        {
            if (Status != PedidoStatus.Recebido)
                throw new InvalidOperationException("Somente pedidos recebidos podem ser processados.");

            Status = PedidoStatus.Processado;
        }

        public void Cancelar()
        {
            Ativo = false;
            Status = PedidoStatus.Cancelado;
        }

        public void AtualizarCliente(string novoCliente)
        {
            if (Status != PedidoStatus.Recebido)
                throw new InvalidOperationException("Não é possível alterar um pedido já processado");

            Cliente = novoCliente;
        }
    }
}
