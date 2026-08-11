using System.Globalization;
using System.Windows.Data;

namespace SalesGoalManger.WPF.Interface.Converters
{
    internal class DecimalMoedaConverter : IValueConverter
    {
        private static readonly CultureInfo PtBr = new CultureInfo("pt-BR");

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal d)
                return d.ToString("N2", PtBr);
            return "0,00";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string texto = value as string ?? "0";

            string digitos = new string(Array.FindAll(texto.ToCharArray(), char.IsDigit));
            if (string.IsNullOrEmpty(digitos))
                digitos = "0";

            decimal valor = decimal.Parse(digitos, PtBr) / 100m;
            return valor;
        }
    }
}
