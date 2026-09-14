using SalesGoalsManager.RegraDeNegocio;
using SalesGoalsManager.RegraDeNegocio.Cadastro;
using SalesGoalsManager.RegraDeNegocio.Comuns;
using SalesGoalsManager.RegraDeNegocio.Dto;
using SalesGoalsManager.WPF.Comuns;
using System.Collections.ObjectModel;
using System.Windows;

namespace SalesGoalsManager.WPF.Interface.ViewModel
{
    public class CadastroProdutoViewModel : ViewModelBase
    {
        public Action FecharJanela { get; set; }

        public ProdutoDto Produto { get; set; }

        public Array ListaCategorias => Enum.GetValues(typeof(ProdutoDto.CategoriaProduto));

        private ProdutoDto _produtoOriginal;

        private bool _modoEdicao;

        private readonly ProdutoCadastro _produtoService = ServiceFactory.CreateProdutoService();

        public CadastroProdutoViewModel()
        {
            Produto = new ProdutoDto();
            _modoEdicao = false;

            CriarComandos();
        }

        public CadastroProdutoViewModel(ProdutoDto produtoSelecionado, ObservableCollection<ProdutoDto> listaProdutos)
        {
            _produtoOriginal = produtoSelecionado;

            Produto = new ProdutoDto
            {
                Id = produtoSelecionado.Id,
                NomeProduto = produtoSelecionado.NomeProduto,
                Categoria = produtoSelecionado.Categoria
            };

            _modoEdicao = true;

            CriarComandos();
        }

        public void CriarComandos()
        {
            _comandos["Voltar"] = new RelayCommand(x => Voltar());
            _comandos["Salvar"] = new RelayCommand(async x => await SalvarAsync());
        }

        public void Limpar()
        {
            Produto.NomeProduto = null;
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
                await _produtoService.SalvarAsync(Produto);

                MessageBox.Show(_modoEdicao  ? "Produto editado com sucesso." : "Produto cadastrado com sucesso.");

                FecharJanela?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
