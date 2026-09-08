using Microsoft.AspNetCore.Mvc;
using SalesGoalsManager.RegraDeNegocio.Dto;
using SalesGoalsManager.RegraDeNegocio.Entidades;
using SalesGoalsManager.RegraDeNegocio.Repositorios;

namespace SalesGoalsManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendedorController : ControllerBase
    {
        private readonly VendedorRepositorio _vendedorRepository;

        public VendedorController(VendedorRepositorio vendedorRepository)
        {
            _vendedorRepository = vendedorRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var vendedores = await _vendedorRepository.ObterTodosAsync();

            var dtos = vendedores.Select(v => new VendedorDto
            {
                Id = v.Id.ToString(),
                NomeVendedor = v.NomeVendedor
            });

            return Ok(dtos);
        }
    }
}
