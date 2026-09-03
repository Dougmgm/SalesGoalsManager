using Microsoft.AspNetCore.Mvc;
using SalesGoalManager.RegraDeNegocio.Dto;
using SalesGoalsManager.Infrastructure.Repositories;

namespace SalesGoalsManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoRepository _produtoRepository;

        public ProdutoController(ProdutoRepository produtoRepository)
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
