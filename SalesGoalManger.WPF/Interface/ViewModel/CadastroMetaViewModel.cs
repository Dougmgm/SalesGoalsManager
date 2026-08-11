using ProjetoCadastros.Comuns;
using ProjetoCadastros.Extensoes;
using ProjetoCadastros.Extensoes.Exceptions;
using ProjetoCadastros.RegraDeNegocio;
using ProjetoCadastros.RegraDeNegocio.Dto;
using System.Collections.ObjectModel;
using System.Windows;
using static ProjetoCadastros.RegraDeNegocio.ProdutoDto;

namespace ProjetoCadastros.Interface.ViewModel
{
    public class CadastroMetaViewModel : ViewModelBase
    {
        public Action FecharJanela { get; set; }

        public MetaVendedorDto MetaVendedor { get; set; }

        public List<ProdutoDto> ListaProdutos { get; set; }

        ObservableCollection<MetaVendedorDto> _listaMetas;

        private bool _modoEdicao;

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

        public void CarregarDadosProduto()
        {
            ListaProdutos = ObterProdutos();
        }

        private List<ProdutoDto> ObterProdutos()
        {
            return new List<ProdutoDto>
            {
                new ProdutoDto { Id = 1.ToString(), NomeProduto = "Barris", Categoria = CategoriaProduto.Liquido },
                new ProdutoDto { Id = 2.ToString(), NomeProduto = "Garrafas e Latas", Categoria = CategoriaProduto.Liquido },
                new ProdutoDto { Id = 3.ToString(), NomeProduto = "Acessórios e Produtos", Categoria = CategoriaProduto.Diversos }
            };
        }

        public Array ListaPeriodicidades => Enum.GetValues(typeof(Periodicidade));

        public CadastroMetaViewModel(ObservableCollection<MetaVendedorDto> listaMetas)
        {
            MetaVendedor = new MetaVendedorDto();

            CarregarDadosProduto();

            _listaMetas = listaMetas;
            _modoEdicao = false;

            CriarComandos();
        }

        public CadastroMetaViewModel(MetaVendedorDto metaSelecionada, ObservableCollection<MetaVendedorDto> listaMetas)
        {
            MetaVendedor = metaSelecionada; // referência direta, sem cópia

            CarregarDadosProduto();

            _listaMetas = listaMetas;
            _modoEdicao = true;

            ProdutoSelecionado = ListaProdutos.FirstOrDefault(p => p.Id == MetaVendedor.Produto);

            CriarComandos();
        }

        public void CriarComandos()
        {
            _comandos["DuplicarValorMeta"] = new RelayCommand(x => DuplicarValorMeta());
            _comandos["Limpar"] = new RelayCommand(x => Limpar());
            _comandos["Voltar"] = new RelayCommand(x => Voltar());
            _comandos["Salvar"] = new RelayCommand(x => Salvar());

        }

        public void DuplicarValorMeta()
        {
            MetaVendedor.ValorMeta = MetaVendedor.ValorMeta * 2;
        }

        public void Limpar()
        {
            MetaVendedor.NomeVendedor = null;
            MetaVendedor.ValorMeta = 0;
            //MetaVendedor.TipoMeta = null;
            //MetaVendedor.Periodicidade = default;
            //ProdutoSelecionado = null;
        }


        public void Voltar()
        {
            if (MessageBox.Show(Constantes.MsgVoltarTelaInicial, "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                FecharJanela?.Invoke();
        }

        public void Salvar()
        {
            if (!ValidarCampos())
                return;

            if (!_modoEdicao)
            {
                _listaMetas.Add(MetaVendedor);
                MessageBox.Show(Constantes.MsgMetaCadastrada);
            }
            else
                MessageBox.Show(Constantes.MsgMetaEditadaComSucesso);
            
            FecharJanela?.Invoke();
        }

        public bool ValidarCampos()
        {
            try
            {
                ValidarNome();
                ValidarMeta();
                ValidarTipoMeta();

                return true;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        public void ValidarNome()
        {
            if (MetaVendedor.NomeVendedor.IsNullOrEmpty())
                throw new ValidacaoDadosException(Constantes.MsgVendedorNaoPreenchido);
        }

        public void ValidarMeta() 
        {
            if (MetaVendedor.ValorMeta.IsNull() || MetaVendedor.ValorMeta < 1)
                throw new ValidacaoDadosException(Constantes.MsgValorMetaNaoPreenchida);
        }

        public void ValidarTipoMeta()
        {
            if (MetaVendedor.TipoMeta.IsNullOrEmpty())
                throw new ValidacaoDadosException(Constantes.MsgTipoMetaNaoPreenchida);
        }
    }
}
