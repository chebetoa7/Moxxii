using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace App.Models.Maps
{
    public class Place
    {
        public Location location { get; set; }
        public string address { get; set; }
        public string description { get; set; }
    }
}
