using SalesGoalsManager.WPF.Interface.ViewModel;
using System.Windows;

namespace SalesGoalsManager.WPF.Interface
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
