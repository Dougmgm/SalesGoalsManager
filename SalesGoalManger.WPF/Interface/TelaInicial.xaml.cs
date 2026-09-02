using SalesGoalManger.WPF.Interface.ViewModel;
using System.Windows;

namespace ProjetoCadastros.Interface
{
    /// <summary>
    /// Lógica interna para TelaInicial.xaml
    /// </summary>
    public partial class TelaInicial : Window
    {
        public TelaInicial()
        {
            InitializeComponent();
            this.DataContext = new TelaInicialViewModel();
        }
    }
}
