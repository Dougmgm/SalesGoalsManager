using Microsoft.AspNetCore.Mvc;
using SalesGoalsManager.RegraDeNegocio.Dto;
using SalesGoalsManager.RegraDeNegocio.Entidades;
using SalesGoalsManager.RegraDeNegocio.Repositorios;

namespace SalesGoalsManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MetaController : ControllerBase
    {
        private readonly MetaRepositorio _metaRepository;

        public MetaController(MetaRepositorio metaRepository)
        {
            _metaRepository = metaRepository;
        }

        //[HttpGet]
        //public async Task<IActionResult> ObterTodas()
        //{
        //    var metas = await _metaRepository.ObterTodasAsync();
        //    return Ok(metas.Select(ParaDto));
        //}

        [HttpGet]
        public async Task<IActionResult> ObterTodas()
        {
            var metas = await _metaRepository.ObterTodasAsync();

            return Ok(metas);
        }

        //private static MetaVendedorDto ParaDto(Meta meta)
        //{
        //    return new MetaVendedorDto
        //    {
        //        Id = meta.Id.ToString(),
        //        Vendedor = meta.VendedorId.ToString(),
        //        NomeVendedor = meta.Vendedor?.Nome,
        //        Produto = meta.ProdutoId.ToString(),
        //        ProdutoNome = meta.Produto?.NomeProduto,
        //        Periodicidade = meta.Periodicidade.ToString(),
        //        TipoMeta = meta.TipoMeta,
        //        ValorMeta = meta.ValorMeta
        //    };
        //}
    }
}
