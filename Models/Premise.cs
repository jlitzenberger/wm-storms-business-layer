using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WM.STORMS.DataAccessLayer;

namespace WM.STORMS.BusinessLayer.Models
{
    public class Premise
    {
        public string CD_DIST { get; set; }
        public long CD_WR { get; set; }
        public string ID_PREMISE { get; set; }
        public string CD_SIC { get; set; }
        public string CD_RATE_CLASS { get; set; }
        public string CD_REVENUE_CLASS { get; set; }
        public Nullable<long> CD_ADDRESS { get; set; }
        public string FG_HAZARD { get; set; }
        public string FG_KEY_AVAIL { get; set; }
        public Nullable<decimal> QT_BASELOAD { get; set; }
        public Nullable<decimal> QT_HEATING_FACTOR { get; set; }
        public string ID_SERVICE { get; set; }

        public PremiseMore PremiseMore { get; set; }

        public Premise()
        {

        }
    }
}
