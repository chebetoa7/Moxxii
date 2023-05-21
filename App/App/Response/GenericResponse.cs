using System;
using System.Collections.Generic;
using System.Text;

namespace App.Response
{
    public partial class GenericResponse
    {
        public bool sucess { get; set; }
        public string message { get; set; }
        public Result_generic result { get; set; }
    }
    public class Result_generic
    {
        public int statusCode { get; set; }
    }
}
