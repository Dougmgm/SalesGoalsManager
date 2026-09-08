using SalesGoalsManager.RegraDeNegocio.Dto;

namespace SalesGoalsManager.RegraDeNegocio.Entidades
{
    public class Produto
    {
        public int Id { get; set; }
        public string NomeProduto { get; set; }
        public ProdutoDto.CategoriaProduto Categoria { get; set; }
    }
}