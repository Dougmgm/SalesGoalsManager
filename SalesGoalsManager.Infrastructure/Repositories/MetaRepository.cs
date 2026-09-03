using Microsoft.EntityFrameworkCore;
using SalesGoalsManager.Domain.Entities;
using SalesGoalsManager.Infrastructure.Data;

namespace SalesGoalsManager.Infrastructure.Repositories
{
    public class MetaRepository
    {
        private readonly SalesGoalsManagerDbContext _context;

        public MetaRepository(SalesGoalsManagerDbContext context)
        {
            _context = context;
        }

        public async Task<List<Meta>> ObterTodasAsync()
        {
            return await _context.Metas
                .Include(m => m.Vendedor)
                .Include(m => m.Produto)
                .ToListAsync();
        }

        public async Task<Meta> ObterPorIdAsync(int id)
        {
            return await _context.Metas
                .Include(m => m.Vendedor)
                .Include(m => m.Produto)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<Meta>> ObterExistentesAsync()
        {
            // usado para validação de duplicidade (unicidade)
            return await _context.Metas.AsNoTracking().ToListAsync();
        }

        public async Task AdicionarAsync(Meta meta)
        {
            _context.Metas.Add(meta);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Meta meta)
        {
            _context.Metas.Update(meta);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(int id)
        {
            var meta = await _context.Metas.FindAsync(id);
            if (meta != null)
            {
                _context.Metas.Remove(meta);
                await _context.SaveChangesAsync();
            }
        }
    }
}
