using APPLICATION.Commands;
using APPLICATION.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace APPLICATION.ViewModels
{
    public class SyntheseViewModel : ViewModelBase
    {
        public ICommand NavigateNumeriqueCommand { get; }  //C'est comme  GoBackCommand
        private string _expressionSimplifier;
        private string _drawingPath;
        public string DrawingPath
        {
            get => _drawingPath;
            
            set
            {
                _drawingPath = value;
                OnPropertyChanged(nameof(DrawingPath));
            }
        }
        public string ExpressionSimplifier
        {
            get { return _expressionSimplifier; }
            set
            {
                _expressionSimplifier = value;
                OnPropertyChanged(nameof(ExpressionSimplifier));
            }
        }

        public SyntheseViewModel(INavigationService<NumeriqueViewModel> numeriqueNavigationService)  // Le boutton de retour gobackCommand
        {
            NavigateNumeriqueCommand = new NavigateCommand<NumeriqueViewModel>(numeriqueNavigationService); // l'instanciation de la command
        }
    }
}
