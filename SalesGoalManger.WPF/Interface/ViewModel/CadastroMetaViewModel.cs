using SalesGoalManager.RegraDeNegocio;
using SalesGoalManager.RegraDeNegocio.Cadastros;
using SalesGoalManager.RegraDeNegocio.Comuns;
using SalesGoalManager.RegraDeNegocio.Consultas;
using SalesGoalManager.RegraDeNegocio.Dto;
using SalesGoalManger.WPF.Comuns;
using WpfDto = SalesGoalManger.WPF.RegraDeNegocio.Dto;
using System.Collections.ObjectModel;
using System.Windows;

namespace SalesGoalManger.WPF.Interface.ViewModel
{
    public class CadastroMetaViewModel : ViewModelBase
    {
        public Action FecharJanela { get; set; }

        public WpfDto.MetaVendedorDto MetaVendedor { get; set; }

        private WpfDto.MetaVendedorDto _metaOriginal;

        private bool _modoEdicao;

        private readonly MetaCadastro _metaCadastro = Fabrica.CriarMetaCadastro();
        private readonly ProdutoConsulta _produtoConsulta = Fabrica.CriarProdutoConsulta();
        private readonly VendedorConsulta _vendedorConsulta = Fabrica.CriarVendedorConsulta();

        private List<ProdutoDto> _listaProdutos;
        public List<ProdutoDto> ListaProdutos
        {
            get => _listaProdutos;
            set => SetProperty(ref _listaProdutos, value, nameof(ListaProdutos));
        }

        private List<VendedorDto> _listaVendedores;
        public List<VendedorDto> ListaVendedores
        {
            get => _listaVendedores;
            set => SetProperty(ref _listaVendedores, value, nameof(ListaVendedores));
        }

        private ProdutoDto _produtoSelecionado;
        public ProdutoDto ProdutoSelecionado
        {
            get => _produtoSelecionado;
            set
            {
                if (SetProperty(ref _produtoSelecionado, value, nameof(ProdutoSelecionado)))
                {
                    MetaVendedor.Produto = value?.Id;
                    MetaVendedor.ProdutoNome = value?.NomeProduto;
                }
            }
        }

        private VendedorDto _vendedorSelecionado;
        public VendedorDto VendedorSelecionado
        {
            get => _vendedorSelecionado;
            set
            {
                if (SetProperty(ref _vendedorSelecionado, value, nameof(VendedorSelecionado)))
                {
                    MetaVendedor.Vendedor = value?.Id;
                    MetaVendedor.NomeVendedor = value?.NomeVendedor;
                }
            }
        }

        public Array ListaPeriodicidades => Enum.GetValues(typeof(WpfDto.Periodicidade));

        public CadastroMetaViewModel(ObservableCollection<WpfDto.MetaVendedorDto> listaMetas)
        {
            MetaVendedor = new WpfDto.MetaVendedorDto();
            _modoEdicao = false;

            CriarComandos();
            _ = CarregarDadosIniciaisAsync();
        }

        public CadastroMetaViewModel(WpfDto.MetaVendedorDto metaSelecionada, ObservableCollection<WpfDto.MetaVendedorDto> listaMetas)
        {
            _metaOriginal = metaSelecionada;

            MetaVendedor = new WpfDto.MetaVendedorDto
            {
                Id = metaSelecionada.Id,
                Vendedor = metaSelecionada.Vendedor,
                NomeVendedor = metaSelecionada.NomeVendedor,
                Produto = metaSelecionada.Produto,
                ProdutoNome = metaSelecionada.ProdutoNome,
                Periodicidade = metaSelecionada.Periodicidade,
                ValorMeta = metaSelecionada.ValorMeta,
                TipoMeta = metaSelecionada.TipoMeta
            };

            _modoEdicao = true;

            CriarComandos();
            _ = CarregarDadosIniciaisAsync(preSelecionar: true);
        }

        private async Task CarregarDadosIniciaisAsync(bool preSelecionar = false)
        {
            try
            {
                ListaProdutos = await _produtoConsulta.ListarTodosAsync();
                ListaVendedores = await _vendedorConsulta.ListarTodosAsync();

                if (preSelecionar)
                {
                    ProdutoSelecionado = ListaProdutos.FirstOrDefault(p => p.Id == MetaVendedor.Produto);
                    VendedorSelecionado = ListaVendedores.FirstOrDefault(v => v.Id == MetaVendedor.Vendedor);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar dados: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void CriarComandos()
        {
            _comandos["DuplicarValorMeta"] = new RelayCommand(x => DuplicarValorMeta());
            _comandos["Limpar"] = new RelayCommand(x => Limpar());
            _comandos["Voltar"] = new RelayCommand(x => Voltar());
            _comandos["Salvar"] = new RelayCommand(async x => await SalvarAsync());
        }

        public void DuplicarValorMeta()
        {
            MetaVendedor.ValorMeta = MetaVendedor.ValorMeta * 2;
        }

        public void Limpar()
        {
            MetaVendedor.NomeVendedor = null;
            MetaVendedor.ValorMeta = 0;
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
                var metaParaSalvar = MetaVendedorMapper.ParaRegraDeNegocio(MetaVendedor);

                await _metaCadastro.SalvarAsync(metaParaSalvar, ProdutoSelecionado);

                MessageBox.Show(_modoEdicao ? Constantes.MsgMetaEditadaComSucesso : Constantes.MsgMetaCadastrada);
                FecharJanela?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}