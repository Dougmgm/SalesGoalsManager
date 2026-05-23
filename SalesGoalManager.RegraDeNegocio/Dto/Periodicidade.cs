using System.ComponentModel;

namespace SalesGoalManager.RegraDeNegocio.Dto
{
    public enum Periodicidade
    {
        [Description("Diária")]
        Diaria = 1,

        [Description("Semanal")]
        Semanal = 2,

        [Description("Mensal")]
        Mensal = 3
    }
}
