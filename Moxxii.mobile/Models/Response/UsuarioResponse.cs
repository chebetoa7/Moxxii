using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moxxii.mobile.Models.Response
{
    public partial class UsuarioResponse
    {
        public int id { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public string otherLastName { get; set; }
        public string birthdate { get; set; }
        public int dateAge { get; set; }
        public string sex { get; set; }
        public string address { get; set; }
        public string country { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}
