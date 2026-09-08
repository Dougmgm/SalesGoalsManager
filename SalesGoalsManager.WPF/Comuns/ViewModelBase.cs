using System.Windows.Input;

namespace SalesGoalsManager.WPF.Comuns
{
    public class ViewModelBase : NotifyPropertyChangedBase
    {
        public readonly Dictionary<string, ICommand> _comandos = new Dictionary<string, ICommand>();
        public ICommand this[string commandName] => _comandos.ContainsKey(commandName) ? _comandos[commandName] : null;
    }
}
