using SalesGoalManager.RegraDeNegocio.Dto;
using SalesGoalManager.RegraDeNegocio.Interfaces;
using SalesGoalManager.RegraDeNegocio.Validacoes;

namespace SalesGoalManager.RegraDeNegocio.Cadastros
{
    public class MetaCadastro
    {
        private readonly IMetaRepositorio _metaRepositorio;
        private readonly MetaVendedorValidacao _validacao;

        public MetaCadastro(IMetaRepositorio metaRepositorio, MetaVendedorValidacao validacao)
        {
            _metaRepositorio = metaRepositorio;
            _validacao = validacao;
        }

        public async Task SalvarAsync(MetaVendedorDto meta, ProdutoDto produto)
        {
            var metasExistentes = await _metaRepositorio.ObterTodasAsync();

            _validacao.Validar(meta, new System.Collections.ObjectModel.ObservableCollection<MetaVendedorDto>(metasExistentes), produto);

            bool ehEdicao = !string.IsNullOrEmpty(meta.Id);

            if (ehEdicao)
                await _metaRepositorio.AtualizarAsync(meta);
            else
                await _metaRepositorio.AdicionarAsync(meta);
        }

        public async Task ExcluirAsync(string id)
        {
            await _metaRepositorio.RemoverAsync(id);
        }
    }
}