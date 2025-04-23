using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using APPLICATION.ViewModels;

namespace APPLICATION.Commands
{
    internal class ReinitialiserCommand : ICommand
    {
        public string Expression, ExpressionSimplifier; 
       // NumeriqueViewModel num = new NumeriqueViewModel();
        public ReinitialiserCommand(Action action)
        {
            this._action = action;
           
        }
       
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action?.Invoke();
           // num.setRen(true);
        }

      

        private readonly Action _action;
    }
}
