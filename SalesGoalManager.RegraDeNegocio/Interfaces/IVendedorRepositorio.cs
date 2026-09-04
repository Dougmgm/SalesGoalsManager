using SalesGoalManager.RegraDeNegocio.Dto;

namespace SalesGoalManager.RegraDeNegocio.Interfaces
{
    public interface IVendedorRepositorio
    {
        Task<List<VendedorDto>> ObterTodosAsync();
    }
}
