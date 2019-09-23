using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WM.STORMS.BusinessLayer.Models
{
    public class Facility
    {
        public string FacilityName { get; set; }
        public string FacilityDescription { get; set; }
        public string FlagActive { get; set; }
        public string FlagCollectedByQty { get; set; }
        public DateTime LastChanged { get; set; }
        public string TPFacility { get; set; }

   //     public List<FacilityAttribute> FacilityAttributes { get; set; }

        public Facility()
        {
            //FacilityAttributes = new List<FacilityAttribute>();
        }
    }
}
