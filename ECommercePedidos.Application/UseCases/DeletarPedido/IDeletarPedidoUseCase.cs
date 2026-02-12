using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommercePedidos.Application.UseCases.DeletarPedido
{
    public interface IDeletarPedidoUseCase
    {
        Task<bool> ExecutarAsync(Guid id);
    }
}
