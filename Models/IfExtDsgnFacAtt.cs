using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WM.STORMS.BusinessLayer.Models
{
    public partial class IFExtDsgnFacAtt
    {  
        public long FacilityNumber { get; set; }
        public long SequenceCode { get; set; }
        public string FacilityValueText { get; set; }
        public string AttributeCode { get; set; }       
        public long ExternalDesignSequence { get; set; }
        public System.DateTime ExternalDesignTimeStamp { get; set; }
        public string OperatorId { get; set; }        
        public string ErrorFlag { get; set; }        
        public Nullable<long> ErrorRunSequence { get; set; }                

        public IFExtDsgnFacAtt()
        {
           
        }
    }
}
