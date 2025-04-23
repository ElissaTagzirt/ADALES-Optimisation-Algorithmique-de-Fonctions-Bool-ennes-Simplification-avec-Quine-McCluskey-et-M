using Haley.Abstractions;
using Haley.Enums;
using Haley.Events;
using Haley.Models;
using Haley.MVVM;
using Haley.Utils;
using Haley.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace APPLICATION.Views
{
    /// <summary>
    /// Interaction logic for ParametreView.xaml
    /// </summary>
    public partial class ParametreView : UserControl
              
    {
        private IThemeService ts;
        private string mode; 

        public ParametreView()
        {
            ts = ThemeService.Singleton;
            InitializeComponent();
        }

        private void LightMode_Checked(object sender, RoutedEventArgs e)
        {
            //Properties.Settings.Default.Colormode = "Light";
            //Properties.Settings.Default.Save();
            mode = "Light"; 
            ts.ChangeTheme(mode); 
        }

        private void DarkMode_Checked(object sender, RoutedEventArgs e)
        {
            //    Properties.Settings.Default.Colormode = "Dark";
            //    Properties.Settings.Default.Save();
            mode = "Dark";
            ts.ChangeTheme(mode);

        }
    }
    
}
