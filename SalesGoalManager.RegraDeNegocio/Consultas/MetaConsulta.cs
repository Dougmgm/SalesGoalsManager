using SalesGoalManager.RegraDeNegocio.Dto;
using SalesGoalManager.RegraDeNegocio.Interfaces;

namespace SalesGoalManager.RegraDeNegocio.Consultas
{
    public class MetaConsulta
    {
        private readonly IMetaRepositorio _metaRepositorio;

        public MetaConsulta(IMetaRepositorio metaRepositorio)
        {
            _metaRepositorio = metaRepositorio;
        }

        public async Task<List<MetaVendedorDto>> ListarTodasAsync()
        {
            return await _metaRepositorio.ObterTodasAsync();
        }

        public async Task<MetaVendedorDto> ObterPorIdAsync(string id)
        {
            return await _metaRepositorio.ObterPorIdAsync(id);
        }

        public async Task<List<MetaVendedorDto>> BuscarAsync(string termo)
        {
            var todas = await _metaRepositorio.ObterTodasAsync();

            if (string.IsNullOrWhiteSpace(termo))
                return todas;

            termo = termo.Trim().ToUpper();

            return todas.Where(x =>
                x.NomeVendedor.ToUpper().Contains(termo) ||
                x.ProdutoNome.ToUpper().Contains(termo) ||
                x.TipoMeta.ToString().ToUpper().Contains(termo)).ToList();
        }
    }
}