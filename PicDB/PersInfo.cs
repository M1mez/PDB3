using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PicDB
{
    public static class PersInfo
    {
        public static string ConnString = 
                @"Server=DESKTOP-DIN7DPC\SQLEXPRESS;
                Database=PicDB;
                Trusted_Connection=True;";

        public static string DeployPath =
            @"O:\GIT\SWE2\SWE2-CS\deploy";

        public static string PicPath =
            @"O:\GIT\SWE2\SWE2-CS\deploy\Pictures";
    }
}
