using APPLICATION.Commands;
using APPLICATION.Service;
using APPLICATION.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace APPLICATION.ViewModels
{
    public class TraceNumeriqueViewModel : ViewModelBase
    {
        public ICommand NavigateNumeriqueCommand { get; }

        public TraceNumeriqueViewModel(INavigationService<NumeriqueViewModel> numeriqueNavigationService)
        {
            NavigateNumeriqueCommand = new NavigateCommand<NumeriqueViewModel>(numeriqueNavigationService);

        }
    }
}