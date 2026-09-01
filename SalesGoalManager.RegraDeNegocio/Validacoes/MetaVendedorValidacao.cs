using SalesGoalManager.RegraDeNegocio.Comuns;
using SalesGoalManager.RegraDeNegocio.Dto;
using SalesGoalManager.RegraDeNegocio.Extensoes;
using SalesGoalManager.RegraDeNegocio.Extensoes.Exceptions;

namespace SalesGoalManager.RegraDeNegocio.Validacoes
{
    public class MetaVendedorValidacao
    {
        public void Validar(MetaVendedorDto meta, ProdutoDto produto)
        {
            var erros = new List<string>();

            if (string.IsNullOrWhiteSpace(meta.NomeVendedor))
                erros.Add(Constantes.MsgVendedorNaoPreenchido);

            if (produto is null)
                erros.Add(Constantes.MsgProdutoNaoPreenchido);

            if (meta.ValorMeta <= 0)
                erros.Add(Constantes.MsgValorMetaNaoPreenchida);

            if (meta.TipoMeta.IsEmpty())
                erros.Add(Constantes.MsgTipoMetaNaoPreenchida);

            if (meta.Periodicidade.IsNull())
                erros.Add(Constantes.MsgPeriodicidadeNaoPreenchida);

            if (produto is not null)
                ValidarCompatibilidadeTipoMetaProduto(meta, produto, erros);

            if (erros.Any())
                throw new ValidacaoDadosException(string.Join(Environment.NewLine, erros));
        }

        private void ValidarCompatibilidadeTipoMetaProduto(MetaVendedorDto meta, ProdutoDto produto, List<string> erros)
        {
            bool metaEmLitros = meta.TipoMeta == TipoMeta.Litros;

            bool produtoNaoLiquido = produto.Categoria != ProdutoDto.CategoriaProduto.Liquido;

            if (metaEmLitros && produtoNaoLiquido)
                erros.Add(Constantes.MsgMetaLitrosParaProdutoLiquido);
        }
    }
}
