using SalesGoalManager.RegraDeNegocio.Dto;

namespace ProjetoCadastros.RegraDeNegocio.Dto
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
