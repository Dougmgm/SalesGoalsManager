namespace SalesGoalManager.RegraDeNegocio.Dto
{
    public class MetaVendedorDto
    {
        public string Id { get; set; }
        public string NomeVendedor { get; set; } //FK
        public string Periodicidade { get; set; }
        public string Produto { get; set; } //FK
        public TipoMeta TipoMeta { get; set; }
        public decimal ValorMeta { get; set; }
    }
}
