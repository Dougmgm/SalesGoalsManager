using System.ComponentModel;

namespace SalesGoalsManager.WPF.RegraDeNegocio.Dto
{
    public enum TipoMeta
    {
        [Description("Monetário (R$)")]
        Monetario = 1,
        [Description("Litros (L)")]
        Litros = 2,
        [Description("Unidades (UN)")]
        Unidades = 3
    }
}