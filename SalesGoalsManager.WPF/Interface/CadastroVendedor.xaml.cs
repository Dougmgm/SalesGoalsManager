using SalesGoalsManager.RegraDeNegocio.Dto;
using SalesGoalsManager.WPF.Interface.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;

namespace SalesGoalsManager.WPF.Interface
{
    /// <summary>
    /// Lógica interna para CadastroVendedor.xaml
    /// </summary>
    public partial class CadastroVendedor : Window
    {
        public CadastroVendedor()
            : this(new CadastroVendedorViewModel())
        {
        }

        public CadastroVendedor(VendedorDto vendedorSelecionado, ObservableCollection<VendedorDto> listaVendedores)
            : this(new CadastroVendedorViewModel(vendedorSelecionado, listaVendedores))
        {
        }

        private CadastroVendedor(CadastroVendedorViewModel viewModel)
        {
            InitializeComponent();
            viewModel.FecharJanela = Close;
            DataContext = viewModel;
        }
    }
}
