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
    public class NavigationBarViewModel : ViewModelBase
    {
        public ICommand NavigateAideCommand { get; }
        public ICommand NavigateParametreCommand { get; }
        public ICommand NavigateAproposCommand { get; }

        public NavigationBarViewModel(INavigationService<AideViewModel> aideNavigationService, INavigationService<ParametreViewModel> parametreNavigationService, INavigationService<AproposViewModel> aproposNavigationService)
        {
            NavigateAideCommand = new NavigateCommand<AideViewModel>(aideNavigationService);
            NavigateParametreCommand = new NavigateCommand<ParametreViewModel>(parametreNavigationService);
            NavigateAproposCommand = new NavigateCommand<AproposViewModel>(aproposNavigationService);
        }
    }
}
