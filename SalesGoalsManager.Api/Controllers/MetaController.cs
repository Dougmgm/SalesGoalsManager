using Microsoft.AspNetCore.Mvc;
using SalesGoalsManager.RegraDeNegocio.Cadastro;
using SalesGoalsManager.RegraDeNegocio.Dto;
using SalesGoalsManager.RegraDeNegocio.Repositorios;

namespace SalesGoalsManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MetaController : ControllerBase
    {
        private readonly MetaRepositorio _metaRepository;
        private readonly MetaCadastro _metaCadastro;
        private readonly ProdutoRepositorio _produtoRepository;

        public MetaController(MetaRepositorio metaRepository, MetaCadastro metaCadastro, ProdutoRepositorio produtoRepository)
        {
            _metaRepository = metaRepository;
            _metaCadastro = metaCadastro;
            _produtoRepository = produtoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodas()
        {
            var metas = await _metaRepository.ObterTodasAsync();

            return Ok(metas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(string id)
        {
            var meta = await _metaRepository.ObterPorIdAsync(id);

            if (meta is null)
                return NotFound();

            return Ok(meta);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] MetaVendedorDto meta)
        {
            try
            {
                var produto = await _produtoRepository.ObterPorIdAsync(meta.Produto);

                await _metaCadastro.SalvarAsync(meta, produto);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(string id, [FromBody] MetaVendedorDto meta)
        {
            try
            {
                meta.Id = id;

                var produto = await _produtoRepository.ObterPorIdAsync(meta.Produto);

                await _metaCadastro.SalvarAsync(meta, produto);

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
            await _metaCadastro.ExcluirAsync(id);

            return Ok();
        }
    }
}
