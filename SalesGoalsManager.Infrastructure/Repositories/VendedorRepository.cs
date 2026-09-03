using Microsoft.EntityFrameworkCore;
using SalesGoalsManager.Domain.Entities;
using SalesGoalsManager.Infrastructure.Data;

namespace SalesGoalsManager.Infrastructure.Repositories
{
    public class VendedorRepository
    {
        private readonly SalesGoalsManagerDbContext _context;

        public VendedorRepository(SalesGoalsManagerDbContext context)
        {
            _context = context;
        }

        public async Task<List<Vendedor>> ObterTodosAsync()
        {
            return await _context.Vendedores.ToListAsync();
        }
    }
}
