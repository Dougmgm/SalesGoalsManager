using System.ComponentModel;
using System.Reflection;
using System.Xml.Serialization;

namespace SalesGoalManager.RegraDeNegocio.Extensoes
{
    public static class ExtensaoEnum
    {

        public static bool IsEmpty<T>(this T enumerationValue)
        where T : struct, Enum
        {
            return Convert.ToInt32(enumerationValue) == 0;
        }

        public static M ParseTo<M>(this Enum valor, string mensagemEsperada = "")
        {
            if (valor == null)
                throw new ArgumentNullException(nameof(valor));

            var mensagem = mensagemEsperada;
            if (String.IsNullOrEmpty(mensagem))
                mensagem = String.Format("Erro: informação: \"{0}\" é inválido", valor);

            try
            {
                var entrada = Enum.GetName(GetNonNullableModelType(valor.GetType()), valor);
                var saida = GetNonNullableModelType(typeof(M));

                return (M)Enum.Parse(saida, entrada, true);
            }
            catch
            {
                throw new ApplicationException(mensagem);
            }
        }

        public static M ParseTo<M>(this Enum valor, M padrao)
        {
            if (valor == null)
                return padrao;

            try
            {
                return valor.ParseTo<M>();
            }
            catch
            {
                return padrao;
            }
        }

        private static Type GetNonNullableModelType(Type propertyType)
        {
            Type propertyTypeA = propertyType;
            Type underlyingType = Nullable.GetUnderlyingType(propertyTypeA);
            if (underlyingType != null)
            {
                propertyTypeA = underlyingType;
            }
            return propertyTypeA;
        }


        public static string GetDescription<T>(this T enumerationValue) where T : struct
        {
            var type = enumerationValue.GetType();

            if (!type.IsEnum)
                throw new ArgumentException($"{nameof(enumerationValue)} must be of Enum type", nameof(enumerationValue));

            var memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo.Length > 0)
            {
                var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }

            return enumerationValue.ToString();
        }
    }
}
