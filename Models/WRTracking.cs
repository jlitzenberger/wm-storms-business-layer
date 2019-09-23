using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WM.STORMS.BusinessLayer.Models {
    public class WRTracking {
        public string District { get; set; }
        public string Status { get; set; }
        public long WorkRequestId { get; set; }
        public string OperatorId { get; set; }
        public string Function { get; set; }
        public string TypeOfChange { get; set; }
        public System.DateTime ChangeDate { get; set; }
        public long? id { get; set; }
    }
}
