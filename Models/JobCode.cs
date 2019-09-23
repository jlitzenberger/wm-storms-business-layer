using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WM.STORMS.BusinessLayer.Models
{
    public class JobCode
    {
        public string CD_JOB { get; set; }
        public string DS_JOB { get; set; }
        public Nullable<System.DateTime> DT_DISCONTINUED { get; set; }
        public Nullable<System.DateTime> DT_EFFECTIVE { get; set; }
        public Nullable<int> DY_PRIOR { get; set; }
        public Nullable<long> DY_TO_REINITIATE { get; set; }
        public Nullable<decimal> HR_EST_TIME { get; set; }
        public string TP_JOB { get; set; }
        public string FG_FAC_DESIGN { get; set; }
        public string FG_FAC_ASBUILT { get; set; }
        public string FG_CHANGE_EST_TIME { get; set; }
        public string CD_EXC_MTHD { get; set; }
        public string IND_INITIATION { get; set; }
        public string FG_AUTO_JOB_COST { get; set; }
        public string TP_WORKS { get; set; }
        public Nullable<short> DY_SERVICE_STD { get; set; }
        public string FG_MOBILE_INIT { get; set; }
        public Nullable<short> QT_OFFSET { get; set; }
        public string CD_APPTPROFILE { get; set; }
        public Nullable<long> ID_CAPABILITY { get; set; }

        public List<CuJobCode> CuJobCodes { get; set; }
    }
}
