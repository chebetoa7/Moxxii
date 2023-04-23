using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavis.UriTemplates;

namespace Moxxii.mobile.Models.Response
{
    public class ResponseToken
    {
        public bool success { get; set; }
        public string message { get; set; }
        public Result result { get; set; }
    }
    public class Result
    {
        public Usuario usuario { get; set; }
        public string token { get; set; }
    }
    public class Usuario
    {
        public int id { get; set; }
        public string disponibility { get; set; }
        public object viajes { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public string otherLastName { get; set; }
        public string birthdate { get; set; }
        public int dateAge { get; set; }
        public string sex { get; set; }
        public string address { get; set; }
        public string country { get; set; }
        public string typeUser { get; set; }
        public bool status { get; set; }
        public string metadata { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}
