﻿using System;
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
using LiveCharts;
using LiveCharts.Wpf;

namespace MvvmWpfApp.Views
{
    /// <summary>
    /// Interaction logic for ChartView.xaml
    /// </summary>
    public partial class ChartView : UserControl
    {
      
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
        public ChartView()
        {
            InitializeComponent();
            DataContext = this;
            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString("#FD5523");
            var brush1 = (Brush)converter.ConvertFromString("#37966F");

            // bar chart
            SeriesCollection = new SeriesCollection
            {   //reports
                new ColumnSeries
                {
                    Title = "Report",
                    Fill = brush,
                    Values = new ChartValues<double> { 3, 10, 7, 6, 5, 10, 5, 2, 2, 0, 0, 0 }
                }
            };

            //adding series will update and animate the chart automatically
            SeriesCollection.Add(new ColumnSeries
            {   //real hits
                Title = "Events",
                Fill = brush1,
                Values = new ChartValues<double> { 2, 5, 1, 6, 3, 8, 5, 2, 1, 0, 0, 0 }
            });

            //also adding values updates and animates the chart automatically
            //SeriesCollection[1].Values.Add(48d);

            Labels = new[] { "Jan", "Feb", "March", "Apr" ,"May",
                "Jun", "July","Aug", "Sep", "Oct", "Nov", "Dec"};
            Formatter = value => value.ToString("N");


        }
        //PointLabel = chartPoint =>
        //  string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
        //ChartValues<double> chartValue = new ChartValues<double> { 100.0};
        //DataContext = this;

        //public Func<ChartPoint, string> PointLabel { get; set; }

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
           // myPieChart.Series[0].Values[0] = badPct;
            //myPieChart.Series[1].Values[0] = 100.0 - badPct;
        }


    }
}
