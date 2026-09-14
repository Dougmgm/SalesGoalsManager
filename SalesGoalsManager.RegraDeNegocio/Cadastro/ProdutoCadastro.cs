using SalesGoalsManager.RegraDeNegocio.Dto;
using SalesGoalsManager.RegraDeNegocio.Interfaces;
using SalesGoalsManager.RegraDeNegocio.Validacoes;

namespace SalesGoalsManager.RegraDeNegocio.Cadastro
{
    public class ProdutoCadastro
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly CadastroProdutoValidacao _validacao;

        public ProdutoCadastro(IProdutoRepositorio produtoRepositorio, CadastroProdutoValidacao validacao)
        {
            _produtoRepositorio = produtoRepositorio;
            _validacao = validacao;
        }

        public async Task SalvarAsync(ProdutoDto produto)
        {
            var produtosExistentes = await _produtoRepositorio.ObterTodosAsync();

            _validacao.Validar(produto, produtosExistentes);

            bool ehEdicao = !string.IsNullOrEmpty(produto.Id);

            if (ehEdicao)
                await _produtoRepositorio.AtualizarAsync(produto);
            else
                await _produtoRepositorio.AdicionarAsync(produto);
        }

        public async Task ExcluirAsync(string id)
        {
            await _produtoRepositorio.RemoverAsync(id);
        }
    }
}
