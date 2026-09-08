using SalesGoalsManager.RegraDeNegocio.Interfaces;
using SalesGoalsManager.RegraDeNegocio.Dto;

namespace SalesGoalsManager.RegraDeNegocio.Consultas
{
    public class ProdutoConsulta
    {
        private readonly IProdutoRepositorio _produtoRepositorio;

        public ProdutoConsulta(IProdutoRepositorio produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }

        public async Task<List<ProdutoDto>> ListarTodosAsync()
        {
            return await _produtoRepositorio.ObterTodosAsync();
        }
    }
}
