using ProjetoCadastros.Interface.ViewModel;
using ProjetoCadastros.RegraDeNegocio.Dto;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace ProjetoCadastros.Interface
{
    /// <summary>
    /// Lógica interna para CadastroMeta.xaml
    /// </summary>
    public partial class CadastroMeta : Window
    {
        public CadastroMeta(ObservableCollection<MetaVendedorDto> listaMetas)
        {
            InitializeComponent();

            var viewModel = new CadastroMetaViewModel(listaMetas);

            viewModel.FecharJanela = Close;

            DataContext = viewModel;
        }

        public CadastroMeta(MetaVendedorDto metaSelecionada, ObservableCollection<MetaVendedorDto> listaMetas)
        {
            InitializeComponent();

            var viewModel = new CadastroMetaViewModel(metaSelecionada, listaMetas);

            viewModel.FecharJanela = Close;

            DataContext = viewModel;
        }

        private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(char.IsDigit);
        }
    }
}
