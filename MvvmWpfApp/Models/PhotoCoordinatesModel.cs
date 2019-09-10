﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmWpfApp.Models
{
    public class PhotoCoordinatesModel
    {
        public double? Lat { get; set; }
        public double? Lon { get; set; }
        public string Error { get; set; }

        public bool HasValidCoordinates()
        {
            return Lat.HasValue && Lon.HasValue;
        }
    }
}
