using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommercePedidos.Application.UseCases.AtualizarPedido
{
    public interface IAtualizarPedidoUseCase
    {
        Task<bool> ExecutarAsync(Guid id, AtualizarPedidoInput input);
    }
}
