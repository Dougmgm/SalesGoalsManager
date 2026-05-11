using ProjetoCadastros.Comuns;
using ProjetoCadastros.RegraDeNegocio;
using ProjetoCadastros.RegraDeNegocio.Dto;
using System;
using System.Collections.Generic;
using static ProjetoCadastros.RegraDeNegocio.ProdutoDto;

namespace ProjetoCadastros.Interface.ViewModel
{
    public class CadastroMetaViewModel : ViewModelBase
    {
        public MetaVendedorDto MetaVendedor { get; set; }

        public List<ProdutoDto> ListaProdutos { get; set; }

        private ProdutoDto _produtoSelecionado;
        public ProdutoDto ProdutoSelecionado
        {
            get => _produtoSelecionado;
            set
            {
                _produtoSelecionado = value;
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

        private Periodicidade _periodicidadeSelecionada;
        public Periodicidade PeriodicidadeSelecionada
        {
            get => _periodicidadeSelecionada;
            set
            {
                _periodicidadeSelecionada = value;
            }
        }

        public CadastroMetaViewModel()
        {
            MetaVendedor = new MetaVendedorDto();
            CarregarDadosProduto();
            CriarComandos();
        }

        public CadastroMetaViewModel(MetaVendedorDto metaSelecionada) : this()
        {
            MetaVendedor = new MetaVendedorDto();

            MetaVendedor.NomeVendedor = metaSelecionada.NomeVendedor;
            MetaVendedor.Periodicidade = metaSelecionada.Periodicidade;
            MetaVendedor.Produto = metaSelecionada.Produto;
            MetaVendedor.TipoMeta = metaSelecionada.TipoMeta;
            MetaVendedor.ValorMeta = metaSelecionada.ValorMeta;
            CriarComandos();
        }

        public void CriarComandos()
        {
            _comandos["DuplicarMeta"] = new RelayCommand(x => DuplicarMeta());
            _comandos["BuscarMeta"] = new RelayCommand(x => BuscarMeta());
            _comandos["ExcluirMeta"] = new RelayCommand(x => ExcluirMeta());
            _comandos["EditarMeta"] = new RelayCommand(x => EditarMeta());
            _comandos["AdicionarMeta"] = new RelayCommand(x => AdicionarMeta());

        }

        public void DuplicarMeta()
        {
            MetaVendedor.ValorMeta = MetaVendedor.ValorMeta * 2;
        }

        public void BuscarMeta()
        {
           
        }

        public void ExcluirMeta()
        {
           
        }

        public void EditarMeta()
        {
          
        }

        public void AdicionarMeta()
        {

        }
    }
}
