using SalesGoalsManager.RegraDeNegocio.Dto;

namespace SalesGoalsManager.RegraDeNegocio.Interfaces
{
    public interface IVendedorRepositorio
    {
        Task<List<VendedorDto>> ObterTodosAsync();
        Task<VendedorDto> ObterPorIdAsync(string id);
        Task AdicionarAsync(VendedorDto vendedor);
        Task AtualizarAsync(VendedorDto vendedor);
        Task RemoverAsync(string id);
    }
}
