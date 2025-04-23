using APPLICATION.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace APPLICATION.ViewModels
{
    internal class StepsViewModel : ViewModelBase
    {
        public string OriginalExpression { get; }
        public string SimplifiedExpression { get; }
        public StepsList StepsList { get; }
        public ICommand GoBackCommand { get; }
        public string Header { get; }

        public StepsViewModel(string header, string originalExpression, string simplifiedExpression, StepsList stepsList, ICommand goBackCommand)
        {
            Header = header;
            OriginalExpression = originalExpression;
            SimplifiedExpression = simplifiedExpression;
            StepsList = stepsList;
            GoBackCommand = goBackCommand;
        }
    }
}
