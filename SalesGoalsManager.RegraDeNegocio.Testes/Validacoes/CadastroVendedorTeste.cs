using SalesGoalsManager.RegraDeNegocio.Dto;
using SalesGoalsManager.RegraDeNegocio.Validacoes;
using System.ComponentModel.DataAnnotations;

namespace SalesGoalsManager.RegraDeNegocio.Testes.Validacoes
{
    public class CadastroVendedorTeste
    {
        private readonly CadastroVendedorValidacao _validacao = new();

        private static VendedorDto CriarVendedorValido()
        {
            return new VendedorDto
            {
                Id = "1",
                NomeVendedor = "João da Silva"
            };
        }

        [Fact]
        public void Validar_Deve_Lancar_Excecao_Quando_Nome_Vendedor_Nao_Preenchido()
        {
            var vendedor = CriarVendedorValido();
            vendedor.NomeVendedor = "";

            var excecao = Assert.Throws<ValidationException>(
                () => _validacao.Validar(vendedor, new List<VendedorDto>()));

            Assert.Contains("Vendedor", excecao.Message);
        }

        [Fact]
        public void Validar_Deve_Lancar_Excecao_Quando_Nome_Vendedor_For_Nulo()
        {
            var vendedor = CriarVendedorValido();
            vendedor.NomeVendedor = null;

            var excecao = Assert.Throws<ValidationException>(
                () => _validacao.Validar(vendedor, new List<VendedorDto>()));

            Assert.Contains("Vendedor", excecao.Message);
        }

        [Fact]
        public void Validar_Deve_Lancar_Excecao_Quando_Existe_Vendedor_Com_Mesmo_Nome()
        {
            var vendedor = CriarVendedorValido();
            vendedor.Id = "1";

            var vendedorExistente = CriarVendedorValido();
            vendedorExistente.Id = "2";
            vendedorExistente.NomeVendedor = vendedor.NomeVendedor;

            var excecao = Assert.Throws<ValidationException>(
                () => _validacao.Validar(vendedor, new List<VendedorDto> { vendedorExistente }));

            Assert.Contains("Já existe um vendedor cadastrado", excecao.Message);
        }

        [Fact]
        public void Validar_Deve_Lancar_Excecao_Quando_Nome_Duplicado_Com_Diferenca_De_Case()
        {
            var vendedor = CriarVendedorValido();
            vendedor.Id = "1";
            vendedor.NomeVendedor = "joão da silva";

            var vendedorExistente = CriarVendedorValido();
            vendedorExistente.Id = "2";
            vendedorExistente.NomeVendedor = "JOÃO DA SILVA";

            var excecao = Assert.Throws<ValidationException>(
                () => _validacao.Validar(vendedor, new List<VendedorDto> { vendedorExistente }));

            Assert.Contains("Já existe um vendedor cadastrado", excecao.Message);
        }

        [Fact]
        public void Validar_Nao_Deve_Lancar_Excecao_Quando_Editando_O_Mesmo_Vendedor_Sem_Mudar_Nome()
        {
            var vendedor = CriarVendedorValido();
            vendedor.Id = "1";

            var listaComEleMesmo = new List<VendedorDto> { vendedor };

            var excecao = Record.Exception(
                () => _validacao.Validar(vendedor, listaComEleMesmo));

            Assert.Null(excecao);
        }

        [Fact]
        public void Validar_Nao_Deve_Lancar_Excecao_Quando_Nomes_Diferentes()
        {
            var vendedor = CriarVendedorValido();
            vendedor.Id = "1";

            var outroVendedor = CriarVendedorValido();
            outroVendedor.Id = "2";
            outroVendedor.NomeVendedor = "Maria Santos";

            var excecao = Record.Exception(
                () => _validacao.Validar(vendedor, new List<VendedorDto> { outroVendedor }));

            Assert.Null(excecao);
        }

        [Fact]
        public void Validar_Nao_Deve_Lancar_Excecao_Quando_Todos_Campos_Validos_E_Nenhuma_Duplicidade()
        {
            var vendedor = CriarVendedorValido();

            var excecao = Record.Exception(
                () => _validacao.Validar(vendedor, new List<VendedorDto>()));

            Assert.Null(excecao);
        }
    }
}
