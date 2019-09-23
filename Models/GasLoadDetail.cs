using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WM.STORMS.BusinessLayer.Models
{
    public class GasLoadDetail
    {
        public string CD_DIST { get; set; }
        public long CD_WR { get; set; }
        public string TP_CUSTOMER { get; set; }
        public string CD_DEL_PRES_PROPOSED { get; set; }
        public string TP_EQUIP { get; set; }
        public string CD_DEL_PRES_EXISTING { get; set; }
        public Nullable<decimal> QT_CONNECT_LOAD { get; set; }
        public Nullable<decimal> QT_CONN_LOAD_INC { get; set; }
        public Nullable<decimal> QT_TOT_EST_DEMAND { get; set; }
        public string DS_COMMENTS { get; set; }
    }
}
