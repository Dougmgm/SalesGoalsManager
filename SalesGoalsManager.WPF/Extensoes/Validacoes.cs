namespace ProjetoCadastros.Extensoes
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

        //public static bool NenhumRadioSelecionado(this GroupBox groupBox)
        //{
        //    return !groupBox.Controls.OfType<RadioButton>().Any(r => r.Checked);
        //}
    }
}
