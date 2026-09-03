using SalesGoalManager.RegraDeNegocio.Comuns;
using SalesGoalManager.RegraDeNegocio.Extensoes;
using SalesGoalManager.RegraDeNegocio.Validacoes;
using SalesGoalManger.WPF.Comuns;
using System.Collections.ObjectModel;
using System.Windows;
using static SalesGoalManger.WPF.RegraDeNegocio.Dto.ProdutoDto;
using MetaVendedorDto = SalesGoalManger.WPF.RegraDeNegocio.Dto.MetaVendedorDto;
using Periodicidade = SalesGoalManger.WPF.RegraDeNegocio.Dto.Periodicidade;
using ProdutoDto = SalesGoalManger.WPF.RegraDeNegocio.Dto.ProdutoDto;

namespace SalesGoalManger.WPF.Interface.ViewModel
{
    public class CadastroMetaViewModel : ViewModelBase
    {
        public Action FecharJanela { get; set; }

        public MetaVendedorDto MetaVendedor { get; set; }

        private MetaVendedorDto _metaOriginal;

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
            _metaOriginal = metaSelecionada;

            MetaVendedor = new MetaVendedorDto
            {
                Id = metaSelecionada.Id,
                NomeVendedor = metaSelecionada.NomeVendedor,
                Produto = metaSelecionada.Produto,
                ProdutoNome = metaSelecionada.ProdutoNome,
                Periodicidade = metaSelecionada.Periodicidade,
                ValorMeta = metaSelecionada.ValorMeta,
                TipoMeta = metaSelecionada.TipoMeta
            };

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
            {
                _metaOriginal.NomeVendedor = MetaVendedor.NomeVendedor;
                _metaOriginal.Produto = MetaVendedor.Produto;
                _metaOriginal.ProdutoNome = MetaVendedor.ProdutoNome;
                _metaOriginal.Periodicidade = MetaVendedor.Periodicidade;
                _metaOriginal.ValorMeta = MetaVendedor.ValorMeta;
                _metaOriginal.TipoMeta = MetaVendedor.TipoMeta;

                MessageBox.Show(Constantes.MsgMetaEditadaComSucesso);
            }

            FecharJanela?.Invoke();
        }

        public bool ValidarCampos()
        {
            try
            {
                var validacao = new MetaVendedorValidacao();

                var metaDto = ConverterMeta();

                var produtoDto = ConverterProduto();

                var metasExistentes = ConverterListaMetas();

                validacao.Validar(metaDto, metasExistentes, produtoDto);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        private SalesGoalManager.RegraDeNegocio.Dto.MetaVendedorDto ConverterMeta()
        {
            return new SalesGoalManager.RegraDeNegocio.Dto.MetaVendedorDto
            {
                Id = MetaVendedor.Id,
                NomeVendedor = MetaVendedor.NomeVendedor,
                Produto = MetaVendedor.Produto,
                Periodicidade = MetaVendedor.Periodicidade.ToString(),
                ValorMeta = MetaVendedor.ValorMeta,
                TipoMeta = MetaVendedor.TipoMeta
            };
        }

        private SalesGoalManager.RegraDeNegocio.Dto.ProdutoDto ConverterProduto()

        {
            if (ProdutoSelecionado.IsNull())
                return null;

            return new SalesGoalManager.RegraDeNegocio.Dto.ProdutoDto
            {
                Id = ProdutoSelecionado.Id,
                NomeProduto = ProdutoSelecionado.NomeProduto,
                Categoria = (SalesGoalManager.RegraDeNegocio.Dto.ProdutoDto.CategoriaProduto)ProdutoSelecionado.Categoria
            };
        }

        private ObservableCollection<SalesGoalManager.RegraDeNegocio.Dto.MetaVendedorDto> ConverterListaMetas()
        {
            return new ObservableCollection<SalesGoalManager.RegraDeNegocio.Dto.MetaVendedorDto>(
                _listaMetas.Select(meta => new SalesGoalManager.RegraDeNegocio.Dto.MetaVendedorDto
                {
                    Id = meta.Id,
                    NomeVendedor = meta.NomeVendedor,
                    Produto = meta.Produto,
                    Periodicidade = meta.Periodicidade.ToString(),
                    ValorMeta = meta.ValorMeta,
                    TipoMeta = meta.TipoMeta
                })
            );
        }
    }
}
