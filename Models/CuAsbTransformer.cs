using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WM.STORMS.BusinessLayer.Models
{
    public class CuAsbTransformer
    {
        public long WorkRequestId { get; set; }      
        public string PointNumber { get; set; }
        public string PointSpanNumber { get; set; }
        public int? Quanity { get; set; }
        public UnitDetail UnitDetail { get; set; }
    }
}
