using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WM.STORMS.BusinessLayer.Models
{
    public class WeTWorkAlloc
    {
        public long CD_WORKPACKET { get; set; }
        public System.DateTime DT_WORK { get; set; }
        public int ID_CREW { get; set; }
        public long CD_WR { get; set; }
        public System.DateTime DT_SCHED { get; set; }
        public Nullable<decimal> HR_ALLOC { get; set; }
        public Nullable<decimal> HR_LBL_REM { get; set; }
        public Nullable<decimal> HR_LBR { get; set; }
        public string FG_SENTSTORMS { get; set; }
        public string FG_DESCHEDULE { get; set; }
        public string ID_CREWRESCHED { get; set; }

        public WeTWorkAlloc()
        {          
        }
    }
}
