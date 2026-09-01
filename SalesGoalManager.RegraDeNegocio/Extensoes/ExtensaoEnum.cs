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

        /// <summary>
        /// Converte um valor de um Enum para outro Enum
        /// </summary>
        /// <typeparam name="M">Enum de saída</typeparam>
        /// <param name="mensagemEsperada">Mensagem padronizada em caso de não se encontrar um valor equivalente</param>
        /// <returns>Valor do Enum de saída</returns>
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

        /// <summary>
        /// Retornar o valor do enumerador definido na classe como XmlEnum
        /// Ex.:  [XmlEnum("1001")]
        ///       Item1001,
        /// </summary>
        public static String ToStringXmlValue(this Enum nType)
        {
            if (nType == null)
                throw new ArgumentNullException(nameof(nType));

            Type oSystype = nType.GetType();
            string strName = System.Enum.GetName(oSystype, nType);
            FieldInfo oFieldInfo = oSystype.GetField(strName);
            object[] rgObjs = oFieldInfo.GetCustomAttributes(typeof(XmlEnumAttribute), false);
            foreach (object obj in rgObjs)
            {
                XmlEnumAttribute oDesc = obj as XmlEnumAttribute;
                if (oDesc != null)
                {
                    return oDesc.Name;
                }
            }

            return "0";
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
