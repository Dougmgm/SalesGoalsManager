namespace ProjetoCadastros.RegraDeNegocio.Dto
{
    public class MetaVendedorDto
    {
        public string NomeVendedor { get; set; }
        public Periodicidade Periodicidade { get; set; }
        public string Produto { get; set; }
        public string TipoMeta { get; set; }
        public decimal ValorMeta { get; set; }
    }
}
