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
    public class AjouterFonctionViewModel : ViewModelBase
    {

        public ICommand NavigateLitteraleCommand { get; }

        public ICommand NavigateNumeriqueCommand { get; }


        public AjouterFonctionViewModel(INavigationService<LitteraleViewModel> litteraleNavigationService, INavigationService<NumeriqueViewModel> numeriqueNavigationService)
        {


            NavigateLitteraleCommand = new NavigateCommand<LitteraleViewModel>(litteraleNavigationService);

            NavigateNumeriqueCommand = new NavigateCommand<NumeriqueViewModel>(numeriqueNavigationService);

        }
    }
}
