using Microsoft.EntityFrameworkCore;
using SalesGoalsManager.Domain.Entities;
using SalesGoalsManager.Infrastructure.Data;

namespace SalesGoalsManager.Infrastructure.Repositories
{
    public class ProdutoRepository
    {
        private readonly SalesGoalsManagerDbContext _context;

        public ProdutoRepository(SalesGoalsManagerDbContext context)
        {
            _context = context;
        }

        public async Task<List<Produto>> ObterTodosAsync()
        {
            return await _context.Produtos.ToListAsync();
        }
    }
}
