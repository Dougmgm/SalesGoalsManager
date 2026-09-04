using SalesGoalManager.RegraDeNegocio.Dto;

namespace SalesGoalManager.RegraDeNegocio.Entidades
{
    public class Produto
    {
        public int Id { get; set; }
        public string NomeProduto { get; set; }
        public ProdutoDto.CategoriaProduto Categoria { get; set; }
    }
}