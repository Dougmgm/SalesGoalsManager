using Microsoft.EntityFrameworkCore;
using SalesGoalManager.RegraDeNegocio.Contexto;
using SalesGoalManager.RegraDeNegocio.Dto;
using SalesGoalManager.RegraDeNegocio.Interfaces;

namespace SalesGoalManager.RegraDeNegocio.Repositorios
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        private readonly SalesGoalsManagerDbContext _context;

        public ProdutoRepositorio(SalesGoalsManagerDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProdutoDto>> ObterTodosAsync()
        {
            var produtos = await _context.Produtos.ToListAsync();

            return produtos.Select(p => new ProdutoDto
            {
                Id = p.Id.ToString(),
                NomeProduto = p.NomeProduto,
                Categoria = p.Categoria
            }).ToList();
        }
    }
}