using Haley.Abstractions;
using Haley.Enums;
using Haley.Events;
using Haley.Models;
using Haley.MVVM;
using Haley.Utils;
using Haley.Services;
using APPLICATION.Service;
using APPLICATION.Stores;
using APPLICATION.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace APPLICATION
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application

    {
        private readonly NavigationStore _navigationStore;
        ICommand drawCommand;
        public App()
        {


            _navigationStore = new NavigationStore();
        }
        protected override void OnStartup(StartupEventArgs e)
        {

            INavigationService<AccueilViewModel> accueilNavigationService = CreateAccueilNavigationService();

            accueilNavigationService.Navigate();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };
            MainWindow.Show();
            base.OnStartup(e);
        }

        private INavigationService<AccueilViewModel> CreateAccueilNavigationService()
        {
            return new LayoutNavigationService<AccueilViewModel>(_navigationStore, () => new AccueilViewModel(), CreateNavigationBareViewModel, CreateNavigationBarViewModel);
        }
        private INavigationService<DictionnaireViewModel> CreateDictionnaireNavigationService()
        {
            return new LayoutNavigationService<DictionnaireViewModel>(_navigationStore, () => new DictionnaireViewModel(), CreateNavigationBareViewModel, CreateNavigationBarViewModel);
        }

        private INavigationService<SyntheseViewModel> CreateSyntheseNavigationService()
        {
            return new LayoutNavigationService<SyntheseViewModel>(_navigationStore, () => new SyntheseViewModel(CreateNumeriqueNavigationService()), CreateNavigationBareViewModel, CreateNavigationBarViewModel);
        }

        private INavigationService<AjouterFonctionViewModel> CreateAjouterFonctionNavigationService()
        {
            return new LayoutNavigationService<AjouterFonctionViewModel>(_navigationStore, () => new AjouterFonctionViewModel(CreateLitteraleNavigationService(), CreateNumeriqueNavigationService()), CreateNavigationBareViewModel, CreateNavigationBarViewModel);
        }
        private INavigationService<LitteraleViewModel> CreateLitteraleNavigationService()
        {
            return new LayoutNavigationService<LitteraleViewModel>(_navigationStore, () => new LitteraleViewModel(CreateAjouterFonctionNavigationService(), CreateTraceNavigationService(), CreateSyntheseNavigationService()), CreateNavigationBareViewModel, CreateNavigationBarViewModel);
        }
        private INavigationService<NumeriqueViewModel> CreateNumeriqueNavigationService()
        {
            return new LayoutNavigationService<NumeriqueViewModel>(_navigationStore, () => new NumeriqueViewModel(CreateAjouterFonctionNavigationService(), CreateTraceNumeriqueNavigationService(),CreateSyntheseNavigationService()),CreateNavigationBareViewModel, CreateNavigationBarViewModel);
        }
        private INavigationService<TraceViewModel> CreateTraceNavigationService()
        {
            return new LayoutNavigationService<TraceViewModel>(_navigationStore, () => new TraceViewModel(CreateLitteraleNavigationService()), CreateNavigationBareViewModel, CreateNavigationBarViewModel);
        }

        private INavigationService<TraceNumeriqueViewModel> CreateTraceNumeriqueNavigationService()
        {
            return new LayoutNavigationService<TraceNumeriqueViewModel>(_navigationStore, () => new TraceNumeriqueViewModel(CreateNumeriqueNavigationService()), CreateNavigationBareViewModel, CreateNavigationBarViewModel);
        }
        private INavigationService<AideViewModel> CreateAidelNavigationService()
        {
            return new LayoutNavigationService<AideViewModel>(_navigationStore, () => new AideViewModel(), CreateNavigationBareViewModel, CreateNavigationBarViewModel);
        }

        private INavigationService<ParametreViewModel> CreateParametreNavigationService()
        {
            return new LayoutNavigationService<ParametreViewModel>(_navigationStore, () => new ParametreViewModel(), CreateNavigationBareViewModel, CreateNavigationBarViewModel);
        }

        private INavigationService<AproposViewModel> CreateAproposNavigationService()
        {
            return new LayoutNavigationService<AproposViewModel>(_navigationStore, () => new AproposViewModel(), CreateNavigationBareViewModel, CreateNavigationBarViewModel);
        }

        private NavigationBarViewModel CreateNavigationBarViewModel()
        {
            return new NavigationBarViewModel(
                  CreateAidelNavigationService(),
                  CreateParametreNavigationService(),
                  CreateAproposNavigationService());

        }
        private NavigationBareViewModel CreateNavigationBareViewModel()
        {
            return new NavigationBareViewModel(
                 CreateAccueilNavigationService(),
                 CreateAjouterFonctionNavigationService(),
                 CreateSyntheseNavigationService(),
                 CreateDictionnaireNavigationService(),
                 CreateLitteraleNavigationService(),
                 CreateNumeriqueNavigationService(),
                 CreateTraceNavigationService(),
                 CreateTraceNumeriqueNavigationService());


        }

        private IThemeService ts; 
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            ts = ThemeService.Singleton;
            Register();

        }

        void Register()
        {

            var _asmBuilder = new AssemblyThemeBuilder();

            _asmBuilder
                .Add("Light", new Uri(@"pack://application:,,,/ADALES;component/Styles/ThemeLight.xaml"))
                .Add("Dark", new Uri(@"pack://application:,,,/ADALES;component/Styles/ThemeDark.xaml"));


            ts.RegisterGroup(_asmBuilder);

            ts.SetStartupTheme("Light"); 

        }
    }
}
