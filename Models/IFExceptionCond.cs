using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WM.STORMS.BusinessLayer.Models
{
    public class IFExceptionCond
    {
        public string CD_DIST { get; set; }
        public long CD_WR { get; set; }
        public string DS_COND { get; set; }
        public string ID_OPER { get; set; }
        public string FG_ERROR { get; set; }
        public System.DateTime TS_IFEXCEPTIONCOND { get; set; }
        public long CD_SEQ { get; set; }
        public Nullable<long> CD_SEQ_ERROR_RUN { get; set; }

        public IFExceptionCond()
        {
        }
    }
}

 