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
        private static Random rnd = new Random();
        public static int GetRandomInt() => rnd.Next();
        private static bool JohannesPC => (Environment.MachineName == "DESKTOP-DIN7DPC");
        public static string ConnString
        {
            get
            {
                if (JohannesPC)
                    return @"Server=DESKTOP-DIN7DPC\SQLEXPRESS; Database=PicDB; Trusted_Connection=True;";
                else return @"Server=STEFFE-PC\SQLEXPRESS; Database=PicDB; Trusted_Connection=True;";
            }
        }


        public static string DeployPath => (JohannesPC) ? @"O:\GIT\SWE2-CS" : @"C:\Users\Steffe\Desktop\FH\4.Semester\PDB3";

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
                /*var name = Environment.MachineName;
                return !(name == "STEFFE-PC" || name == "DESKTOP-DIN7DPC");*/
            }
        }

        //private static string _workingDirectory;

        public static string PicPath => Path.Combine(DeployPath, "Pictures");
        public static string IcoPath => Path.Combine(DeployPath, "Icons");
    }
}
