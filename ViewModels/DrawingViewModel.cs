using APPLICATION.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace APPLICATION.ViewModels
{
    internal class DrawingViewModel : INotifyPropertyChanged
    {
        public ICommand GoBackCommand { get; }

        public string DrawingPath
        {
            get => _drawingPath;
            set
            {
                _drawingPath = value;
                NotifyPropertyChanged(nameof(DrawingPath));
            }
        }

        public string ExpressionSimplifier
        {
            get { return _expressionSimplifier; }
            set
            {
                _expressionSimplifier = value;
                NotifyPropertyChanged(nameof(ExpressionSimplifier));
            }
        }

        public DrawingViewModel(ICommand goBackCommand)
        {
           GoBackCommand = goBackCommand;
            //GoBackCommand = new ResultatCommand(GoBack);
        }
        private void GoBack()
        {
          //  CurrentPageViewModel = new NumeriqueViewModel(DrawCommand)
            //{
             //   Expression = _lastExpression
            //};
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _expressionSimplifier;
        private string _drawingPath;
    }
}
