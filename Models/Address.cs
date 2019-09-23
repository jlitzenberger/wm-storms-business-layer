using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WM.STORMS.DataAccessLayer;
using WM.STORMS.BusinessLayer;

namespace WM.STORMS.BusinessLayer.Models
{
    public class Address
    {
        //public int WorkRequest { get; set; } // Needs to be removed
        public string Organization { get; set; }
        public string Building { get; set; }
        public string Floor { get; set; }
        public string StreetNumber { get; set; }
        public string StreetPrefix { get; set; }
        public string StreetName { get; set; }
        public string StreetType { get; set; }
        public string StreetSuffix { get; set; }
        public string UnitID { get; set; }
        public string UnitType { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        //public string JobNm { get; set; } //Needs to be removed
        public string ExtraInfo { get; set; }
        public string FreeFormat { get; set; }
        public string CityCode { get; set; }

        public Address()
        {

        }
    }
}
