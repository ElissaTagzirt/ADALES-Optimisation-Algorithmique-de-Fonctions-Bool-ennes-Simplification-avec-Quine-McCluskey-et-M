using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPLICATION.ViewModels
{
    public class VisibilityViewModel : ViewModelBase
    {
        private String _visibility;
        public String Visibility
        {
            get {
                if (_visibility == null)
                {
                    return "Collapsed";
                }

                return _visibility;

            }

           set
            {
                _visibility = value;
                OnPropertyChanged(Visibility); 
            }

        }

    }
}
