using SalesGoalManger.WPF.Comuns;

namespace SalesGoalManger.WPF.RegraDeNegocio.Dto
{
    public class MetaVendedorDto : NotifyPropertyChangedBase
    {
        public string Id { get; set; }

        private Periodicidade _periodicidade;
        public Periodicidade Periodicidade
        {
            get => _periodicidade;
            set => SetProperty(ref _periodicidade, value, nameof(Periodicidade));
        }

        public string Produto { get; set; }
        public string ProdutoNome { get; set; }

        private string _vendedor;
        public string Vendedor
        {
            get => _vendedor;
            set => SetProperty(ref _vendedor, value, nameof(Vendedor));
        }

        private TipoMeta _tipoMeta;
        public TipoMeta TipoMeta
        {
            get => _tipoMeta;
            set
            {
                if (SetProperty(ref _tipoMeta, value, nameof(TipoMeta)))
                {
                    if (_tipoMeta != TipoMeta.Monetario)
                        ValorMeta = Math.Truncate(ValorMeta);
                }
            }
        }

        private string _nomeVendedor;
        public string NomeVendedor
        {
            get => _nomeVendedor;
            set => SetProperty(ref _nomeVendedor, value, nameof(NomeVendedor));
        }

        private decimal _valorMeta;
        public decimal ValorMeta
        {
            get => _valorMeta;
            set => SetProperty(ref _valorMeta, value, nameof(ValorMeta));
        }
    }
}