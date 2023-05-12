using System;
using System.Collections.Generic;
using System.Text;

namespace App.Response
{
    public class tokenUpdateResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public Result_ result { get; set; }
    }
    public class Result_
    {
        public string value { get; set; }
        public List<object> formatters { get; set; }
        public List<object> contentTypes { get; set; }
        public object declaredType { get; set; }
        public int statusCode { get; set; }
    }
}
