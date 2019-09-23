using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WM.STORMS.BusinessLayer.Models
{
    public class WorkRequestNonDesigned
    {
        public string CD_DIST { get; set; }
        public long CD_WR { get; set; }
        public string CD_JOB { get; set; }
        public string DS_JOB { get; set; }
        public string FG_REINITIATED { get; set; }
    }
}
