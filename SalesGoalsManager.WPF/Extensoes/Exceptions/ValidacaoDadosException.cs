using System;

namespace ProjetoCadastros.Extensoes.Exceptions
{
    [Serializable]
    public class ValidacaoDadosException : Exception
    {
        public ValidacaoDadosException() { }

        public ValidacaoDadosException(string mensagem) : base(mensagem) { }
    }
}
