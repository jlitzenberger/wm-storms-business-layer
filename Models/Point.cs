using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WM.STORMS.DataAccessLayer;

namespace WM.STORMS.BusinessLayer.Models
{
    public class Point
    {
        public string WorkRequest { get; set; }
        public string DesignNumber { get; set; }
        public string PointID { get; set; }
        public string PointNumber { get; set; }
        public string PointSpanNumber { get; set; }
        public string Length { get; set; }
        public string District { get; set; }
        public string MainStatusIndicator { get; set; }
        public string RestorationFlag { get; set; }
        public List<CU> CUs { get; set; }

        public Point()
        {
            //CUs = new List<CU>();
        }
    }
}
