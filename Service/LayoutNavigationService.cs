using APPLICATION.Stores;
using APPLICATION.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPLICATION.Service
{
    public class LayoutNavigationService<TViewModel> : INavigationService<TViewModel> where TViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<NavigationBareViewModel> _createNavigationBareViewModel;
        private readonly Func<NavigationBarViewModel> _createNavigationBarViewModel;
        private readonly Func<TViewModel> _createViewModel;

        public LayoutNavigationService(NavigationStore navigationStore, Func<TViewModel> createViewModel, Func<NavigationBareViewModel> createNavigationBareViewModel, Func<NavigationBarViewModel> createNavigationBarViewModel)
        {
            _navigationStore = navigationStore;
            _createNavigationBareViewModel = createNavigationBareViewModel;
            _createNavigationBarViewModel = createNavigationBarViewModel;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel = new LayoutViewModel(_createNavigationBareViewModel(),_createNavigationBarViewModel(), _createViewModel());
        }
    }
}
