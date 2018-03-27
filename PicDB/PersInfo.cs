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

        private static string _workingDirectory = null;

        public static string DeployPath {
            get { return _workingDirectory ?? (_workingDirectory = System.Environment.CurrentDirectory); }
        }

    public static string PicPath = Path.Combine(DeployPath, "Pictures");
    public static string IcoPath = Path.Combine(DeployPath, "Icons");
    }
}
