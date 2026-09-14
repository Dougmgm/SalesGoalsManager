using Microsoft.AspNetCore.Mvc;
using SalesGoalsManager.RegraDeNegocio.Cadastro;
using SalesGoalsManager.RegraDeNegocio.Dto;
using SalesGoalsManager.RegraDeNegocio.Repositorios;

namespace SalesGoalsManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendedorController : ControllerBase
    {
        private readonly VendedorRepositorio _vendedorRepositorio;
        private readonly VendedorCadastro _vendedor;

        public VendedorController(VendedorRepositorio vendedorRepository, VendedorCadastro vendedor)
        {
            _vendedorRepositorio = vendedorRepository;
            _vendedor = vendedor;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var vendedores = await _vendedorRepositorio.ObterTodosAsync();
            return Ok(vendedores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(string id)
        {
            var vendedor = await _vendedorRepositorio.ObterPorIdAsync(id);

            if (vendedor is null)
                return NotFound();

            return Ok(vendedor);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] VendedorDto vendedor)
        {
            try
            {
                await _vendedor.SalvarAsync(vendedor);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(string id, [FromBody] VendedorDto vendedor)
        {
            try
            {
                vendedor.Id = id;
                await _vendedor.SalvarAsync(vendedor);
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
            await _vendedor.ExcluirAsync(id);
            return Ok();
        }
    }
}
