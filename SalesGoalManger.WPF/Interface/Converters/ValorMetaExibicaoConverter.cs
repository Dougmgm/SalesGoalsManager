using SalesGoalManager.RegraDeNegocio.Dto;
using System.Globalization;
using System.Windows.Data;

namespace SalesGoalManger.WPF.Interface.Converters
{
    internal class ValorMetaExibicaoConverter : IMultiValueConverter
    {
        private static readonly CultureInfo PtBr = new CultureInfo("pt-BR");

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is not decimal valor)
                return string.Empty;

            if (values[1] is TipoMeta tipoMeta && tipoMeta == TipoMeta.Monetario)
                return valor.ToString("C2", PtBr); 

            return ((long)valor).ToString("N0", PtBr);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotSupportedException();
    }
}
