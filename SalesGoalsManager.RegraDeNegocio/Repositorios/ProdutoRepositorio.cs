using Microsoft.EntityFrameworkCore;
using SalesGoalsManager.RegraDeNegocio.Contexto;
using SalesGoalsManager.RegraDeNegocio.Dto;
using SalesGoalsManager.RegraDeNegocio.Entidades;
using SalesGoalsManager.RegraDeNegocio.Interfaces;

namespace SalesGoalsManager.RegraDeNegocio.Repositorios
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
            return produtos.Select(ParaDto).ToList();
        }

        public async Task<ProdutoDto> ObterPorIdAsync(string id)
        {
            var produto = await _context.Produtos.FindAsync(int.Parse(id));
            return produto is null ? null : ParaDto(produto);
        }

        public async Task AdicionarAsync(ProdutoDto dto)
        {
            var entidade = new Produto
            {
                NomeProduto = dto.NomeProduto,
                Categoria = dto.Categoria
            };

            _context.Produtos.Add(entidade);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(ProdutoDto dto)
        {
            var entidade = await _context.Produtos.FindAsync(int.Parse(dto.Id));
            if (entidade is null) return;

            entidade.NomeProduto = dto.NomeProduto;
            entidade.Categoria = dto.Categoria;

            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(string id)
        {
            var entidade = await _context.Produtos.FindAsync(int.Parse(id));
            if (entidade is not null)
            {
                _context.Produtos.Remove(entidade);
                await _context.SaveChangesAsync();
            }
        }

        private static ProdutoDto ParaDto(Produto produto)
        {
            return new ProdutoDto
            {
                Id = produto.Id.ToString(),
                NomeProduto = produto.NomeProduto,
                Categoria = produto.Categoria
            };
        }
    }
}