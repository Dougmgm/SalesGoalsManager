using SalesGoalsManager.RegraDeNegocio.Comuns;
using SalesGoalsManager.RegraDeNegocio.Dto;
using SalesGoalsManager.RegraDeNegocio.Extensoes;
using System.ComponentModel.DataAnnotations;

namespace SalesGoalsManager.RegraDeNegocio.Validacoes
{
    public class CadastroProdutoValidacao
    {
        public void Validar(ProdutoDto produto, List<ProdutoDto> produtosExistentes)
        {
            var erros = new List<string>();

            ValidarCamposObrigatorios(produto, erros);
            ValidarNomeDuplicado(produto, produtosExistentes, erros);

            LançarExcecaoSeHouverErros(erros);
        }

        private void ValidarCamposObrigatorios(ProdutoDto produto, List<string> erros)
        {
            if (produto.NomeProduto.IsNullOrEmpty())
                erros.Add(Constantes.MsgNomeProdutoNaoPreenchido);

            if (produto.Categoria.IsNull())
                erros.Add(Constantes.MsgCategoriaNaoSelecionada);
        }

        private void ValidarNomeDuplicado(ProdutoDto produto, List<ProdutoDto> produtosExistentes, List<string> erros)
        {
            bool nomeDuplicado = produtosExistentes.Any(p =>
                p.Id != produto.Id &&
                p.NomeProduto.Trim().Equals(produto.NomeProduto?.Trim(), StringComparison.OrdinalIgnoreCase));

            if (nomeDuplicado)
                erros.Add(Constantes.MsgProdutoMesmoNome);
        }

        private void LançarExcecaoSeHouverErros(List<string> erros)
        {
            if (erros.Any())
                throw new ValidationException(string.Join(Environment.NewLine, erros));
        }
    }
}
