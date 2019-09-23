using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WM.STORMS.BusinessLayer.Models
{
    public partial class IFError
    {
        //public long CD_SEQ_ERROR { get; set; }
        //public Nullable<long> CD_SQLCODE { get; set; }
        //public string NM_INTERFACE { get; set; }
        //public System.DateTime TS_ERROR { get; set; }
        //public string TXT_SQLERRTEXT { get; set; }
        //public string TXT_IFERROR { get; set; }
        //public long CD_SEQ_ERROR_RUN { get; set; }
        //public string CD_DIST { get; set; }
        //public Nullable<long> CD_WR { get; set; }
        //public Nullable<long> CD_WORKPACKET { get; set; }
        //public string CD_PROJECT { get; set; }
        //public string CD_CREW { get; set; }
        //public string ID_EMPL { get; set; }
        //public string CD_FLEET { get; set; }
        //public string NM_TABLE { get; set; }
        //public string NM_COLUMN { get; set; }
        //public string FG_DATA_ERROR { get; set; }
        //public System.DateTime TS_ERROR_LOGGED { get; set; }

        public long CD_SEQ_ERROR { get; set; }
        public Nullable<long> CD_SQLCODE { get; set; }
        public string NM_INTERFACE { get; set; }
        public DateTime TS_ERROR { get; set; }
     // public string TS_ERROR { get; set; }
        public string TXT_SQLERRTEXT { get; set; }
        public string TXT_IFERROR { get; set; }
        public long CD_SEQ_ERROR_RUN { get; set; }
        public string CD_DIST { get; set; }
        public Nullable<long> CD_WR { get; set; }
        public Nullable<long> CD_WORKPACKET { get; set; }
        public string CD_PROJECT { get; set; }
        public string CD_CREW { get; set; }
        public string ID_EMPL { get; set; }
        public string CD_FLEET { get; set; }
        public string NM_TABLE { get; set; }
        public string NM_COLUMN { get; set; }
        public string FG_DATA_ERROR { get; set; }
        public System.DateTime TS_ERROR_LOGGED { get; set; }

        public IFError()
        {
        }
    }
}
