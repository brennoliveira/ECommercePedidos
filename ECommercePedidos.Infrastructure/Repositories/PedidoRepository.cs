using ECommercePedidos.Domain.Entities;
using ECommercePedidos.Domain.Enums;
using ECommercePedidos.Domain.Interfaces.Repositories;
using ECommercePedidos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommercePedidos.Infrastructure.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _context;

        public PedidoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarPedidoAsync(Pedido pedido)
        {
            await _context.Pedidos.AddAsync(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task<Pedido?> ObterPedidoPorIdAsync(Guid id)
        {
            return await _context.Pedidos
                .Include(p => p.Itens)
                .FirstOrDefaultAsync(p => p.Id == id && p.Ativo);
        }

        public async Task<IEnumerable<Pedido>> ObterTodosPedidosAsync(int page, int pageSize, PedidoStatus? status)
        {
            var query = _context.Pedidos
                .Include(p => p.Itens)
                .Where(p => p.Ativo)
                .AsQueryable();

            if (status.HasValue)
                query = query.Where(p => p.Status == status.Value);

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }   

        public async Task AtualizarPedidoAsync(Pedido pedido)
        {
            _context.Pedidos.Update(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task ExcluirPedidoAsync(Guid id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedidos.Remove(pedido);
                await _context.SaveChangesAsync();
            }
        }
    }
}
