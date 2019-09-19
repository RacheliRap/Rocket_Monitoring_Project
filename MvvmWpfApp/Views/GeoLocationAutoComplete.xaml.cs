using MvvmWpfApp.ViewModels;
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
using QuickType;
using MvvmWpfApp.Models;
using Toolkit.WPF.Controls.ConsoleControl;

namespace MvvmWpfApp.Controls
{
    /// <summary>
    /// Interaction logic for GeoLocationAutoComplete.xaml
    /// </summary>
    public partial class GeoLocationAutoComplete : UserControl
    {

        public static readonly DependencyProperty SelectedLocationProperty = DependencyProperty.Register(
            "SelectedLocation", typeof(Prediction), typeof(GeoLocationAutoComplete), new PropertyMetadata(default(Result)));

        public event SelectionChangedEventHandler SelectedChanged
        {
            add { CompleteBox.SelectionChanged += value; }
            remove { CompleteBox.SelectionChanged += value; }
        }

        public Prediction SelectedLocation
        {
            get { return (Prediction)GetValue(SelectedLocationProperty); }
            set { SetValue(SelectedLocationProperty, value); }
        }
 

        public event RoutedEventHandler TextChenged
        {
            add { CompleteBox.TextChanged += value; }
            remove { CompleteBox.TextChanged += value; }
        }

        public GeoLocationAutoCompleteVM CompleteVM { get; set; }
        public GeoLocationAutoComplete()
        {
            InitializeComponent();
            CompleteVM = new GeoLocationAutoCompleteVM();
            DataContext = CompleteVM;
        }

        private void AutoCompleteBox_OnTextChanged(object sender, RoutedEventArgs e)
        {
            CompleteVM.AutoComp(sender as AutoCompleteBox);
        }

        private void CompleteBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedLocation = (Prediction)(sender as AutoCompleteBox)?.SelectedItem;
        }
    }
}
