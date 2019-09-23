using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WM.STORMS.BusinessLayer.Models
{
    public class ExceptionCondition
    {
        public string District { get; set; }
        public long WorkRequestId { get; set; }
        public string ConditionDescription { get; set; }
        public long ConditionCode { get; set; }
        public string OperatorId { get; set; }     
        public System.DateTime ExceptionConditionTimeStamp { get; set; }
        public long SequenceNumber { get; set; }

        public ExceptionCondition()
        {
        }
    }
}
