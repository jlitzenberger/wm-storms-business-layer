using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WM.STORMS.DataAccessLayer;

namespace WM.STORMS.BusinessLayer.Models
{
    public class CUAsb
    {
        public long WorkRequestId { get; set; }
        public string UnitAction { get; set; }
        public string Account { get; set; }
        public string UnitCode { get; set; }
        public string MuId { get; set; }
        public string PointNumber { get; set; }
        public string PointSpanNumber { get; set; }
        public string Usage { get; set; }
        public string OnOff { get; set; }
        public string EquipNumber { get; set; }
        public string SupplyMethod { get; set; }
        public string Quantity { get; set; }
        public UnitDetail UnitDetail { get; set; }
        public string CrewClass { get; set; }
        public string District { get; set; }     
        public string WorkOrderCode { get; set; }
        public Nullable<decimal> FacilityLaborHours { get; set; }
        public string RestorationFlag { get; set; }
        public string FacilityCode { get; set; }
        public DateTime TimeStampLastChanged { get; set; }
        public string OrderNumber { get; set; }
        public Nullable<short> AsbuiltDesignNumber { get; set; }
        public long CUId { get; set; }
        public long? WorkpacketId { get; set; }
        public Nullable<decimal> ConstructionLaborHours { get; set; }
        public Nullable<decimal> NonConstructionLaborHours { get; set; }
        public Nullable<decimal> ConstructionLaborAmount { get; set; }
        public Nullable<decimal> NonConstructionLaborAmount { get; set; }     
        public string BudgetItemCode { get; set; }
        
        public List<CUFacilityAttribute> CUFacilityAttributes { get; set; }

        public CUAsb()
        {
        }      
    }
}
