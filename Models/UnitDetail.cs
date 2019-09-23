using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WM.STORMS.DataAccessLayer;

namespace WM.STORMS.BusinessLayer.Models
{
    public class UnitDetail
    {
        public string Name { get; set; }
        public string CD_CU_KIND { get; set; }
        public string CD_CU_MAKE { get; set; }
        public string CD_LBR { get; set; }
        public string CD_UOM { get; set; }
        public string CD_USAGE { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> DiscontinuedDate { get; set; }
        public Nullable<System.DateTime> EffectiveDate { get; set; }
        public Nullable<decimal> FC_LBR_HRS { get; set; }
        public string FG_CAPITAL { get; set; }
        public string FG_MAINTENANCE { get; set; }
        public string FG_OPERATIONS { get; set; }
        public string FG_RESTORATION { get; set; }
        public string FG_TEMPORARY { get; set; }
        public string CUMUIndicator { get; set; }
        public string IND_UTIL { get; set; }
        public string TP_ASSET { get; set; }
        public string TP_CU { get; set; }
        public string CD_SPEC { get; set; }
        public string CD_FACILITY { get; set; }
        public string FG_HR_CHANGED { get; set; }
        public System.DateTime TS_LAST_CHANGED { get; set; }
        public Nullable<decimal> AMT_MATL_ITEM_TOT { get; set; }
        public Nullable<decimal> AMT_SALVAGE_TOT { get; set; }
        public Nullable<decimal> AMT_SCRAP_TOT { get; set; }
        public string CD_CATEGORY { get; set; }
        public string IND_ACCT { get; set; }
        public string IND_ACTION { get; set; }
        public string FG_HIDE_CU { get; set; }
        public string CD_BID_ITEM { get; set; }
        public string FG_MOBILE { get; set; }
        //public string CD_CREW_CLASS { get; set; }

        public List<CUStructure> CUStructures { get; set; }
        public Facility Facility { get; set; }
        //public List<LaborDetail> LaborDetails { get; set; }

        public UnitDetail()
        {
            //CUStructures = new List<CUStructure>();
            //Facility = new Facility();
            //LaborDetails = new List<LaborDetail>();
        }
    }
}
