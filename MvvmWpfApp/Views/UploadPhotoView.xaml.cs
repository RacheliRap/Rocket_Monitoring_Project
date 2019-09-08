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
using MvvmWpfApp.Models;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;

namespace MvvmWpfApp.Views
{
    /// <summary>
    /// Interaction logic for UploadPhotoView.xaml
    /// </summary>
    public partial class UploadPhotoView : UserControl
    {
        public UploadPhotoView()
        {
            InitializeComponent();
        }
        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                UriTextBox.Text = new Uri(op.FileName).ToString();
                Uri uri = new Uri(@"C:\Users\Racheli\Pictures\yrtsa.jpg");
                imgPhoto.Source = new BitmapImage(uri);

                ActionResult(uri.ToString());

            }


        }

        private void saveReport_Click(object sender, RoutedEventArgs e)
        {
            ActionResult(UriTextBox.Text);
        }

        //extract coordinates from file 
        public PhotoCoordinatesModel ActionResult(String photoUri)
        {
            var directories = ImageMetadataReader.ReadMetadata("C:/Users/Racheli/Documents/final/windows-system-project-mvvm-master/MvvmWpfApp/Views/explosion1.jpg");

            var gps = directories.OfType<GpsDirectory>().FirstOrDefault();

            if (gps != null)
            {
                var location = gps.GetGeoLocation();

                if (location != null)
                    Console.WriteLine("Lat {0} Lng {1}", location.Latitude, location.Longitude);
            }
            var model = new PhotoCoordinatesModel();
            try
            {
                using (var reader = new ExifLib.ExifReader("C:/Users/Racheli/Documents/final/windows-system-project-mvvm-master/MvvmWpfApp/Views/explosion1.jpg"))
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
}
