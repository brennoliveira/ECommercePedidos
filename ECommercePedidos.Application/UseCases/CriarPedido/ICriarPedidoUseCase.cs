using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommercePedidos.Application.UseCases.CriarPedido
{
    public interface ICriarPedidoUseCase
    {
        Task<Guid> ExecutarAsync(CriarPedidoInput input);
    }
}
