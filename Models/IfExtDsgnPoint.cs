using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WM.STORMS.BusinessLayer.Models
{
    public partial class IFExtDsgnPoint
    {
        public long ExternalDesignSequence { get; set; }
        public DateTime ExternalDesignTimeStamp { get; set; }
        public string OperatorId { get; set; }
        public string PointNumber { get; set; }
        public string PointSpanNumber { get; set; }
        public string District { get; set; }
        public long WorkRequest { get; set; }       
        public string WorkStatusIndicator { get; set; }
        public string MainStatusIndicator { get; set; }
        public string ProcessIndicator { get; set; }
        public string EntityCode { get; set; }     
        public Nullable<decimal> SpanLength { get; set; }
        public string PointAd { get; set; }
        public string PointId { get; set; }
        public string CrewCode { get; set; }
        public string ErrorFlag { get; set; }        
        public Nullable<long> ErrorRunSequence { get; set; }
        public string ContractorCalcInd { get; set; }       
        public string BidItemCode { get; set; }
        public string RWorksFlag { get; set; }     

     //   public List<IFExtDsgnCU> IfExtDsgnCU { get; set; }

        public IFExtDsgnPoint()
        {
            //CUs = new List<CU>();
        }
    }

}

