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
    public class TraceViewModel : ViewModelBase
    {
        public ICommand NavigateLitteraleCommand { get; }


        public TraceViewModel(INavigationService<LitteraleViewModel> litteraleNavigationService)
        {
            NavigateLitteraleCommand = new NavigateCommand<LitteraleViewModel>(litteraleNavigationService);

        }
    }
}
