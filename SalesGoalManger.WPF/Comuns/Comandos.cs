using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace ProjetoCadastros.Comuns
{
    public class Comandos
    {
        private readonly Dictionary<string, ICommand> _commands = new Dictionary<string, ICommand>();

        public void AdicionarComando(string nome, Action<object> execute, Predicate<object> canExecute = null)
        {
            _commands[nome] = new RelayCommand(execute, canExecute);
        }

        public ICommand GetCommand(string nome)
        {
            return _commands.ContainsKey(nome) ? _commands[nome] : null;
        }
    }
}
