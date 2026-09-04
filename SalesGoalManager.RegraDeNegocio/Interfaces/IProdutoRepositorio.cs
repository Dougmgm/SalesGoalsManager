using SalesGoalManager.RegraDeNegocio.Dto;

namespace SalesGoalManager.RegraDeNegocio.Interfaces
{
    public interface IProdutoRepositorio
    {
        Task<List<ProdutoDto>> ObterTodosAsync();
    }
}
