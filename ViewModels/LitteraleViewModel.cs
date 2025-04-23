using APPLICATION.Commands;
using APPLICATION.Models;
using APPLICATION.Service;
using APPLICATION.Stores;
using Svg;
using System;
using System.Collections;
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
    public class LitteraleViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public ICommand DrawLitCommand { get; }
        public ICommand TraceLitCommand { get; }
        public ICommand GoBackCommand { get; }
        public ICommand NavigateAjouterFonctionCommand { get; }
        public ICommand NavigateTraceNumeriqueCommand { get; }
        public ICommand ResultCommand { get; }
        public ICommand ReinitialiseCommand { get; }

        public INotifyPropertyChanged CurrentPageViewModel
        {
            get => _currentPageViewModel;
            set
            {
                _currentPageViewModel = value;
                NotifyPropertyChanged(nameof(CurrentPageViewModel));
            }
        }
      
        public LitteraleViewModel(INavigationService<AjouterFonctionViewModel> ajouterFonctionNavigationService, INavigationService<TraceViewModel> traceNavigationService, INavigationService<SyntheseViewModel> syntheseNavigationService)
        {
            DrawLitCommand = new ResultatCommand(async () => await Draw(), canExecute: false);
            TraceLitCommand = new ResultatCommand(Trace, canExecute: false);
            GoBackCommand = new ResultatCommand(GoBack);
            NavigateAjouterFonctionCommand = new NavigateCommand<AjouterFonctionViewModel>(ajouterFonctionNavigationService);
           // NavigateTraceNumeriqueCommand = new NavigateCommand<TraceNumeriqueViewModel>(traceNumeriqueNavigationService);
            CurrentPageViewModel = new InputLitViewModel(DrawLitCommand, TraceLitCommand, NavigateAjouterFonctionCommand);
        }

        private void Trace()
        {
            if(CurrentPageViewModel is InputLitViewModel inputLitViewModel)
            {
                _lastExpression = inputLitViewModel.Expression;
                if(inputLitViewModel.Steps == null || inputLitViewModel.Steps.Count == 0)
                {
                    inputLitViewModel.SimplifierLitterale();
                }

                CurrentPageViewModel = new StepsViewModel(header: "La trace Littérale",
                                                          originalExpression: inputLitViewModel.Expression,
                                                          simplifiedExpression: inputLitViewModel.ExpressionSimplifier,
                                                          inputLitViewModel.Steps,
                                                          goBackCommand: GoBackCommand);
            }
        }

        private void GoBack()
        {
            if(CurrentPageViewModel is StepsViewModel stepViewModel)
            {
                CurrentPageViewModel = new InputLitViewModel(DrawLitCommand, TraceLitCommand, NavigateAjouterFonctionCommand)
                {
                    ExpressionSimplifier = stepViewModel.SimplifiedExpression,
                    Expression = _lastExpression
                };
            }
            else  if(CurrentPageViewModel is DrawingLitViewModel drawViewModel)
            {
                CurrentPageViewModel = new InputLitViewModel(DrawLitCommand, TraceLitCommand, NavigateAjouterFonctionCommand)
                {
                    ExpressionSimplifier = drawViewModel.ExpressionSimplifier,
                    Expression = _lastExpression
                };
            }
        }

        private async Task Draw()
        {
            if (CurrentPageViewModel is InputLitViewModel inputLitViewModel)
            {
                string expression;

                if (string.IsNullOrEmpty(inputLitViewModel.ExpressionSimplifier))
                {
                    expression = inputLitViewModel.Expression;
                }
                else
                {
                    expression = inputLitViewModel.ExpressionSimplifier;
                }

                _lastExpression = inputLitViewModel.Expression;

                try
                {
                    string drawingFilePath = await DrawExpression(expression);
                    this.CurrentPageViewModel = new DrawingLitViewModel(GoBackCommand)
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

        public INavigationService<LitteraleViewModel> litteraleNavigationService;

       // ImpliquantsEssLitterale imp = new ImpliquantsEssLitterale();

    }
}
