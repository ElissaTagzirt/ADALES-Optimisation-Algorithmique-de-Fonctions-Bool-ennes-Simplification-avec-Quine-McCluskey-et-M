using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace APPLICATION.Components
{
    public partial class StepEntryControl : UserControl
    {
        public string DecimalsString
        {
            get { return (string)GetValue(DecimalsStringProperty); }
            set { SetValue(DecimalsStringProperty, value); }
        }

        public IEnumerable<int> Decimals
        {
            get { return (IEnumerable<int>)GetValue(DecimalsProperty); }
            set { SetValue(DecimalsProperty, value); }
        }

        public byte? Check
        {
            get { return (byte)GetValue(CheckProperty); }
            set { SetValue(CheckProperty, value); }
        }

        public int? Groupe
        {
            get { return (int)GetValue(GroupeProperty); }
            set { SetValue(GroupeProperty, value); }
        }

        public string Minterme
        {
            get { return (string)GetValue(MintermeProperty); }
            set { SetValue(MintermeProperty, value); }
        }

        public static readonly DependencyProperty MintermeProperty =
            DependencyProperty.Register("Minterme", typeof(string), typeof(StepEntryControl), new PropertyMetadata(null));

        public static readonly DependencyProperty GroupeProperty =
            DependencyProperty.Register("Groupe", typeof(int?), typeof(StepEntryControl), new PropertyMetadata(null));

        public static readonly DependencyProperty CheckProperty =
            DependencyProperty.Register("Check", typeof(byte?), typeof(StepEntryControl), new PropertyMetadata(null));

        public static readonly DependencyProperty DecimalsProperty =
            DependencyProperty.Register("Decimals", typeof(IEnumerable<int>), typeof(StepEntryControl), new PropertyMetadata(null, DecimalsChanged));

        private static void DecimalsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var @this = (StepEntryControl)d;
            var newList = (IEnumerable<int>)e.NewValue;
            var decimalsString = string.Join(",", newList);
            @this.DecimalsString = decimalsString;
        }

        public static readonly DependencyProperty DecimalsStringProperty =
            DependencyProperty.Register("DecimalsString", typeof(string), typeof(StepEntryControl), new PropertyMetadata(null));
        public StepEntryControl()
        {
            InitializeComponent();
        }
    }
}
