using SalesGoalsManager.WPF.Interface;
using SalesGoalsManager.RegraDeNegocio;
using SalesGoalsManager.RegraDeNegocio.Cadastro;
using SalesGoalsManager.RegraDeNegocio.Comuns;
using SalesGoalsManager.RegraDeNegocio.Consultas;
using SalesGoalsManager.RegraDeNegocio.Extensoes;
using SalesGoalsManager.WPF.Comuns;
using SalesGoalsManager.WPF.RegraDeNegocio.Dto;
using System.Collections.ObjectModel;
using System.Windows;

namespace SalesGoalsManager.WPF.Interface.ViewModel
{
    public class TelaInicialViewModel : ViewModelBase
    {
        public TelaInicialDto Tela { get; set; }

        public ObservableCollection<MetaVendedorDto> ListaMetas { get; set; }

        private ObservableCollection<MetaVendedorDto> _listaFiltrada;
        public ObservableCollection<MetaVendedorDto> ListaFiltrada
        {
            get => _listaFiltrada;
            set => SetProperty(ref _listaFiltrada, value, nameof(ListaFiltrada));
        }

        private string _totalRegistros;
        public string TotalRegistros
        {
            get => _totalRegistros;
            set => SetProperty(ref _totalRegistros, value, nameof(TotalRegistros));
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
            _ = CarregarDados();
            DefinirTotalRegistros();
        }

        public async Task CarregarDados()
        {
            try
            {
                var metaConsulta = ServiceFactory.CriarMetaConsulta(); 

                var metasRegraDeNegocio = await metaConsulta.ListarTodasAsync();

                var metasWpf = metasRegraDeNegocio
                    .Select(MetaVendedorMapper.ParaWpf)
                    .ToList();

                ListaMetas = new ObservableCollection<MetaVendedorDto>(metasWpf);
                ListaFiltrada = new ObservableCollection<MetaVendedorDto>(ListaMetas);

                DefinirTotalRegistros();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar as metas: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

            if (lista is null || lista.Count == 0)
                return;

            TotalRegistros = $"{lista.Count} Registro(s)";
        }

        private async void CadastrarMeta()
        {
            var formCadastrarProduto = new CadastroMeta(ListaMetas);

            formCadastrarProduto.ShowDialog();

            await CarregarDados();
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

        public async void ExcluirMeta()
        {
            if (MetaSelecionada.IsNull())
            {
                MessageBox.Show(Constantes.MsgSelecionarMeta);
                return;
            }

            if (MessageBox.Show(Constantes.MsgExcluirMeta, "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var metaConsulta = ServiceFactory.CriarMetaCadastro(); 

                await metaConsulta.ExcluirAsync(MetaSelecionada.Id);

                ListaMetas.Remove(MetaSelecionada);
                ListaFiltrada.Remove(MetaSelecionada);
                DefinirTotalRegistros();
            }
        }

        public async void EditarMeta()
        {
            if (MetaSelecionada.IsNull())
            {
                MessageBox.Show(Constantes.MsgSelecionarMeta);
                return;
            }

            var formCadastro = new CadastroMeta(MetaSelecionada, ListaMetas);

            formCadastro.ShowDialog();

            await CarregarDados();
        }
    }
}