using ProjetoCadastros.Comuns;
using System.Collections.Generic;
using System.Windows.Input;

namespace ProjetoCadastros.Interface.ViewModel
{
    public class ViewModelBase : NotifyPropertyChangedBase
    {
        public readonly Dictionary<string, ICommand> _comandos = new Dictionary<string, ICommand>();
        public ICommand this[string commandName] => _comandos.ContainsKey(commandName) ? _comandos[commandName] : null;
    }
}
