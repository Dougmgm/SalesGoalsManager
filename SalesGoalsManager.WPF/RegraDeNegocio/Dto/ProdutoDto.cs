namespace ProjetoCadastros.RegraDeNegocio
{
    public class ProdutoDto
    {
        public string Id { get; set; }
        public string NomeProduto { get; set; }
        public CategoriaProduto Categoria { get; set; }

        public enum CategoriaProduto
        {
            Liquido,
            Diversos
        }
    }
}
