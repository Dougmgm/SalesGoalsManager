using SalesGoalsManager.RegraDeNegocio.Dto;
using SalesGoalsManager.RegraDeNegocio.Validacoes;
using System.ComponentModel.DataAnnotations;

namespace SalesGoalsManager.RegraDeNegocio.Testes.Validacoes
{
    public class CadastroProdutoTeste
    {
        private readonly CadastroProdutoValidacao _validacao = new();

        private static ProdutoDto CriarProdutoValido()
        {
            return new ProdutoDto
            {
                Id = "1",
                NomeProduto = "Barris",
                Categoria = ProdutoDto.CategoriaProduto.Liquido
            };
        }

        [Fact]
        public void Validar_Deve_Lancar_Excecao_Quando_Nome_Produto_Nao_Preenchido()
        {
            var produto = CriarProdutoValido();
            produto.NomeProduto = "";

            var excecao = Assert.Throws<ValidationException>(
                () => _validacao.Validar(produto, new List<ProdutoDto>()));

            Assert.Contains("Nome do produto", excecao.Message);
        }

        [Fact]
        public void Validar_Deve_Lanca_Excecao_Quando_Nome_Produto_For_Nulo()
        {
            var produto = CriarProdutoValido();
            produto.NomeProduto = null;

            var excecao = Assert.Throws<ValidationException>(
                () => _validacao.Validar(produto, new List<ProdutoDto>()));

            Assert.Contains("Nome do produto", excecao.Message);
        }

        [Fact]
        public void Validar_Deve_Lancar_Excecao_Quando_Ja_Existe_Produto_Com_Mesmo_Nome()
        {
            var produto = CriarProdutoValido();
            produto.Id = "1";

            var produtoExistente = CriarProdutoValido();
            produtoExistente.Id = "2";
            produtoExistente.NomeProduto = produto.NomeProduto;

            var excecao = Assert.Throws<ValidationException>(
                () => _validacao.Validar(produto, new List<ProdutoDto> { produtoExistente }));

            Assert.Contains("Já existe um produto cadastrado", excecao.Message);
        }

        [Fact]
        public void Validar_Deve_Lancar_Excecao_Quando_Nome_Duplicado_Com_Diferenca_De_Case()
        {
            var produto = CriarProdutoValido();
            produto.Id = "1";
            produto.NomeProduto = "barris";

            var produtoExistente = CriarProdutoValido();
            produtoExistente.Id = "2";
            produtoExistente.NomeProduto = "BARRIS";

            var excecao = Assert.Throws<ValidationException>(
                () => _validacao.Validar(produto, new List<ProdutoDto> { produtoExistente }));

            Assert.Contains("Já existe um produto cadastrado", excecao.Message);
        }

        [Fact]
        public void Validar_Nao_Deve_Lancar_Excecao_Quando_Editando_Mesmo_Produto_Sem_Mudar_Nome()
        {
            var produto = CriarProdutoValido();
            produto.Id = "1";

            var listaComEleMesmo = new List<ProdutoDto> { produto };

            var excecao = Record.Exception(
                () => _validacao.Validar(produto, listaComEleMesmo));

            Assert.Null(excecao);
        }

        [Fact]
        public void Validar_Nao_Deve_Lancar_Excecao_Quando_Nomes_Diferentes()
        {
            var produto = CriarProdutoValido();
            produto.Id = "1";

            var outroProduto = CriarProdutoValido();
            outroProduto.Id = "2";
            outroProduto.NomeProduto = "Garrafas e Latas";

            var excecao = Record.Exception(
                () => _validacao.Validar(produto, new List<ProdutoDto> { outroProduto }));

            Assert.Null(excecao);
        }

        [Fact]
        public void Validar_Nao_Deve_Lancar_Excecao_Quando_Todos_Os_Campos_Validos_E_Nenhuma_Duplicidade()
        {
            var produto = CriarProdutoValido();

            var excecao = Record.Exception(
                () => _validacao.Validar(produto, new List<ProdutoDto>()));

            Assert.Null(excecao);
        }

        [Theory]
        [InlineData(ProdutoDto.CategoriaProduto.Liquido)]
        [InlineData(ProdutoDto.CategoriaProduto.Diversos)]
        public void Validar_Nao_Deve_Lancar_Excecao_Para_Qualquer_Categoria_Valida(ProdutoDto.CategoriaProduto categoria)
        {
            var produto = CriarProdutoValido();
            produto.Categoria = categoria;

            var excecao = Record.Exception(
                () => _validacao.Validar(produto, new List<ProdutoDto>()));

            Assert.Null(excecao);
        }
    }
}
