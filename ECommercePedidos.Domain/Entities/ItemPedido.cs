using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommercePedidos.Domain.Entities
{
    public class ItemPedido
    {
        public Guid Id { get; set; }
        public string Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }

        public decimal SubTotal => Quantidade * PrecoUnitario;

        protected ItemPedido() { }

        public ItemPedido (string produto, int quantidade, decimal precoUnitario)
        {
            Id = Guid.NewGuid();
            Produto = produto;
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
        }
    }
}
