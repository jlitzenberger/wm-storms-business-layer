using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WM.STORMS.BusinessLayer.Models
{
    public class DamageReport
    {
        public long WorkRequest { get; set; }
        public int WitnessSequenceNumber { get; set; }
        public string WitnessName { get; set; }
        public string WitnessAddress { get; set; }
        public string WitnessCity { get; set; }
        public string WitnessState { get; set; }
        public string WitnessZip { get; set; }
        public string WitnessPhone { get; set; }

        public DamageReport()
        {
        }
    }
}
