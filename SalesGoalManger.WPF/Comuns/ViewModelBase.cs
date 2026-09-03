using System.Windows.Input;

namespace SalesGoalManger.WPF.Comuns
{
    public class ViewModelBase : NotifyPropertyChangedBase
    {
        public readonly Dictionary<string, ICommand> _comandos = new Dictionary<string, ICommand>();
        public ICommand this[string commandName] => _comandos.ContainsKey(commandName) ? _comandos[commandName] : null;
    }
}
