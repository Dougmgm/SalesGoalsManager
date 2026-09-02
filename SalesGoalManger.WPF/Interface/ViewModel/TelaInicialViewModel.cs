using ProjetoCadastros.Interface;
using SalesGoalManager.RegraDeNegocio.Comuns;
using SalesGoalManager.RegraDeNegocio.Dto;
using SalesGoalManager.RegraDeNegocio.Extensoes;
using SalesGoalManger.WPF.Comuns;
using SalesGoalManger.WPF.RegraDeNegocio.Dto;
using System.Collections.ObjectModel;
using System.Windows;
using MetaVendedorDto = SalesGoalManger.WPF.RegraDeNegocio.Dto.MetaVendedorDto;
using Periodicidade = SalesGoalManger.WPF.RegraDeNegocio.Dto.Periodicidade;

namespace SalesGoalManger.WPF.Interface.ViewModel
{
    public class TelaInicialViewModel : ViewModelBase
    {
        public TelaInicialDto Tela { get; set; }
        public ObservableCollection<MetaVendedorDto> ListaMetas { get; set; }

        private ObservableCollection<MetaVendedorDto> _listaFiltrada;

        private string _totalRegistros;

        public string TotalRegistros
        {
            get => _totalRegistros;
            set => SetProperty(ref _totalRegistros, value, nameof(TotalRegistros));
        }

        public ObservableCollection<MetaVendedorDto> ListaFiltrada
        {
            get => _listaFiltrada;
            set => SetProperty(ref _listaFiltrada, value, nameof(ListaFiltrada));
        }

        private MetaVendedorDto _metaSelecionada;
        public MetaVendedorDto MetaSelecionada
        {
            get => _metaSelecionada;
            set => SetProperty(ref _metaSelecionada, value, nameof(MetaSelecionada));
        }

        public TelaInicialViewModel()
        {
            Tela = new TelaInicialDto();
            CriarComandos();
            CarregarMockMetas();
            DefinirTotalRegistros();
        }

        private void CarregarMockMetas()
        {
            ListaMetas = new ObservableCollection<MetaVendedorDto>
            {
                new MetaVendedorDto
                {
                    Id = "1",
                    NomeVendedor = "João da Silva",
                    Periodicidade = Periodicidade.Mensal,
                    Produto = "1",
                    ProdutoNome = "Barris",
                    ValorMeta = 1500,
                    TipoMeta = TipoMeta.Unidades
                },
                new MetaVendedorDto
                {
                    Id = "2",
                    NomeVendedor = "Maria Santos",
                    Periodicidade = Periodicidade.Semanal,
                    Produto = "2",
                    ProdutoNome = "Produto B",
                    TipoMeta = TipoMeta.Litros,
                    ValorMeta = 3000
                }
            };

            ListaFiltrada = new ObservableCollection<MetaVendedorDto>(ListaMetas);
        }

        public void CriarComandos()
        {
            _comandos["LimparBusca"] = new RelayCommand(x => LimparBusca());
            _comandos["BuscarMeta"] = new RelayCommand(x => BuscarMeta());
            _comandos["ExcluirMeta"] = new RelayCommand(x => ExcluirMeta());
            _comandos["EditarMeta"] = new RelayCommand(x => EditarMeta());
            _comandos["CadastrarMeta"] = new RelayCommand(x => CadastrarMeta());
        }

        public void DefinirTotalRegistros(ObservableCollection<MetaVendedorDto> listaFiltrada = null)
        {
            var lista = listaFiltrada ?? ListaMetas;

            TotalRegistros = $"{lista.Count} Registro(s)";
        }

        private void CadastrarMeta()
        {
            var formCadastrarProduto = new CadastroMeta(ListaMetas);

            formCadastrarProduto.ShowDialog();

            ListaFiltrada = new ObservableCollection<MetaVendedorDto>(ListaMetas);

            DefinirTotalRegistros();
        }

        public void LimparBusca()
        {
            Tela.TextoDeBusca = "";

            ListaFiltrada = new ObservableCollection<MetaVendedorDto>(ListaMetas);

            DefinirTotalRegistros();
        }

        public void BuscarMeta()
        {
            if (Tela.TextoDeBusca.IsNullOrEmpty())
                return;

            string termo = Tela.TextoDeBusca.Trim().ToUpper();

            var filtrado = ListaMetas.Where(x =>
                x.NomeVendedor.ToUpper().Contains(termo) ||
                x.ProdutoNome.ToUpper().Contains(termo) ||
                x.TipoMeta.ToString().ToUpper().Contains(termo)).ToList();

            ListaFiltrada = new ObservableCollection<MetaVendedorDto>(filtrado);

            DefinirTotalRegistros(ListaFiltrada);
        }

        public void ExcluirMeta()
        {
            if (MetaSelecionada.IsNull())
            {
                MessageBox.Show(Constantes.MsgSelecionarMeta);
                return;
            }

            if (MessageBox.Show(Constantes.MsgExcluirMeta, "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                ListaMetas.Remove(MetaSelecionada);
                ListaFiltrada.Remove(MetaSelecionada);
                DefinirTotalRegistros();
            }
        }

        public void EditarMeta()
        {
            if (MetaSelecionada.IsNull())
            {
                MessageBox.Show(Constantes.MsgSelecionarMeta);
                return;
            }

            var formCadastro = new CadastroMeta(MetaSelecionada, ListaMetas);

            formCadastro.ShowDialog();

            ListaFiltrada = new ObservableCollection<MetaVendedorDto>(ListaMetas);

            DefinirTotalRegistros();
        }
    }
}