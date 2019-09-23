using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WM.STORMS.DataAccessLayer;

namespace WM.STORMS.BusinessLayer.Models
{
    public class CU
    {
        public string WorkRequest { get; set; }
        public string UnitAction { get; set; }
        public string Account { get; set; }
        public string UnitCode { get; set; }
        public string MUID { get; set; }
        public string DesignNumber { get; set; }
        public string PointNumber { get; set; }
        public string PointSpanNumber { get; set; }
        public string Usage { get; set; }
        public string OnOff { get; set; }
        public string EquipNumber { get; set; }
        public string SupplyMethod { get; set; }
        public string Quantity { get; set; }
        public string CrewClass { get; set; }
        public string District { get; set; }
        public string RestorationFlag { get; set; }
        public string MaterialSubExistsFlag { get; set; }
        public UnitDetail UnitDetail { get; set; }
        public List<CUFacilityAttribute> CUFacilityAttributes { get; set; }
        //public List<MaterialItem> MaterialItems { get; set; }
        public List<LaborDetail> LaborDetails { get; set; }
        public long? WorkpacketId { get; set; }

        public CU()
        {
            //UnitDetail = new UnitDetail();
            //CUFacilityAttributes = new List<CUFacilityAttribute>();
            //MaterialItems = new List<MaterialItem>();
            //LaborDetails = new List<LaborDetail>();
        }

    }
}
