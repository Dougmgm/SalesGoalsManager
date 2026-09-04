using SalesGoalManager.RegraDeNegocio.Dto;

namespace SalesGoalManager.RegraDeNegocio.Entidades
{
    public class Meta
    {
        public int Id { get; set; }

        public int VendedorId { get; set; }
        public Vendedor Vendedor { get; set; }

        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }

        public Periodicidade Periodicidade { get; set; }
        public TipoMeta TipoMeta { get; set; }
        public decimal ValorMeta { get; set; }
    }
}