using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Droid.Service
{
    public class FileAccess
    {
        public static string GetLocalFilePath(string FileName)
        {
            string path = System.Environment.GetFolderPath(
                System.Environment.SpecialFolder.Personal
                );
            return System.IO.Path.Combine(path, FileName);
        }
    }
}