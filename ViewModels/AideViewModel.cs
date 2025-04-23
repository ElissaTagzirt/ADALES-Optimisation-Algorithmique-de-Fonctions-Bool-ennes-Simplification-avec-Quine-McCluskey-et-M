using APPLICATION.Commands;
using APPLICATION.Service;
using APPLICATION.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace APPLICATION.ViewModels
{

    public class AideViewModel : ViewModelBase
    {
        public VisibilityViewModel Visibility { get; private set; }
        public AideViewModel()
        {

            Visibility = new VisibilityViewModel();

        }

        public void setVisibility(String value)
        {
            Visibility.Visibility = value;
        }

    }
}
