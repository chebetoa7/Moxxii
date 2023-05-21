using App.Models.Body;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Response
{
    public partial class UsuarioResponse
    {
        public int id { get; set; }
        public string disponibility { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public string otherLastName { get; set; }
        public string birthdate { get; set; }
        public int dateAge { get; set; }
        public string sex { get; set; }
        public string address { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string distric { get; set; }
        public int ubicationRealLat { get; set; }
        public int ubicationRealLon { get; set; }
        public string typeUser { get; set; }
        public bool status { get; set; }
        public string metadata { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string tokenfirebase { get; set; }
        public string dataPushUpdate { get; set; }
        public bool loginNow { get; set; }
    }
}
