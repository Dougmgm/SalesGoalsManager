using SalesGoalsManager.RegraDeNegocio.Dto;

namespace SalesGoalsManager.RegraDeNegocio.Interfaces
{
    public interface IProdutoRepositorio
    {
        Task<List<ProdutoDto>> ObterTodosAsync();
        Task<ProdutoDto> ObterPorIdAsync(string id);
        Task AdicionarAsync(ProdutoDto produto);
        Task AtualizarAsync(ProdutoDto produto);
        Task RemoverAsync(string id);
    }
}
