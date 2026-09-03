using Microsoft.AspNetCore.Mvc;
using SalesGoalManager.RegraDeNegocio.Dto;
using SalesGoalsManager.Infrastructure.Repositories;

namespace SalesGoalsManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendedorController : ControllerBase
    {
        private readonly VendedorRepository _vendedorRepository;

        public VendedorController(VendedorRepository vendedorRepository)
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
                NomeVendedor = v.Nome
            });

            return Ok(dtos);
        }
    }
}
