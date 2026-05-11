namespace SalesGoalsManager.Api.Models.Domain
{
    public class Meta
    {
        public Guid Id { get; set; }
        public Vendedore Vendedor { get; set; } //FK
        public string Periodicidade { get; set; }
        public string Produto { get; set; } //FK
        public string TipoMeta { get; set; }
        public decimal ValorMeta { get; set; }
    }
}
