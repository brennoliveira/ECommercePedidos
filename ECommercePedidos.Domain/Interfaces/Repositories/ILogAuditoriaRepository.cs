using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommercePedidos.Domain.Interfaces.Repositories
{
    public interface ILogAuditoriaRepository
    {
        Task SalvarLogAsync(Guid pedidoId, string mensagem);
    }
}
