using Microsoft.AspNetCore.Mvc;
using SalesGoalsManager.RegraDeNegocio.Dto;
using SalesGoalsManager.RegraDeNegocio.Entidades;
using SalesGoalsManager.RegraDeNegocio.Repositorios;

namespace SalesGoalsManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoRepositorio _produtoRepository;

        public ProdutoController(ProdutoRepositorio produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var produtos = await _produtoRepository.ObterTodosAsync();

            var dtos = produtos.Select(p => new ProdutoDto
            {
                Id = p.Id.ToString(),
                NomeProduto = p.NomeProduto,
                Categoria = p.Categoria
            });

            return Ok(dtos);
        }
    }
}
