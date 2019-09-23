using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WM.STORMS.DataAccessLayer;

namespace WM.STORMS.BusinessLayer.Models
{
    public class LeakGeneral
    {
        public long WorkRequest { get; set; }
        public long SequenceCode { get; set; }
        public string LeakFoundIndicator { get; set; }
        public string LeakGradeCode { get; set; }
        public string MainValveNumber { get; set; }

        public LeakGeneral() { }    
    }
}
