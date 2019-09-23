using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WM.STORMS.DataAccessLayer;

namespace WM.STORMS.BusinessLayer.Models
{
    public class Geo
    {
        public string Area { get; set; }
        public string Dist { get; set; }
        public int WorkRequest { get; set; } //Needs to be removed
        public string XCoordinate { get; set; }
        public string YCoordinate { get; set; }
        public string Zone { get; set; }

        public Geo() { }

    }
}
