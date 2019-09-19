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
using System.Windows.Shapes;

namespace MvvmWpfApp.Views
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
        }
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        } 

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl usc = null;
            GridMain.Children.Clear();
            mainTitle.Text = "Detecting Rocket Hits";
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemHome":
                    usc = new MapView();
                    GridMain.Children.Add(usc);
                    mainTitle.Text = "View Map and Hits";
                    break;
                case "ItemReport":
                    usc = new NewReportFormView();
                    GridMain.Children.Add(usc);
                    mainTitle.Text = "Add New Report";
                    break;
                case "ItemAnalysis":
                    usc = new ChartView();
                    GridMain.Children.Add(usc);
                    mainTitle.Text = "Analysis";
                    break;
                case "ItemEvaluate":
                    usc = new EvaluateView();
                    GridMain.Children.Add(usc);
                    mainTitle.Text = "Evaluate k-Means Performance";
                    break;
                case "ItemUpdate":
                    usc = new UploadPhotoView();
                    GridMain.Children.Add(usc);
                    mainTitle.Text = "Upload Photo for Real Location";
                    break;
                default:
                    break;
            }
        }

        private void ItemCreate_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}
