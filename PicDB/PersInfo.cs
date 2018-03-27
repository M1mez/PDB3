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

        public static string DeployPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static string PicPath = DeployPath + @"\Pictures";
    }
}
