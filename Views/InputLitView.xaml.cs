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
    /// Interaction logic for LitteraleView.xaml
    /// </summary>
    public partial class InputLitView : UserControl
    {
        public string s;
        //string s2;
        string operation;

        public InputLitView()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(ResultatDisplay.Text);

        }
        private void ETButton_Click(object sender, RoutedEventArgs e)
        {
            s = TextDisplay.Text;
            // s2 = s;
            s = s + "&";
            TextDisplay.Text = s;

        }

        private void ParOButton_Click(object sender, RoutedEventArgs e)
        {
            s = TextDisplay.Text;
            //s2 = s;
            s = s + "(";
            TextDisplay.Text = s;
        }

        private void NONButton_Click(object sender, RoutedEventArgs e)
        {
            s = TextDisplay.Text;
            // s2 = s;
            s = s + "!";
            TextDisplay.Text = s;
        }

        private void ParFButton_Click(object sender, RoutedEventArgs e)
        {
            s = TextDisplay.Text;
            //s2 = s;
            s = s + ")";
            TextDisplay.Text = s;
        }

        private void OUButton_Click(object sender, RoutedEventArgs e)
        {
            s = TextDisplay.Text;
            //s2 = s;
            s = s + "+";
            TextDisplay.Text = s;
        }

        private void CButton_Click(object sender, RoutedEventArgs e)
        {
            s = TextDisplay.Text;
            // s2 = s;
            s = "";
            TextDisplay.Text = "";
        }

        private void CEButton_Click(object sender, RoutedEventArgs e)
        {
            s = TextDisplay.Text;
            if (s.Length >= 1)
            {
                string c = s.Substring(0, s.Length - 1);
                s = c;

            }
            else
            {
                s = "";
            }
            TextDisplay.Text = s;
        }
    }
}