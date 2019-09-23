using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WM.STORMS.BusinessLayer.Models
{
    public class CuJobCode
    {
        public string CD_JOB { get; set; }
        public string CD_CU { get; set; }
        public string NO_POINT { get; set; }
        public string NO_POINT_SPAN { get; set; }
        public string CD_USAGE { get; set; }
        public string IND_ACCT { get; set; }
        public string IND_ACTION { get; set; }
        public string IND_ON_OFF { get; set; }
        public Nullable<decimal> QT_ACTION { get; set; }
        public string CD_FACILITY { get; set; }
        public Nullable<long> CD_JOBCDPACKET { get; set; }
        public Nullable<decimal> HR_LBR { get; set; }
        public string CD_CREW_CLASS { get; set; }
        public string CD_MU { get; set; }
    }
}
