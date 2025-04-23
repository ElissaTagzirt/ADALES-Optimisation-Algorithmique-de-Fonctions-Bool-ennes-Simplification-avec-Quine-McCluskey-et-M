using APPLICATION.Stores;
using APPLICATION.ViewModels;
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
    /// Interaction logic for AideView.xaml
    /// </summary>
    public partial class AideView : UserControl
    {

        AideViewModel _aide = new AideViewModel();

        public AideView()
        {
            InitializeComponent();
            DataContext = _aide;
        }

        private void Erreurs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Erreurs.SelectedItem == SyntaxeErreur)
            {
                _aide.setVisibility("Visible");
                SyntaxeT.Visibility = Visibility.Visible;
                _aide.setVisibility("Collapsed");
                EntryT.Visibility = Visibility.Collapsed;
            }
            if (Erreurs.SelectedItem == EntryErreur)
            {
                _aide.setVisibility("Visible");
                EntryT.Visibility = Visibility.Visible;
                _aide.setVisibility("Collapsed");
                SyntaxeT.Visibility = Visibility.Collapsed;


            }

        }
    }
}
