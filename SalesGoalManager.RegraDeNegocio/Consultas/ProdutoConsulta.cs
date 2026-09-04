using SalesGoalManager.RegraDeNegocio.Dto;
using SalesGoalManager.RegraDeNegocio.Interfaces;

namespace SalesGoalManager.RegraDeNegocio.Consultas
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
