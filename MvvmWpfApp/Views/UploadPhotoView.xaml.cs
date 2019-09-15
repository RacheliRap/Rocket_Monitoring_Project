using Microsoft.Win32;
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
using ExifLib;
using System.Drawing;
using MvvmWpfApp.Models;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using System.Drawing.Imaging;
using GuigleAPI;
using System.Xml;
using GoogleDirections;
using GuigleAPI.Model;
using System.Net.Http;
using System.Net;

//using GoogleDirections;

namespace MvvmWpfApp.Views
{
    /// <summary>
    /// Interaction logic for UploadPhotoView.xaml
    /// </summary>
    public partial class UploadPhotoView : UserControl
    {
        public const string strGooglePlaceAPILey = "AIzaSyBsAIjK9ba8-EYxltrtnBEnp91mw--Muro";
        public const string strPlacesAutofillUrl = "https://maps.googleapis.com/maps/api/geocode/json?latlng=";

        public UploadPhotoView()
        {
            InitializeComponent();
        }
        private async void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                UriTextBox.Foreground = System.Windows.Media.Brushes.Black;
                UriTextBox.Text = new Uri(op.FileName).LocalPath;
                imgPhoto.Source = new BitmapImage(new Uri(op.FileName));

            }
            //------------------------------------------------------------------------
           //var geocoder = new Geocoder("AIzaSyBsAIjK9ba8-EYxltrtnBEnp91mw--Muro");
           //var locations = geocoder.Geocode("Hawks Road, Kingston, UK");
            // var address = geocoder.ReverseGeocode(new LatLng(51.408580, -0.292470));
            //-----------------------------------------------------------------
            //GoogleGeocodingAPI.GoogleAPIKey = "AIzaSyDllS5d3Ot3QQ6YoEOoICyemScrx8AAe1Y";
            //var result = await GoogleGeocodingAPI.GetCityFromCoordinatesAsync(0.2323, 0.2323);
            //var city = result.Item1;
            //var country = result.Item3;
            //AddressResponse result = await GoogleGeocodingAPI.SearchAddressAsync("100 Market St, Southbank");
            //------------------------------------------------------------------
           WebClient client = new WebClient();
            string coordinate1 = "32.797821,-96.781720";
            string strFullURL;
            StringBuilder builderLocationAutoComplete = new StringBuilder(strPlacesAutofillUrl);
            builderLocationAutoComplete.Append(coordinate1).Append("&key=").Append(strGooglePlaceAPILey);
            strFullURL = builderLocationAutoComplete.ToString();
            builderLocationAutoComplete.Clear();
            builderLocationAutoComplete = null;
            string strResult;
            try
            {
                strResult = await client.DownloadStringTaskAsync(new Uri(strFullURL));
            }
            catch
            {
                strResult = "Exception";
            }
            finally
            {
                client.Dispose();
                client = null;
            }
           


            //Console.WriteLine("enter coordinate:");
     

        }

        private void saveReport_Click(object sender, RoutedEventArgs e)
        {
            GeoLocation location =  ActionResult(UriTextBox.Text);
            double lat = location.Latitude;
            if (location == null)
            {
                MessageBox.Show("Make sure to take the photo while your phone location is on."
                    ,"Couldn't Extract coordinates from image!" ,MessageBoxButton.OK, 
                    MessageBoxImage.Error);
            }
            StringBuilder showString = new StringBuilder("A new falling event was added at Lat ");
            showString.Append(location.Latitude).Append(",lat ").Append(location.Longitude);
            MessageBox.Show(showString.ToString(), "Add Event", MessageBoxButton.OK);
        }

        //extract coordinates from file 
        public GeoLocation ActionResult(String photoUri)
        {
            var directories = ImageMetadataReader.ReadMetadata(photoUri);

            var gps = directories.OfType<GpsDirectory>().FirstOrDefault();

            if (gps != null)
            {
                var location = gps.GetGeoLocation();

                if (location != null)
                    return location;
                    Console.WriteLine("Lat {0} Lng {1}", location.Latitude, location.Longitude);
            }
            return null;
        }


            /*public void try1(String photoUri)
            {
                using (Bitmap bitmap = new Bitmap(photoUri))
                {
                    var longitude = GetCoordinateDouble(bitmap.PropertyItems.Single(p => p.Id == 4));
                    var latitude = GetCoordinateDouble(bitmap.PropertyItems.Single(p => p.Id == 2));

                    Console.WriteLine($"Longitude: {longitude}");
                    Console.WriteLine($"Latitude: {latitude}");

                    Console.WriteLine($"https://www.google.com/maps/place/{latitude},{longitude}");

                }

                var model = new PhotoCoordinatesModel();
                try
                {
                    using (var reader = new ExifLib.ExifReader(photoUri))
                    {
                        model.Lat = reader.GetLatitude();
                        model.Lon = reader.GetLongitude();
                    }
                }
                catch (ExifLibException exifex)
                {
                    model.Error = exifex.Message;
                }

                return model;
            }


        }


        private static double GetCoordinateDouble(PropertyItem propItem)
            {
                uint degreesNumerator = BitConverter.ToUInt32(propItem.Value, 0);
                uint degreesDenominator = BitConverter.ToUInt32(propItem.Value, 4);
                double degrees = degreesNumerator / (double)degreesDenominator;


                uint minutesNumerator = BitConverter.ToUInt32(propItem.Value, 8);
                uint minutesDenominator = BitConverter.ToUInt32(propItem.Value, 12);
                double minutes = minutesNumerator / (double)minutesDenominator;

                uint secondsNumerator = BitConverter.ToUInt32(propItem.Value, 16);
                uint secondsDenominator = BitConverter.ToUInt32(propItem.Value, 20);
                double seconds = secondsNumerator / (double)secondsDenominator;

                double coorditate = degrees + (minutes / 60d) + (seconds / 3600d);
                string gpsRef = System.Text.Encoding.ASCII.GetString(new byte[1] { propItem.Value[0] }); //N, S, E, or W  

                if (gpsRef == "S" || gpsRef == "W")
                {
                    coorditate = coorditate * -1;
                }
                return coorditate;
            }

        }*/
    }
}
