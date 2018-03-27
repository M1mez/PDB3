using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE2.Interfaces.Models;
using PicDB.Classes;

namespace PicDB.Models
{
    class PhotographerModel : IPhotographerModel
    {
        public PhotographerModel()
        {
                ID = DAL_Conn.GetNextId("Photographer");
                Console.WriteLine(ID);
        }

        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDay { get; set; } = null;
        public string Notes { get; set; }
    }
}
