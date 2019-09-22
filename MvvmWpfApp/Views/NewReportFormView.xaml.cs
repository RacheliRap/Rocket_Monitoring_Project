using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.CompilerServices;
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
using BE;
using MvvmWpfApp.Controls;
using MvvmWpfApp.ViewModels;
using QuickType;
using MvvmWpfApp.Models;
using System.Net;
using Newtonsoft.Json;

namespace MvvmWpfApp.Views
{
    /// <summary>
    /// Interaction logic for NewReportFormView.xaml
    /// </summary>
    public partial class NewReportFormView : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty ReportFormVmProperty = DependencyProperty.Register(
            "ReportFormVm", typeof(NewReportFormVM), typeof(NewReportFormView), new PropertyMetadata(default(NewReportFormVM)));

        public NewReportFormVM ReportFormVm
        {
            get { return (NewReportFormVM)GetValue(ReportFormVmProperty); }
            set { SetValue(ReportFormVmProperty, value); }
        }

        public NewReportFormView()
        {
            InitializeComponent();
            InitForm();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Do not load your data at design time
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ResetForm(object sender, EventArgs e)
        {
            InitForm();
        }

        private void InitForm()
        {
            ReportFormVm = new NewReportFormVM();
            DataContext = ReportFormVm;
            SaveButton.Command = ReportFormVm.AddReportCommand;
            SaveButton.CommandParameter = ReportFormVm.FormModel;
            SaveButton.IsEnabled = false;
            ReportFormVm.PropertyChanged += (sender, args) => InitForm();
            //AddressTextBox.CompleteVM = new GeoLocationAutoCompleteVM();
        }

        private void SaveEnableCheck(object sender, RoutedEventArgs routedEventArgs)
        {
            int _;
            SaveButton.IsEnabled = AddressTextBox.SelectedLocation != null &&
                                   NameTextBox.Text != "" &&
                                   !(NoiseIntensityTextBox.Text == "0" ||
                                   !int.TryParse(NoiseIntensityTextBox.Text, out _)) &&
                                   !(NumOfExplosionsTextBox.Text == "0" ||
                                   !int.TryParse(NumOfExplosionsTextBox.Text, out _));
        }

        private void AddressTextBox_OnSelectedChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(sender is AutoCompleteBox) || e.AddedItems.Count == 0) return;
            (DataContext as NewReportFormVM).Report.Address = ((Prediction) e.AddedItems[0]).description;
            getCoordFromAddress((DataContext as NewReportFormVM).Report.Address);
            //(DataContext as NewReportFormVM).Report.Latitude = ((Result) e.AddedItems[0])?.Position?[0]??0;
            //(DataContext as NewReportFormVM).Report.Longitude = ((Result) e.AddedItems[0])?.Position?[1]??0;
        }


        private void ActionButton_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void ActionButton_Loaded_1(object sender, RoutedEventArgs e)
        {

        }

        private async void getCoordFromAddress(string address)
        {
             string strGooglePlaceAPILey = "AIzaSyAD9hZLLf8l_i48FI07LaS74WLOqkuK4C4";
             string strPlacesAutofillUrl = "https://maps.googleapis.com/maps/api/geocode/json?address=";

            string strResult, strFullURL;
            WebClient client = new WebClient();
            StringBuilder builderLocationAutoComplete = new StringBuilder(strPlacesAutofillUrl);
            builderLocationAutoComplete.Append(address).Append("&key=").Append(strGooglePlaceAPILey);
            strFullURL = builderLocationAutoComplete.ToString();
            builderLocationAutoComplete.Clear();
            builderLocationAutoComplete = null;
            try
            {
                strResult = await client.DownloadStringTaskAsync(new Uri(strFullURL));

                RootObject results = JsonConvert.DeserializeObject<RootObject>(strResult);
                (DataContext as NewReportFormVM).Report.Latitude = results.results[0].geometry.location.lat;
                (DataContext as NewReportFormVM).Report.Longitude = results.results[0].geometry.location.lng;

            }
            catch
            {
                strResult = "Exception";
                address = null;
            }
            finally
            {
                client.Dispose();
                client = null;
            }
        }
    }
}