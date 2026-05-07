using ProjetoCadastros.Comuns;
using ProjetoCadastros.Extensoes;
using ProjetoCadastros.RegraDeNegocio.Dto;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ProjetoCadastros.Interface.ViewModel
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
        }

        private void CarregarMockMetas()
        {
            ListaMetas = new ObservableCollection<MetaVendedorDto>
            {
                new MetaVendedorDto
                {
                    NomeVendedor = "João da Silva",
                    Periodicidade = Periodicidade.Mensal,
                    Produto = "Barris",
                    ValorMeta = 1500,
                    TipoMeta = "Unidades"
                },
                new MetaVendedorDto
                {
                    NomeVendedor = "Maria Santos",
                    Periodicidade = Periodicidade.Semanal,
                    Produto = "Produto B",
                    TipoMeta = "Quantidade",
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
            _comandos["AdicionarMeta"] = new RelayCommand(x => AdicionarMeta());
            _comandos["CadastrarMeta"] = new RelayCommand(x => CadastrarMeta());
        }

        private void CadastrarMeta()
        {
            var formCadastrarProduto = new CadastroMeta();
            formCadastrarProduto.ShowDialog();
        }

        public void LimparBusca()
        {
            Tela.TextoDeBusca = "";
            ListaFiltrada = new ObservableCollection<MetaVendedorDto>(ListaMetas);
        }

        public void BuscarMeta()
        {
            if (Tela.TextoDeBusca.IsNullOrEmpty())
                return;

            string termo = Tela.TextoDeBusca.Trim().ToUpper();
            var filtrado = ListaMetas.Where(x =>
                x.NomeVendedor.ToUpper().Contains(termo) ||
                x.Produto.ToUpper().Contains(termo) ||
                x.TipoMeta.ToUpper().Contains(termo)).ToList();
            ListaFiltrada = new ObservableCollection<MetaVendedorDto>(filtrado);
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
            }
        }

        public void EditarMeta()
        {
            if (MetaSelecionada.IsNull())
            {
                MessageBox.Show(Constantes.MsgSelecionarMeta);
                return;
            }

            var formCadastro = new CadastroMeta(MetaSelecionada);

            formCadastro.ShowDialog();
            ListaFiltrada = new ObservableCollection<MetaVendedorDto>(ListaMetas);
        }
        public void AdicionarMeta() 
        {
            throw new NotImplementedException();
        }
    }
}