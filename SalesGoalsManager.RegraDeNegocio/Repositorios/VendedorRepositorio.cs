using Microsoft.EntityFrameworkCore;
using SalesGoalsManager.RegraDeNegocio.Contexto;
using SalesGoalsManager.RegraDeNegocio.Dto;
using SalesGoalsManager.RegraDeNegocio.Entidades;
using SalesGoalsManager.RegraDeNegocio.Interfaces;

namespace SalesGoalsManager.RegraDeNegocio.Repositorios
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

            return vendedores.Select(ParaDto).ToList();
        }

        public async Task<VendedorDto> ObterPorIdAsync(string id)
        {
            var vendedor = await _context.Vendedores.FindAsync(int.Parse(id));
            return vendedor is null ? null : ParaDto(vendedor);
        }

        public async Task AdicionarAsync(VendedorDto dto)
        {
            var entidade = new Vendedor { Nome = dto.NomeVendedor };
            _context.Vendedores.Add(entidade);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(VendedorDto dto)
        {
            var entidade = await _context.Vendedores.FindAsync(int.Parse(dto.Id));
            if (entidade is null) return;

            entidade.Nome = dto.NomeVendedor;
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(string id)
        {
            var entidade = await _context.Vendedores.FindAsync(int.Parse(id));
            if (entidade is not null)
            {
                _context.Vendedores.Remove(entidade);
                await _context.SaveChangesAsync();
            }
        }

        private static VendedorDto ParaDto(Vendedor vendedor)
        {
            return new VendedorDto
            {
                Id = vendedor.Id.ToString(),
                NomeVendedor = vendedor.Nome
            };
        }
    }
}