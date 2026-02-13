using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommercePedidos.Application.UseCases.ProcessarPedido
{
    public interface IProcessarPedidoUseCase
    {
        Task ExecutarAsync(Guid pedidoId);
    }
}
