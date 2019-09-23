using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WM.STORMS.DataAccessLayer;

namespace WM.STORMS.BusinessLayer.Models
{
    public class CUFacilityAttribute
    {    
        public long WorkRequestId { get; set; }
        public string CuName { get; set; }
        public string AttributeName { get; set; }
        public string PointNumber { get; set; }
        public string PointSpan { get; set; }
        public string Usage { get; set; }
        public string SupplyMethod { get; set; }
        public string Account { get; set; }
        public string Action { get; set; }
        public string OnOff { get; set; }
        public string Equip { get; set; }
        public string MuId { get; set; }
        public long IdSeq { get; set; }
        public string Value { get; set; }
        public string Facility { get; set; }
        public string RequiredFlag { get; set; }
        public string District { get; set; }
        public long IdCU { get; set; }
        public CUFacilityAttribute() { }
    }
}
