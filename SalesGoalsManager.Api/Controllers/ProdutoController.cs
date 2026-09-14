using Microsoft.AspNetCore.Mvc;
using SalesGoalsManager.RegraDeNegocio.Cadastro;
using SalesGoalsManager.RegraDeNegocio.Dto;
using SalesGoalsManager.RegraDeNegocio.Repositorios;

namespace SalesGoalsManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoRepositorio _produtoRepositorio;
        private readonly ProdutoCadastro _cadastroProduto;

        public ProdutoController(ProdutoRepositorio produtoRepository, ProdutoCadastro produto)
        {
            _produtoRepositorio = produtoRepository;
            _cadastroProduto = produto;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var produtos = await _produtoRepositorio.ObterTodosAsync();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(string id)
        {
            var produto = await _produtoRepositorio.ObterPorIdAsync(id);

            if (produto is null)
                return NotFound();

            return Ok(produto);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] ProdutoDto produto)
        {
            try
            {
                await _cadastroProduto.SalvarAsync(produto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(string id, [FromBody] ProdutoDto produto)
        {
            try
            {
                produto.Id = id;
                await _cadastroProduto.SalvarAsync(produto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(string id)
        {
            await _cadastroProduto.ExcluirAsync(id);
            return Ok();
        }
    }
}
