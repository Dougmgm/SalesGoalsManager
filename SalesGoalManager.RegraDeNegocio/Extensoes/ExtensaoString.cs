using System.Globalization;

namespace SalesGoalManager.RegraDeNegocio.Extensoes
{
    public class ExtensaoString
    {
        public static decimal ConverterParaDecimal(string valorMonetario)
        {
            if (string.IsNullOrWhiteSpace(valorMonetario))
                throw new ArgumentException("Valor monetário inválido.");

            string valorLimpo = valorMonetario
                 .Replace("R$", "")
                 .Trim();

            return decimal.Parse(valorLimpo, new CultureInfo("pt-BR"));
        }
    }
}
