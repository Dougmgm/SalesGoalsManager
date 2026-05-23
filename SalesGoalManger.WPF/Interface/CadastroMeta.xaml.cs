using ProjetoCadastros.Interface.ViewModel;
using ProjetoCadastros.RegraDeNegocio.Dto;
using System.Windows;

namespace ProjetoCadastros.Interface
{
    /// <summary>
    /// Lógica interna para CadastroMeta.xaml
    /// </summary>
    public partial class CadastroMeta : Window
    {
        public CadastroMeta()
        {
            InitializeComponent();
            DataContext = new CadastroMetaViewModel();
        }

        public CadastroMeta(MetaVendedorDto metaSelecionada) : this()
        {
            DataContext = new CadastroMetaViewModel(metaSelecionada);
        }
    }
}
