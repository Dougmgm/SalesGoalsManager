using SalesGoalManger.WPF.Interface.ViewModel;
using SalesGoalManger.WPF.RegraDeNegocio.Dto;
using System.Collections.ObjectModel;
using System.Windows;

namespace ProjetoCadastros.Interface
{
    /// <summary>
    /// Lógica interna para CadastroMeta.xaml
    /// </summary>
    public partial class CadastroMeta : Window
    {
        public CadastroMeta(ObservableCollection<MetaVendedorDto> listaMetas)
        : this(new CadastroMetaViewModel(listaMetas))
        {
        }

        public CadastroMeta(MetaVendedorDto metaSelecionada, ObservableCollection<MetaVendedorDto> listaMetas)
            : this(new CadastroMetaViewModel(metaSelecionada, listaMetas))
        {
        }

        private CadastroMeta(CadastroMetaViewModel viewModel)
        {
            InitializeComponent();

            viewModel.FecharJanela = Close;

            DataContext = viewModel;
        }

        private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(char.IsDigit);
        }
    }
}
