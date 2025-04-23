using APPLICATION.Commands;
using APPLICATION.Models;
using APPLICATION.Service;
using APPLICATION.Stores;
using Svg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace APPLICATION.ViewModels
{

   
    public class NumeriqueViewModel : ViewModelBase  , INotifyPropertyChanged
    {

        public string Expression;
        public string ExpressionSimplifier;
        public ICommand DrawCommand { get; }
        public ICommand GoBackCommand { get; }
        public ICommand NavigateAjouterFonctionCommand { get; }
        public ICommand NavigateTraceNumeriqueCommand { get; }
        public ICommand ResultCommand { get; }
        public ICommand TraceCommand { get; }

        public INotifyPropertyChanged CurrentPageViewModel
        {
                get => _currentPageViewModel;
                set
                {
                    _currentPageViewModel = value;
                    NotifyPropertyChanged(nameof(CurrentPageViewModel));
                }
        }

            public NumeriqueViewModel(INavigationService<AjouterFonctionViewModel> ajouterFonctionNavigationService, INavigationService<TraceNumeriqueViewModel> traceNumeriqueNavigationService, INavigationService<SyntheseViewModel> syntheseNavigationService)
            {
                DrawCommand = new ResultatCommand(async () => await Draw(), canExecute: false);
                GoBackCommand = new ResultatCommand(GoBack);
            TraceCommand = new ResultatCommand(Trace, canExecute: false);
                NavigateAjouterFonctionCommand = new NavigateCommand<AjouterFonctionViewModel>(ajouterFonctionNavigationService);
                NavigateTraceNumeriqueCommand = new NavigateCommand<TraceNumeriqueViewModel>(traceNumeriqueNavigationService);
                CurrentPageViewModel = new InputViewModel(DrawCommand,TraceCommand, NavigateAjouterFonctionCommand);
            }
       

        private void Trace()
        {
            if (CurrentPageViewModel is InputViewModel inputViewModel)
            {
                CurrentPageViewModel = new StepsViewModel(header: "La trace Numérique",
                                                          originalExpression: inputViewModel.Expression,
                                                          simplifiedExpression: inputViewModel.ExpressionSimplifier,
                                                          stepsList: inputViewModel.Steps,
                                                          goBackCommand: new ResultatCommand(() =>
                                                          {
                                                              CurrentPageViewModel = new InputViewModel(DrawCommand, TraceCommand, NavigateAjouterFonctionCommand)
                                                              {
                                                                  Expression = inputViewModel.Expression,
                                                                  ExpressionSimplifier = inputViewModel.ExpressionSimplifier,
                                                                  Steps = inputViewModel.Steps
                                                              };
                                                          }));
            }
        }
        private void GoBack()
        {
            CurrentPageViewModel = new InputViewModel(DrawCommand,TraceCommand, NavigateAjouterFonctionCommand)
            {
                Expression = _lastExpression
            };
        }
        
        private async Task Draw()
        {
            if (CurrentPageViewModel is InputViewModel inputViewModel)
            {
                string expression;
                if (string.IsNullOrEmpty(inputViewModel.ExpressionSimplifier))
                {
                    expression = inputViewModel.ExpressionSimplifier;
                }
                else
                {
                    expression = inputViewModel.ExpressionSimplifier;
                }
        
                _lastExpression = inputViewModel.Expression;
        
                try
                {
                    string drawingFilePath = await DrawExpression(expression);
                    this.CurrentPageViewModel = new DrawingViewModel(GoBackCommand)
                    {
                        DrawingPath = drawingFilePath,
                        ExpressionSimplifier = expression
                    };
        
                    await Task.Delay(2000);
                    File.Delete(drawingFilePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,
                                    ex.GetType().ToString(),
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                }
            }
        }

        private async Task<string> DrawExpression(string expression)
        {
            var drawer = new ExpressionDrawer();
            return await drawer.DrawExpression(expression);
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private INotifyPropertyChanged _currentPageViewModel;
        public event PropertyChangedEventHandler PropertyChanged;
        private string _lastExpression;
       
        public INavigationService<NumeriqueViewModel> numeriqueNavigationService;
        ImpliquantsEss imp = new ImpliquantsEss();
    }
}
