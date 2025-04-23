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
    public class NavigationBareViewModel : ViewModelBase
    {
        public ICommand NavigateAccueilCommand { get; }
        public ICommand NavigateAjouterFonctionCommand { get; }
        public ICommand NavigateSyntheseCommand { get; }
        public ICommand NavigateDictionnaireCommand { get; }
        public ICommand NavigateLitteraleCommand { get; }
        public ICommand NavigateNumeriqueCommand { get; }
        public ICommand NavigateTraceCommand { get; }
        public ICommand NavigateTraceNumeriqueCommand { get; }




        public NavigationBareViewModel(INavigationService<AccueilViewModel> acceuilNavigationService,
            INavigationService<AjouterFonctionViewModel> ajouterFonctionNavigationService,
            INavigationService<SyntheseViewModel> syntheseNavigationService,
            INavigationService<DictionnaireViewModel> dictionnaireNavigationService,
            INavigationService<LitteraleViewModel> litteraleNavigationService,
            INavigationService<NumeriqueViewModel> numeriqueNavigationService,
            INavigationService<TraceViewModel> traceNavigationService,
            INavigationService<TraceNumeriqueViewModel> traceNumeriqueNavigationService)
        {
            NavigateAccueilCommand = new NavigateCommand<AccueilViewModel>(acceuilNavigationService);
            NavigateAjouterFonctionCommand = new NavigateCommand<AjouterFonctionViewModel>(ajouterFonctionNavigationService);
            NavigateSyntheseCommand = new NavigateCommand<SyntheseViewModel>(syntheseNavigationService);
            NavigateDictionnaireCommand = new NavigateCommand<DictionnaireViewModel>(dictionnaireNavigationService);
            NavigateLitteraleCommand = new NavigateCommand<LitteraleViewModel>(litteraleNavigationService);
            NavigateNumeriqueCommand = new NavigateCommand<NumeriqueViewModel>(numeriqueNavigationService);
            NavigateTraceCommand = new NavigateCommand<TraceViewModel>(traceNavigationService);
            NavigateTraceNumeriqueCommand = new NavigateCommand<TraceNumeriqueViewModel>(traceNumeriqueNavigationService);


        }
    }
}
