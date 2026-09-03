using SalesGoalManger.WPF.Comuns;

namespace SalesGoalManger.WPF.RegraDeNegocio.Dto
{
    public class TelaInicialDto : ViewModelBase
    {
        private string _textoDeBusca;
        public string TextoDeBusca
        {
            get => _textoDeBusca;
            set => SetProperty(ref _textoDeBusca, value, nameof(TextoDeBusca));
        }

    }
}
