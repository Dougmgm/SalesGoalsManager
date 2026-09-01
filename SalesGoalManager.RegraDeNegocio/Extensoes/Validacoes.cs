namespace SalesGoalManager.RegraDeNegocio.Extensoes
{
    public static class Validacoes
    {
        public static bool IsNull(this object @object)
        {
            return @object == null;
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNullOrZero(this int? valor)
        {
            if (valor == null)
                return true;

            return valor == 0;
        }
    }
}
