using SalesGoalManager.RegraDeNegocio.Dto;
using System.Globalization;
using System.Windows.Data;

namespace SalesGoalManger.WPF.Interface.Converters
{
    internal class ValorMetaFormatoConverter : IMultiValueConverter
    {
        private static readonly CultureInfo PtBr = new CultureInfo("pt-BR");
        private TipoMeta _ultimoTipoMeta;

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            decimal valor = values[0] is decimal d ? d : 0;

            if (values[1] is TipoMeta tipoMeta)
                _ultimoTipoMeta = tipoMeta;

            if (_ultimoTipoMeta == TipoMeta.Monetario)
                return valor.ToString("N2", PtBr);

            return ((long)valor).ToString("N0", PtBr);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            string texto = value as string ?? "0";
            string digitos = new string(texto.Where(char.IsDigit).ToArray());

            if (string.IsNullOrEmpty(digitos))
                digitos = "0";

            decimal valor = _ultimoTipoMeta == TipoMeta.Monetario ? decimal.Parse(digitos, PtBr) / 100m : decimal.Parse(digitos, PtBr);

            return new object[] { valor, Binding.DoNothing };
        }
    }
}