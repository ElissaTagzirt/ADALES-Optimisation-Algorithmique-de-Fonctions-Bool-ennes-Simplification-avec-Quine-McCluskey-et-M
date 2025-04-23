using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace APPLICATION.Commands
{

    internal class ResultatCommand : ICommand
    {
        public bool IsEnabled
        {
            get { return _canExecute; }
            set 
            {
                if(_canExecute != value)
                {
                    _canExecute = value; 
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public ResultatCommand(Action action, bool canExecute = true)
        {
            this._action = action;
            this.IsEnabled = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return IsEnabled;
        }

        public void Execute(object parameter)
        {
            _action?.Invoke();
        }

        private bool _canExecute;
        private readonly Action _action;
    }
}
