using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommercePedidos.Application.UseCases.CriarPedido
{
    public class CriarPedidoInput
    {
        public string Cliente { get; set; } = string.Empty;
        public List<ItemInput> Itens { get; set; } = [];
    }

    public class ItemInput
    {
        public string Produto { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
    }
}
