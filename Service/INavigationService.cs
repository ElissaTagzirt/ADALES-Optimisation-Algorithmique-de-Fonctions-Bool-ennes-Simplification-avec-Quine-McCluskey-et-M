using APPLICATION.ViewModels;

namespace APPLICATION.Service
{
    public interface INavigationService<TViewModel> where TViewModel : ViewModelBase
    {
        void Navigate();
    }
}