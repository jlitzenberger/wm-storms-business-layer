using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WM.STORMS.BusinessLayer.Models
{
    public partial class IFExtDsgnCU
    {
        public string CUCode { get; set; }
        public string AccountIndicator { get; set; }
        public string OnOffIndicator { get; set; }        
        public string UsageCode { get; set; }
        public string SupplyMethodCode { get; set; }
        public string EquipmentNumber { get; set; }        
        public Nullable<System.DateTime> CompletionDate { get; set; }
        public string ActionIndicator { get; set; }
        public decimal ActionQuantity { get; set; }         
        public string MUCode { get; set; }              
        public long ExternalDesignSequence { get; set; }
        public System.DateTime ExternalDesignTimeStamp { get; set; }
        public string OperatorId { get; set; }
        public string PointNumber { get; set; }
        public string PointSpanNumber { get; set; }
        public string District { get; set; }
        public long WorkRequest { get; set; }   
        public string ProcessIndicator { get; set; }
        public Nullable<long> Workpacket { get; set; }        
        public string CrewCode { get; set; }
        public string ErrorFlag { get; set; }
        public Nullable<long> ErrorRunSequence { get; set; }
        public string ContractorFlag { get; set; }
        public Nullable<long> FacilityNumber { get; set; }
       

        public List<IFExtDsgnFacAtt> IfExtDsgnFacAtt { get; set; }

        public IFExtDsgnCU()
        {
            
        }
    }
}
