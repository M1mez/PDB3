using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE2.Interfaces.Models;
using PicDB.Classes;
using PicDB.Layers_DA;

namespace PicDB.Models
{
    class PhotographerModel : IPhotographerModel
    {
        public PhotographerModel()
        {
        }

        public int ID { get; set; }
        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                ID = Constants.GetRandomInt();
            }
        }
        public string LastName { get; set; }
        public DateTime? BirthDay { get; set; } = null;
        public string Notes { get; set; }
    }
}
