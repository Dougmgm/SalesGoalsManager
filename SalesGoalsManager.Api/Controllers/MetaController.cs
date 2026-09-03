using Microsoft.AspNetCore.Mvc;
using SalesGoalManager.RegraDeNegocio.Dto;
using SalesGoalsManager.Domain.Entities;
using SalesGoalsManager.Infrastructure.Repositories;

namespace SalesGoalsManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MetaController : ControllerBase
    {
        private readonly MetaRepository _metaRepository;

        public MetaController(MetaRepository metaRepository)
        {
            _metaRepository = metaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodas()
        {
            var metas = await _metaRepository.ObterTodasAsync();
            return Ok(metas.Select(ParaDto));
        }

        private static MetaVendedorDto ParaDto(Meta meta)
        {
            return new MetaVendedorDto
            {
                Id = meta.Id.ToString(),
                Vendedor = meta.VendedorId.ToString(),
                NomeVendedor = meta.Vendedor?.Nome,
                Produto = meta.ProdutoId.ToString(),
                ProdutoNome = meta.Produto?.NomeProduto,
                Periodicidade = meta.Periodicidade.ToString(),
                TipoMeta = meta.TipoMeta,
                ValorMeta = meta.ValorMeta
            };
        }
    }
}
