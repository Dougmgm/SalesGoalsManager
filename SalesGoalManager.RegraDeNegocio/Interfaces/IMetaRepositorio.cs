using SalesGoalManager.RegraDeNegocio.Dto;

namespace SalesGoalManager.RegraDeNegocio.Interfaces
{
    public interface IMetaRepositorio
    {
        Task<List<MetaVendedorDto>> ObterTodasAsync();
        Task<MetaVendedorDto> ObterPorIdAsync(string id);
        Task AdicionarAsync(MetaVendedorDto meta);
        Task AtualizarAsync(MetaVendedorDto meta);
        Task RemoverAsync(string id);
    }
}
