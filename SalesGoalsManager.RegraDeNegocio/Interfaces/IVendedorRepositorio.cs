using SalesGoalsManager.RegraDeNegocio.Dto;

namespace SalesGoalsManager.RegraDeNegocio.Interfaces
{
    public interface IVendedorRepositorio
    {
        Task<List<VendedorDto>> ObterTodosAsync();
    }
}
