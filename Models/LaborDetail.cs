using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WM.STORMS.DataAccessLayer;

namespace WM.STORMS.BusinessLayer.Models
{
    public class LaborDetail
    {
        public string LaborCode { get; set; }
        public string IndOnOff { get; set; }
        public string IndAction { get; set; }
        public string CrewClass { get; set; }
        public string FlagActive { get; set; }
        public decimal LaborHours { get; set; }
        public decimal AdditionalLaborHours { get; set; }

        public LaborDetail() { }

    }
}
