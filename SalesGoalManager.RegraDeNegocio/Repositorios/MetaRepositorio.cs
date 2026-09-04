using Microsoft.EntityFrameworkCore;
using SalesGoalManager.RegraDeNegocio.Contexto;
using SalesGoalManager.RegraDeNegocio.Dto;
using SalesGoalManager.RegraDeNegocio.Entidades;
using SalesGoalManager.RegraDeNegocio.Interfaces;

namespace SalesGoalManager.RegraDeNegocio.Repositorios
{
    public class MetaRepositorio : IMetaRepositorio
    {
        private readonly SalesGoalsManagerDbContext _context;

        public MetaRepositorio(SalesGoalsManagerDbContext context)
        {
            _context = context;
        }

        public async Task<List<MetaVendedorDto>> ObterTodasAsync()
        {
            var metas = await _context.Metas
                .Include(m => m.Vendedor)
                .Include(m => m.Produto)
                .ToListAsync();

            return metas.Select(ParaDto).ToList();
        }

        public async Task<MetaVendedorDto> ObterPorIdAsync(string id)
        {
            var meta = await _context.Metas
                .Include(m => m.Vendedor)
                .Include(m => m.Produto)
                .FirstOrDefaultAsync(m => m.Id == int.Parse(id));

            return meta is null ? null : ParaDto(meta);
        }

        public async Task AdicionarAsync(MetaVendedorDto dto)
        {
            var entidade = new Meta
            {
                VendedorId = int.Parse(dto.Vendedor),
                ProdutoId = int.Parse(dto.Produto),
                Periodicidade = Enum.Parse<Periodicidade>(dto.Periodicidade),
                TipoMeta = dto.TipoMeta,
                ValorMeta = dto.ValorMeta
            };

            _context.Metas.Add(entidade);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(MetaVendedorDto dto)
        {
            var entidade = await _context.Metas.FindAsync(int.Parse(dto.Id));
            if (entidade is null) return;

            entidade.VendedorId = int.Parse(dto.Vendedor);
            entidade.ProdutoId = int.Parse(dto.Produto);
            entidade.Periodicidade = Enum.Parse<Periodicidade>(dto.Periodicidade);
            entidade.TipoMeta = dto.TipoMeta;
            entidade.ValorMeta = dto.ValorMeta;

            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(string id)
        {
            var entidade = await _context.Metas.FindAsync(int.Parse(id));
            if (entidade is not null)
            {
                _context.Metas.Remove(entidade);
                await _context.SaveChangesAsync();
            }
        }

        private static MetaVendedorDto ParaDto(Meta meta)
        {
            return new MetaVendedorDto
            {
                Id = meta.Id.ToString(),
                Vendedor = meta.VendedorId.ToString(),
                NomeVendedor = meta.Vendedor?.Nome,
                Produto = meta.ProdutoId.ToString(),
                ProdutoNome = meta.Produto?.NomeProduto,
                Periodicidade = meta.Periodicidade.ToString(),
                TipoMeta = meta.TipoMeta,
                ValorMeta = meta.ValorMeta
            };
        }
    }
}