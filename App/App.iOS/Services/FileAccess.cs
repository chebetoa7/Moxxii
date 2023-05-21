using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace App.iOS.Services
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