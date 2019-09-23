using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WM.STORMS.BusinessLayer.Models
{
    public class IFMobileCompletion
    {
        public string District { get; set; }
        public long WorkRequest { get; set; }
        public Nullable<System.DateTime> CompletionDate { get; set; }
        public string CompletingCrewId { get; set; }
        public string ErrorFlag { get; set; }
        public System.DateTime TimeStampMobileCompletion { get; set; }
        public long WorkPacket { get; set; }
        public string ResolutionCode { get; set; }
        public long SequenceCode { get; set; }
        public string JobCode { get; set; }
        public string JobType { get; set; }
        public Nullable<long> ErrorRunSequenceCode { get; set; }

        public IFMobileCompletion()
        {
        }
    }
}
