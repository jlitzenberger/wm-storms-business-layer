using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WM.STORMS.BusinessLayer.Models
{
    public class MUStructure
    {
        public string CD_CU { get; set; }
        public string CD_MU { get; set; }
        public string CD_USAGE { get; set; }
        public decimal QT_CU { get; set; }
        public string CD_DRAWING_SEQ { get; set; }
        public long CD_SEQ { get; set; }
        public long CD_MUSTRUCTURE { get; set; }
        public string IND_ACCT { get; set; }
        public string IND_ACTION { get; set; }

      //  public UnitDetail UnitDetail { get; set; }

        public MUStructure()
        {
        }
    }
}
