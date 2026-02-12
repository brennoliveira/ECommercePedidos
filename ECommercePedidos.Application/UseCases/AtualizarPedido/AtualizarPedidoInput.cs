using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommercePedidos.Domain.Entities;
using ECommercePedidos.Domain.Enums;

namespace ECommercePedidos.Application.UseCases.AtualizarPedido
{
    public class AtualizarPedidoInput
    {
        public string NomeCliente { get; set; } = string.Empty;
        public List<ItemPedido> Itens { get; set; } = [];
    }
}
