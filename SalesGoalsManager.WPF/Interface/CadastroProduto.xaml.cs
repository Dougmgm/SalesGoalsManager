using SalesGoalsManager.RegraDeNegocio.Dto;
using SalesGoalsManager.WPF.Interface.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;

namespace SalesGoalsManager.WPF.Interface
{
    /// <summary>
    /// Lógica interna para CadastroProduto.xaml
    /// </summary>
    public partial class CadastroProduto : Window
    {
        public CadastroProduto() : this(new CadastroProdutoViewModel())
        {
        }

        public CadastroProduto(ProdutoDto produtoSelecionado, ObservableCollection<ProdutoDto> listaProdutos)
            : this(new CadastroProdutoViewModel(produtoSelecionado, listaProdutos))
        {
        }

        private CadastroProduto(CadastroProdutoViewModel viewModel)
        {
            InitializeComponent();
            viewModel.FecharJanela = Close;
            DataContext = viewModel;
        }
    }
}
