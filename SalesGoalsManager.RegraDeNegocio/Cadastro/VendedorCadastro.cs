using SalesGoalsManager.RegraDeNegocio.Dto;
using SalesGoalsManager.RegraDeNegocio.Interfaces;
using SalesGoalsManager.RegraDeNegocio.Validacoes;

namespace SalesGoalsManager.RegraDeNegocio.Cadastro
{
    public class VendedorCadastro
    {
        private readonly IVendedorRepositorio _vendedorRepository;
        private readonly CadastroVendedorValidacao _validator;

        public VendedorCadastro(IVendedorRepositorio vendedorRepository, CadastroVendedorValidacao validator)
        {
            _vendedorRepository = vendedorRepository;
            _validator = validator;
        }

        public async Task SalvarAsync(VendedorDto vendedor)
        {
            var vendedoresExistentes = await _vendedorRepository.ObterTodosAsync();
            _validator.Validar(vendedor, vendedoresExistentes);

            bool ehEdicao = !string.IsNullOrEmpty(vendedor.Id);

            if (ehEdicao)
                await _vendedorRepository.AtualizarAsync(vendedor);
            else
                await _vendedorRepository.AdicionarAsync(vendedor);
        }

        public async Task ExcluirAsync(string id)
        {
            await _vendedorRepository.RemoverAsync(id);
        }
    }
}
