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
using LiveCharts.Wpf;
using LiveCharts;

namespace MvvmWpfApp.Views
{
    /// <summary>
    /// Interaction logic for EvaluateView.xaml
    /// </summary>
    public partial class EvaluateView : UserControl
    {

        public EvaluateView()
        {
            InitializeComponent();
            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString("#FD5523");
            var brush1 = (Brush)converter.ConvertFromString("#37966F");
            myPieChart.Series.Add(new PieSeries { Title = "BAD", Fill = brush, StrokeThickness = 0, Values = new ChartValues<double> { 40.0 } });
            myPieChart.Series.Add(new PieSeries { Title = "GOOD", Fill = brush1, StrokeThickness = 0, Values = new ChartValues<double> { 60.0 } });
            DataContext = this;
            slider.Value = 100;

        }


        private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
        {
            var chart = (LiveCharts.Wpf.PieChart)chartpoint.ChartView;

            //clear selected slice.
            foreach (PieSeries series in chart.Series)
                series.PushOut = 0;

            var selectedSeries = (PieSeries)chartpoint.SeriesView;
            selectedSeries.PushOut = 8;
        }

        internal void RefreshData(double badPct)
        {
            myPieChart.Series[0].Values[0] = badPct;
            myPieChart.Series[1].Values[0] = 100.0 - badPct;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            myPieChart.Series[0].Values[0] = 40 - slider.Value / 100;
            myPieChart.Series[1].Values[0] = 60 + slider.Value / 100;
        }

    }
}
