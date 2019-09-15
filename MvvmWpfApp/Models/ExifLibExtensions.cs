using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExifLib;
using MvvmWpfApp.Models;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;

namespace MvvmWpfApp.Models
{
    public static class ExifLibExtensions
    {
        public static double? GetLatitude(this ExifLib.ExifReader reader)
        {
            return reader.GetCoordinate(ExifTags.GPSLatitude);
        }

        public static double? GetLongitude(this ExifLib.ExifReader reader)
        {
            return reader.GetCoordinate(ExifTags.GPSLongitude);
        }

        private static double? GetCoordinate(this ExifLib.ExifReader reader, ExifTags type)
        {
            double[] coordinates;
            if (reader.GetTagValue(type, out coordinates))
            {
                return ToDoubleCoordinates(coordinates);
            }

            return null;
        }

        private static double ToDoubleCoordinates(double[] coordinates)
        {
            return coordinates[0] + coordinates[1] / 60f + coordinates[2] / 3600f;
        }
    }
}
