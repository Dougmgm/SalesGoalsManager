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

        public string Produto { get; set; } //FK

        public string ProdutoNome { get; set; }

        private string _tipoMeta;
        public string TipoMeta
        {
            get => _tipoMeta;
            set => SetProperty(ref _tipoMeta, value, nameof(TipoMeta));
        }

        private string _nomeVendedor;
        public string NomeVendedor //FK
        {
            get => _nomeVendedor;
            set => SetProperty(ref _nomeVendedor, value, nameof(NomeVendedor));
        }

        private decimal _valorMeta;
        public decimal ValorMeta //FK
        {
            get => _valorMeta;
            set => SetProperty(ref _valorMeta, value, nameof(ValorMeta));
        }
    }
}
