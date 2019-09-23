using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WM.STORMS.BusinessLayer.Models
{
    public class WorkPacketHistory
    {
        public long WorkPacketId { get; set; }
        public long Sequence { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
        public string RecordedOperator { get; set; }
        public DateTime DateTimeRecorded { get; set; }
        public DateTime? DateTimeStatusReached { get; set; }
        public DateTime? DateTimeStatusEndEstimate { get; set; }

        public WorkPacketHistory()
        {
        }
    }
}
