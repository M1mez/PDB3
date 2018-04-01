using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;

namespace PicDB
{
    public static class Constants
    {

        private static bool JohannesPC => (Environment.MachineName == "DESKTOP-DIN7DPC");
        public static string ConnString
        {
            get
            {
                if (JohannesPC)
                    return @"Server=DESKTOP-DIN7DPC\SQLEXPRESS; Database=PicDB; Trusted_Connection=True;";
                else return "";
            }
        }


        public static string DeployPath => (JohannesPC) ? @"O:\GIT\SWE2-CS" : "STEFANPFAD";

        /// <summary>
        /// returns Bool if the program is executed via UnitTest
        /// </summary>
        public static bool IsUnitTest
        {
            get
            {
                var isUnitTest = AppDomain.CurrentDomain.GetAssemblies().Any(
                    a => a.FullName.ToLowerInvariant().StartsWith("nunit.framework"));
                return isUnitTest;
            }
        }

        //private static string _workingDirectory;

        public static string PicPath => Path.Combine(DeployPath, "Pictures");
        public static string IcoPath => Path.Combine(DeployPath, "Icons");
    }
}
