using System;
using System.Collections.Generic;
using System.Text;

namespace App.Response
{
    public class SolicitudNuevoViajeResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public solicitudResponse result { get; set; }
    }

    public class solicitudResponse
    {
        public int id { get; set; }
        public int idPasajero { get; set; }
        public int idConductor { get; set; }
        public double latInitial { get; set; }
        public double longInitial { get; set; }
        public double latEnd { get; set; }
        public double longEnd { get; set; }
        public string city { get; set; }
        public string dictric { get; set; }
        public bool status { get; set; }
        public string confirmationStatus { get; set; }
    }
}
