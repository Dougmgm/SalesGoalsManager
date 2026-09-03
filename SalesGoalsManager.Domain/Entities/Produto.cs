using SalesGoalManager.RegraDeNegocio.Dto;

namespace SalesGoalsManager.Domain.Entities
{
    public class Produto
    {
        public int Id { get; set; }
        public string NomeProduto { get; set; }
        public ProdutoDto.CategoriaProduto Categoria { get; set; }
    }
}
