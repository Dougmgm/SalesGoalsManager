namespace SalesGoalManager.RegraDeNegocio.Dto
{
    public class MetaVendedorDto
    {
        public string Id { get; set; }
        public string Vendedor { get; set; }       // FK — guarda VendedorDto.Id
        public string NomeVendedor { get; set; }   // só exibição
        public string Periodicidade { get; set; }
        public string Produto { get; set; }        // FK — guarda ProdutoDto.Id
        public string ProdutoNome { get; set; }     // só exibição
        public TipoMeta TipoMeta { get; set; }
        public decimal ValorMeta { get; set; }
    }
}
