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
        private static bool JohannesPC => 
            (Environment.MachineName == "DESKTOP-DIN7DPC");
        private static bool StefanPC => 
            (Environment.MachineName == "STEFFE-PC"); //TODO
        private static bool StefanLAPTOP => 
            (Environment.MachineName == "STEFFE"); //TODO

        public static string ConnString
        {
            get
            {
                if (JohannesPC)
                    return ""; //@"Server=DESKTOP-DIN7DPC\SQLEXPRESS; Database=PicDB; Trusted_Connection=True;";
                if (StefanPC)
                    return @"Server=STEFFE-PC\SQLEXPRESS; Database=PicDB; Trusted_Connection=True;"; //TODO
                if (StefanLAPTOP)
                    return "irgendeinPfadhalt"; //TODO

                else return "";
            }
        }


        public static string DeployPath
        {
            get
            {
                if (JohannesPC)
                    return @"O:\GIT\SWE2\SWE2-CS";
                if (StefanPC)
                    return @"C:\Users\Steffe\Desktop\FH\4.Semester\PDB3\";
                if (StefanLAPTOP)
                    return "PFADHALT"; //TODO

                else return Environment.CurrentDirectory;
            }
        }

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
