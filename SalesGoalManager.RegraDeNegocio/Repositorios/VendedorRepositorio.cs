using Microsoft.EntityFrameworkCore;
using SalesGoalManager.RegraDeNegocio.Contexto;
using SalesGoalManager.RegraDeNegocio.Dto;
using SalesGoalManager.RegraDeNegocio.Interfaces;

namespace SalesGoalManager.RegraDeNegocio.Repositorios
{
    public class VendedorRepositorio : IVendedorRepositorio
    {
        private readonly SalesGoalsManagerDbContext _context;

        public VendedorRepositorio(SalesGoalsManagerDbContext context)
        {
            _context = context;
        }

        public async Task<List<VendedorDto>> ObterTodosAsync()
        {
            var vendedores = await _context.Vendedores.ToListAsync();

            return vendedores.Select(v => new VendedorDto
            {
                Id = v.Id.ToString(),
                NomeVendedor = v.Nome
            }).ToList();
        }
    }
}