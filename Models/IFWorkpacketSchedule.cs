using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WM.STORMS.BusinessLayer.Models
{
    public class IFWorkpacketSchedule
    {
        public System.DateTime TimeStamp { get; set; }
        public long ErrorSequenceCode { get; set; }
        public string CrewId { get; set; }
        public long WorkRequest { get; set; }
        public string District { get; set; }
        public string CrewClassCode { get; set; }
        public System.DateTime ScheduleDate { get; set; }
        public string ErrorFlag { get; set; }
        public Nullable<decimal> RemainingHours { get; set; }
        public string RescheduleHoldFlag { get; set; }
        public long WorkPacket { get; set; }
        public Nullable<System.DateTime> WorkDate { get; set; }
        public Nullable<long> ErrorRunSequenceCode { get; set; }
        public string CrewMustDoFlag { get; set; }

        public IFWorkpacketSchedule()
        {
        }
    }
}
