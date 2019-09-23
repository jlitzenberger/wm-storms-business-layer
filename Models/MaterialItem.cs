using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WM.STORMS.DataAccessLayer;

namespace WM.STORMS.BusinessLayer.Models
{
    public class MaterialItem
    {
        public string NO_MATL_ITEM { get; set; }
        public decimal AMT_MATL_ITEM { get; set; }
        public decimal AMT_SALVAGE { get; set; }
        public decimal AMT_SCRAP { get; set; }
        public string CD_MATL_CLASS { get; set; }
        public string CD_UOM { get; set; }
        public string DS_MATL_ITEM { get; set; }
        public int? DY_LEAD_TIME { get; set; }
        public string FG_ACTIVE { get; set; }
        public string FG_PREV_CAPITALIZE { get; set; }
        public string FG_STOCK_ITEM { get; set; }
        public decimal PC_ALLOWANCE { get; set; }
        public decimal QT_MIN { get; set; }
        public string FG_TRUCK_STOCK { get; set; }
        public string FG_MAJOR { get; set; }
        public string FG_ASSET { get; set; }
        public string CD_STOCKING { get; set; }
        public DateTime TS_LAST_CHANGED { get; set; }

        public MaterialItem()
        {
        }

    }
}
