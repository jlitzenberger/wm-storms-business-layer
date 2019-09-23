using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WM.STORMS.DataAccessLayer;

namespace WM.STORMS.BusinessLayer.Models
{
    public class CUStructure
    {       
        public string CD_CU { get; set; }
        public string NO_MATL_ITEM { get; set; }
        public decimal PC_ALLOWANCE { get; set; }
        public decimal QT_ITEM { get; set; }
        public string CD_DRAWING_SEQ { get; set; }
        public DateTime TS_LAST_CHANGED { get; set; }
        public MaterialItem MaterialItem { get; set; }

        public CUStructure()
        {
            //MaterialItem = new MaterialItem();
        }
    }
}
