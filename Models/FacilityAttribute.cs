using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WM.STORMS.BusinessLayer.Models
{
    public class FacilityAttribute
    {
        public string AttributeName { get; set; }
        public string FacilityName { get; set; }
        public string Action { get; set; }
        public string Validation { get; set; }
        public string FlagActive { get; set; }
        public string IDSeq { get; set; }
        public DateTime LastChanged { get; set; }

        public FacilityAttribute()
        {
        }
    }
}
