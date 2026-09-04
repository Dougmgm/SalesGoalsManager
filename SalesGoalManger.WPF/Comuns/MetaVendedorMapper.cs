using WpfDto = SalesGoalManger.WPF.RegraDeNegocio.Dto;
using RegraNegocioDto = SalesGoalManager.RegraDeNegocio.Dto;

namespace SalesGoalManger.WPF.Comuns
{
    public static class MetaVendedorMapper
    {
        public static RegraNegocioDto.MetaVendedorDto ParaRegraDeNegocio(WpfDto.MetaVendedorDto origem)
        {
            if (origem is null) return null;

            return new RegraNegocioDto.MetaVendedorDto
            {
                Id = origem.Id,
                Vendedor = origem.Vendedor,
                NomeVendedor = origem.NomeVendedor,
                Produto = origem.Produto,
                ProdutoNome = origem.ProdutoNome,
                Periodicidade = origem.Periodicidade.ToString(),
                TipoMeta = (RegraNegocioDto.TipoMeta)(int)origem.TipoMeta,
                ValorMeta = origem.ValorMeta
            };
        }

        public static WpfDto.MetaVendedorDto ParaWpf(RegraNegocioDto.MetaVendedorDto origem)
        {
            if (origem is null) return null;

            Enum.TryParse<WpfDto.Periodicidade>(origem.Periodicidade, out var periodicidade);

            return new WpfDto.MetaVendedorDto
            {
                Id = origem.Id,
                Vendedor = origem.Vendedor,
                NomeVendedor = origem.NomeVendedor,
                Produto = origem.Produto,
                ProdutoNome = origem.ProdutoNome,
                Periodicidade = periodicidade,
                TipoMeta = (WpfDto.TipoMeta)(int)origem.TipoMeta,
                ValorMeta = origem.ValorMeta
            };
        }
    }
}