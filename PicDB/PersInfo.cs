using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;

namespace PicDB
{
    public static class PersInfo
    {
        public static string ConnString = 
                @"Server=DESKTOP-DIN7DPC\SQLEXPRESS;
                Database=PicDB;
                Trusted_Connection=True;";

        private static string _workingDirectory;
        public static string DeployPath => _workingDirectory ?? (_workingDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SWE2-CS"));

        public static string PicPath => Path.Combine(DeployPath, "Pictures");
        public static string IcoPath => Path.Combine(DeployPath, "Icons");
    }
}
