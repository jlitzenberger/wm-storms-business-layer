using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WM.STORMS.DataAccessLayer;

namespace WM.STORMS.BusinessLayer.Models
{
    public class PointAsb
    {       
        public long WorkRequestId { get; set; }       
        public string PointID { get; set; }
        public string PointNumber { get; set; }
        public string PointSpanNumber { get; set; }
        public string Length { get; set; }
        public string MainStatusIndicator { get; set; }
        public string District { get; set; }
        public Nullable<short> AsbuiltDesignNumber { get; set; }
        public DateTime TimeStampLastChanged { get; set; }
                
        public List<CUAsb> CUAsbs { get; set; }

        public PointAsb()
        {
            //CUs = new List<CU>();
        }
    }
}
