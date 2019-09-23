using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WM.STORMS.DataAccessLayer;

namespace WM.STORMS.BusinessLayer.Models
{
    public class CUEst
    {
        public string CD_DIST { get; set; }
        public long CD_WR { get; set; }
        public short NO_DESIGN { get; set; }
        public string NO_POINT { get; set; }
        public string NO_POINT_SPAN { get; set; }
        public string CD_CU { get; set; }
        public string CD_USAGE { get; set; }
        public string IND_ACCT { get; set; }
        public string IND_ON_OFF { get; set; }
        public string NO_EQUIP { get; set; }
        public string IND_ACTION { get; set; }
        public string CD_SUPPLY_METHOD { get; set; }
        public string CD_MU { get; set; }
        public string CD_BDGT_ITEM { get; set; }
        public string CD_OWNERSHIP { get; set; }
        public string CD_WO { get; set; }
        public Nullable<decimal> FC_LBR_HRS { get; set; }
        public string FG_RESTORATION { get; set; }
        public Nullable<decimal> QT_ACTION { get; set; }
        public string FG_MATL_SUB_EXISTS { get; set; }
        public string CD_FACILITY { get; set; }
        public string CD_CREW_CLASS { get; set; }
        public string NO_ORDER { get; set; }
        public Nullable<long> CD_WORKPACKET { get; set; }
        public Nullable<long> ID_ACTIVITY { get; set; }
        public long ID_CU { get; set; }
        public Nullable<decimal> HR_LBR_CONSTRUCT { get; set; }
        public Nullable<decimal> HR_LBR_NON_CONSTR { get; set; }
        public Nullable<decimal> AMT_LBR_CONSTRUCT { get; set; }
        public Nullable<decimal> AMT_LBR_NON_CONSTR { get; set; }
        public Nullable<decimal> AMT_MATL { get; set; }
        public Nullable<decimal> AMT_MATL_PREV_CAP { get; set; }
        public Nullable<decimal> AMT_MATL_TRK_STK { get; set; }
        public Nullable<decimal> AMT_MATL_SCRAP { get; set; }
        public Nullable<decimal> AMT_MATL_SALVAGE { get; set; }
        public List<CUFacilityAttribute> CUFacilityAttributes { get; set; }

     //   public virtual TWMCU TWMCU { get; set; }
     //   public virtual ICollection<TWMWRFACILITY> TWMWRFACILITies { get; set; }

         public CUEst()
        {
            //UnitDetail = new UnitDetail();
       //     CUFacilityAttributes = new List<CUFacilityAttribute>();
            //MaterialItems = new List<MaterialItem>();
            //LaborDetails = new List<LaborDetail>();
        }



    }
}
