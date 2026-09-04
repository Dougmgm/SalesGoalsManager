using SalesGoalManager.RegraDeNegocio.Comuns;
using SalesGoalManager.RegraDeNegocio.Dto;
using SalesGoalManager.RegraDeNegocio.Extensoes.Exceptions;
using SalesGoalManager.RegraDeNegocio.Validacoes;
using System.Collections.ObjectModel;
using Xunit;

namespace SalesGoalManager.RegraDeNegocio.Testes.Validacoes
{
    public class MetaVendedorValidacaoTestes
    {
        private readonly MetaVendedorValidacao _validacao = new();

        [Fact]
        public async Task ListarTodosAsync_DeveRetornarProdutosDoBanco()
        {
            var consulta = Fabrica.CriarProdutoConsulta();
            var produtos = await consulta.ListarTodosAsync();

            Assert.NotEmpty(produtos);
        }

        private static MetaVendedorDto CriarMetaValida()
        {
            return new MetaVendedorDto
            {
                Id = "1",
                NomeVendedor = "João da Silva",
                Produto = "1",
                Periodicidade = Periodicidade.Mensal.ToString(),
                TipoMeta = TipoMeta.Monetario,
                ValorMeta = 1500
            };
        }

        private static ProdutoDto CriarProdutoLiquido()
        {
            return new ProdutoDto
            {
                Id = "1",
                NomeProduto = "Barris",
                Categoria = ProdutoDto.CategoriaProduto.Liquido
            };
        }

        private static ProdutoDto CriarProdutoDiverso()
        {
            return new ProdutoDto
            {
                Id = "3",
                NomeProduto = "Acessórios e Produtos",
                Categoria = ProdutoDto.CategoriaProduto.Diversos
            };
        }

        [Fact]
        public void Validar_Deve_Lancar_Excecao_Quando_Nome_Vendedor_Nao_Preenchido()
        {
            var meta = CriarMetaValida();
            meta.NomeVendedor = "";

            var excecao = Assert.Throws<ValidacaoDadosException>(
                () => _validacao.Validar(meta, new ObservableCollection<MetaVendedorDto>(), CriarProdutoLiquido()));

            Assert.Contains(Constantes.MsgVendedorNaoPreenchido, excecao.Message);
        }

        [Fact]
        public void Validar_Deve_Lancar_Excecao_Quando_Produto_Nao_Informado()
        {
            var meta = CriarMetaValida();

            var excecao = Assert.Throws<ValidacaoDadosException>(
                () => _validacao.Validar(meta, new ObservableCollection<MetaVendedorDto>(), produto: null));

            Assert.Contains(Constantes.MsgProdutoNaoPreenchido, excecao.Message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void Validar_Deve_Lancar_Excecao_Quando_Valor_Meta_Menor_Ou_Igual_Zero(decimal valorInvalido)
        {
            var meta = CriarMetaValida();
            meta.ValorMeta = valorInvalido;

            var excecao = Assert.Throws<ValidacaoDadosException>(
                () => _validacao.Validar(meta, new ObservableCollection<MetaVendedorDto>(), CriarProdutoLiquido()));

            Assert.Contains(Constantes.MsgValorMetaNaoPreenchida, excecao.Message);
        }

        [Fact]
        public void Validar_Deve_Lancar_Excecao_Quando_Tipo_Meta_Nao_Preenchido()
        {
            var meta = CriarMetaValida();
            meta.TipoMeta = default; 

            var excecao = Assert.Throws<ValidacaoDadosException>(
                () => _validacao.Validar(meta, new ObservableCollection<MetaVendedorDto>(), CriarProdutoLiquido()));

            Assert.Contains(Constantes.MsgTipoMetaNaoPreenchida, excecao.Message);
        }

        [Theory]
        [InlineData("0")]
        [InlineData("")]
        [InlineData("TESTE")]
        public void Validar_Deve_Lancar_Excecao_Quando_Periodicidade_Invalida_Ou_Nao_Preenchida(string periodicidadeInvalida)
        {
            var meta = CriarMetaValida();
            meta.Periodicidade = periodicidadeInvalida;

            var excecao = Assert.Throws<ValidacaoDadosException>(
                () => _validacao.Validar(meta, new ObservableCollection<MetaVendedorDto>(), CriarProdutoLiquido()));

            Assert.Contains(Constantes.MsgPeriodicidadeNaoPreenchida, excecao.Message);
        }

        [Fact]
        public void Validar_Deve_Lancar_Excecao_Quando_Tipo_Meta_Litros_Para_Produto_Nao_Liquido()
        {
            var meta = CriarMetaValida();
            meta.TipoMeta = TipoMeta.Litros;

            var excecao = Assert.Throws<ValidacaoDadosException>(
                () => _validacao.Validar(meta, new ObservableCollection<MetaVendedorDto>(), CriarProdutoDiverso()));

            Assert.Contains(Constantes.MsgMetaLitrosParaProdutoLiquido, excecao.Message);
        }

        [Fact]
        public void Validar_Nao_Deve_Lancar_Excecao_Quando_Tipo_Meta_Litros_Para_Produto_Liquido()
        {
            var meta = CriarMetaValida();
            meta.TipoMeta = TipoMeta.Litros;

            var excecao = Record.Exception(
                () => _validacao.Validar(meta, new ObservableCollection<MetaVendedorDto>(), CriarProdutoLiquido()));

            Assert.Null(excecao);
        }

        [Theory]
        [InlineData(TipoMeta.Monetario)]
        [InlineData(TipoMeta.Unidades)]
        public void Validar_Nao_Deve_Restrigir_Produto_Quando_Tipo_Meta_Nao_For_Litros(TipoMeta tipoMeta)
        {
            var meta = CriarMetaValida();
            meta.TipoMeta = tipoMeta;

            var excecao = Record.Exception(
                () => _validacao.Validar(meta, new ObservableCollection<MetaVendedorDto>(), CriarProdutoDiverso()));

            Assert.Null(excecao);
        }

        [Fact]
        public void Validar_Deve_Lancar_Excecao_Quando_Meta_Ja_Existe_Para_Mesmo_Vendedor_Produto_E_Periodicidade()
        {
            var meta = CriarMetaValida();

            var metaExistente = CriarMetaValida();
            metaExistente.Id = "999";

            var metasExistentes = new ObservableCollection<MetaVendedorDto> { metaExistente };

            var excecao = Assert.Throws<ValidacaoDadosException>(
                () => _validacao.Validar(meta, metasExistentes, CriarProdutoLiquido()));

            Assert.Contains(Constantes.MsgMetaVendedorCadastrada, excecao.Message);
        }

        [Fact]
        public void Validar_Nao_Deve_Lancar_Excecao_Quando_Editando_A_Mesma_Meta_Existente()
        {
            var meta = CriarMetaValida();
            var metasExistentes = new ObservableCollection<MetaVendedorDto> { meta };

            var excecao = Record.Exception(
                () => _validacao.Validar(meta, metasExistentes, CriarProdutoLiquido()));

            Assert.Null(excecao);
        }

        [Fact]
        public void Validar_Nao_Deve_Lancar_Excecao_Quando_Todos_Os_Campos_Validos_E_Nenhuma_Duplicidade()
        {
            var meta = CriarMetaValida();

            var excecao = Record.Exception(
                () => _validacao.Validar(meta, new ObservableCollection<MetaVendedorDto>(), CriarProdutoLiquido()));

            Assert.Null(excecao);
        }

        [Fact]
        public void Validar_Deve_Acumular_Multiplos_Erros_Quando_Varios_Campos_Invalidos()
        {
            var meta = new MetaVendedorDto
            {
                Id = "1",
                NomeVendedor = "",
                Produto = null,
                Periodicidade = "",
                TipoMeta = default,
                ValorMeta = 0
            };

            var excecao = Assert.Throws<ValidacaoDadosException>(
                () => _validacao.Validar(meta, new ObservableCollection<MetaVendedorDto>(), produto: null));

            Assert.Contains(Constantes.MsgVendedorNaoPreenchido, excecao.Message);
            Assert.Contains(Constantes.MsgProdutoNaoPreenchido, excecao.Message);
            Assert.Contains(Constantes.MsgValorMetaNaoPreenchida, excecao.Message);
            Assert.Contains(Constantes.MsgTipoMetaNaoPreenchida, excecao.Message);
            Assert.Contains(Constantes.MsgPeriodicidadeNaoPreenchida, excecao.Message);
        }

        [Fact]
        public void Validar_Meta_Repetida_Deve_Adicionar_Erro_Quando_Existe_Meta_Com_Mesmo_Vendedor_Produto_E_Periodicidade()
        {
            var meta = CriarMetaValida();

            var metaExistente = CriarMetaValida();
            metaExistente.Id = "999";

            var metasExistentes = new ObservableCollection<MetaVendedorDto> { metaExistente };
            var erros = new List<string>();

            _validacao.ValidarMetaRepetida(meta, metasExistentes, erros);

            Assert.Contains(Constantes.MsgMetaVendedorCadastrada, erros);
        }

        [Fact]
        public void Validar_Meta_Repetida_Nao_Deve_Adicionar_Erro_Quando_Id_For_O_Mesmo()
        {
            var meta = CriarMetaValida();
            var metasExistentes = new ObservableCollection<MetaVendedorDto> { meta };
            var erros = new List<string>();

            _validacao.ValidarMetaRepetida(meta, metasExistentes, erros);

            Assert.Empty(erros);
        }

        [Fact]
        public void Validar_Meta_Repetida_Nao_Deve_Adicionar_Erro_Quando_Periodicidade_For_Diferente()
        {
            var meta = CriarMetaValida();

            var metaExistente = CriarMetaValida();
            metaExistente.Id = "999";
            metaExistente.Periodicidade = Periodicidade.Semanal.ToString();

            var metasExistentes = new ObservableCollection<MetaVendedorDto> { metaExistente };
            var erros = new List<string>();

            _validacao.ValidarMetaRepetida(meta, metasExistentes, erros);

            Assert.Empty(erros);
        }

        [Fact]
        public void Validar_Meta_Repetida_Nao_Deve_Adicionar_Erro_Quando_Produto_For_Diferente()
        {
            var meta = CriarMetaValida();

            var metaExistente = CriarMetaValida();
            metaExistente.Id = "999";
            metaExistente.Produto = "2";

            var metasExistentes = new ObservableCollection<MetaVendedorDto> { metaExistente };
            var erros = new List<string>();

            _validacao.ValidarMetaRepetida(meta, metasExistentes, erros);

            Assert.Empty(erros);
        }
    }
}