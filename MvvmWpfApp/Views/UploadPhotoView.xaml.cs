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
using System.Net;
using Newtonsoft.Json;

//using GoogleDirections;

namespace MvvmWpfApp.Views
{
    /// <summary>
    /// Interaction logic for UploadPhotoView.xaml
    /// </summary>
    public partial class UploadPhotoView : UserControl
    {
        public const string strGooglePlaceAPILey = "AIzaSyAD9hZLLf8l_i48FI07LaS74WLOqkuK4C4";
        public const string strPlacesAutofillUrl = "https://maps.googleapis.com/maps/api/geocode/json?latlng=";
        GeoLocation location;
        string address;


        public UploadPhotoView()
        {
            InitializeComponent();
            saveReport.IsEnabled = false;
        }
        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            UploadPhoto(); //open file dialog and upload photo to view
            saveReport.IsEnabled = true; //if a photo was uploaded, enable the save button
            location = getCordFromPhoto(UriTextBox.Text); //get the coordinates from the photo
            if(location != null)
            {   

                getAddressFromCord(); //return address from coordinates
            }
     
        }
        // return address from coordinates by google API
        private async void getAddressFromCord()
        {
            string strResult, strFullURL;
            WebClient client = new WebClient();
            StringBuilder coordinate = new StringBuilder(location.Latitude.ToString()).Append(",").Append(location.Longitude.ToString());
            String mCoordinates = coordinate.ToString();
            StringBuilder builderLocationAutoComplete = new StringBuilder(strPlacesAutofillUrl);
            builderLocationAutoComplete.Append(coordinate).Append("&key=").Append(strGooglePlaceAPILey);
            strFullURL = builderLocationAutoComplete.ToString();
            builderLocationAutoComplete.Clear();
            builderLocationAutoComplete = null;
            try
            {
                strResult = await client.DownloadStringTaskAsync(new Uri(strFullURL));
                RootObject results = JsonConvert.DeserializeObject<RootObject>(strResult);
                address = results.results[0].formatted_address;
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

        // open file dialog, allow the user to chooce a photo and upload it to the view
        private void UploadPhoto()
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
        }

        private void saveReport_Click(object sender, RoutedEventArgs e)
        {
            if (location == null) //can't extract location from photo
            {
                MessageBox.Show("Make sure to take the photo while your phone location is on."
                    ,"Couldn't Extract coordinates from image!" ,MessageBoxButton.OK, 
                    MessageBoxImage.Error);
            }
            else
            {
                if(address == null) // The photo had coordinates, but couldnt convert them to address
                {
                    StringBuilder showString = new StringBuilder("Are you sure you want to add a new falling event at:\n ");
                    showString.Append("https://www.google.com/maps/place/{lat},{lon}");
                    MessageBox.Show(showString.ToString(), "Add Event", MessageBoxButton.OK);
                }
                else // The photo contained coordinates, and address was extracted
                {
                    StringBuilder showString = new StringBuilder("Are you sure you want to add a new falling event at ");
                    showString.Append("\n").Append(address).Append("?");
                    var result = MessageBox.Show(showString.ToString(), "Add Event",  MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        showString = new StringBuilder("A new falling event at was added at:");
                        showString.Append("\n").Append(address);
                        MessageBox.Show(showString.ToString(), "Added Event", MessageBoxButton.OK);
                        saveReport.IsEnabled = false;


                    }
                    else if (result == MessageBoxResult.No)
                    {
                        
                    }

                }
            }
        
        }

        //extract coordinates from file 
        public GeoLocation getCordFromPhoto(String photoUri)
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
