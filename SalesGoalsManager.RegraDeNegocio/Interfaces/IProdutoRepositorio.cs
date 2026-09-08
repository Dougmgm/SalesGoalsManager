using SalesGoalsManager.RegraDeNegocio.Dto;

namespace SalesGoalsManager.RegraDeNegocio.Interfaces
{
    public interface IProdutoRepositorio
    {
        Task<List<ProdutoDto>> ObterTodosAsync();
    }
}
