using SalesGoalsManager.RegraDeNegocio;
using SalesGoalsManager.RegraDeNegocio.Cadastro;
using SalesGoalsManager.RegraDeNegocio.Comuns;
using SalesGoalsManager.RegraDeNegocio.Dto;
using SalesGoalsManager.WPF.Comuns;
using System.Collections.ObjectModel;
using System.Windows;

namespace SalesGoalsManager.WPF.Interface.ViewModel
{
    public class CadastroVendedorViewModel : ViewModelBase
    {
        public Action FecharJanela { get; set; }

        public VendedorDto Vendedor { get; set; }

        private VendedorDto _vendedorOriginal;

        private bool _modoEdicao;

        private readonly VendedorCadastro _vendedorService = ServiceFactory.CreateVendedorService();

        public CadastroVendedorViewModel()
        {
            Vendedor = new VendedorDto();
            _modoEdicao = false;

            CriarComandos();
        }

        public CadastroVendedorViewModel(VendedorDto vendedorSelecionado, ObservableCollection<VendedorDto> listaVendedores)
        {
            _vendedorOriginal = vendedorSelecionado;

            Vendedor = new VendedorDto
            {
                Id = vendedorSelecionado.Id,
                NomeVendedor = vendedorSelecionado.NomeVendedor
            };

            _modoEdicao = true;

            CriarComandos();
        }

        public void CriarComandos()
        {   
            _comandos["Voltar"] = new RelayCommand(x => Voltar());
            _comandos["Salvar"] = new RelayCommand(async x => await SalvarAsync());
        }

        public void Voltar()
        {
            if (MessageBox.Show(Constantes.MsgVoltarTelaInicial, "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                FecharJanela?.Invoke();
        }

        public async Task SalvarAsync()
        {
            try
            {
                await _vendedorService.SalvarAsync(Vendedor);

                MessageBox.Show(_modoEdicao ? "Vendedor editado com sucesso." : "Vendedor cadastrado com sucesso."); //COLOCAR CONSTANTES

                FecharJanela?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
