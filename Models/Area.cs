using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WM.STORMS.BusinessLayer.Models
{
    public class Area {

        public List<Zone> Zones { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}