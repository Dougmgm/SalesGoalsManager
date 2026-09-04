using SalesGoalManager.RegraDeNegocio.Dto;
using SalesGoalManager.RegraDeNegocio.Interfaces;

namespace SalesGoalManager.RegraDeNegocio.Consultas
{
    public class VendedorConsulta
    {
        private readonly IVendedorRepositorio _vendedorRepositorio;

        public VendedorConsulta(IVendedorRepositorio vendedorRepositorio)
        {
            _vendedorRepositorio = vendedorRepositorio;
        }

        public async Task<List<VendedorDto>> ListarTodosAsync()
        {
            return await _vendedorRepositorio.ObterTodosAsync();
        }
    }
}