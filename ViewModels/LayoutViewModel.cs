using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPLICATION.ViewModels
{
    public class LayoutViewModel : ViewModelBase
    {
        public NavigationBareViewModel NavigationBareViewModel { get; }
        public NavigationBarViewModel NavigationBarViewModel { get; }
        public ViewModelBase ContentViewModel { get; }

        public LayoutViewModel(NavigationBareViewModel navigationBareViewModel, NavigationBarViewModel navigationBarViewModel, ViewModelBase contentViewModelViewModel)
        {
            NavigationBareViewModel = navigationBareViewModel;
            NavigationBarViewModel = navigationBarViewModel;
            ContentViewModel = contentViewModelViewModel;
        }
    }
}
