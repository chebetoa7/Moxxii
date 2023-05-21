using System;
using System.Collections.Generic;
using System.Text;

namespace App.Response
{
    public class DataResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public Usuario usuario { get; set; }
    }
}
