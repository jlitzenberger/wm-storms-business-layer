using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WM.STORMS.BusinessLayer.Models
{
    public class PremiseMore
    {
        public long WorkRequestId { get; set; }
        public string District { get; set; }
        public string PremiseId { get; set; }
        public string ServiceId { get; set; }

        public string number_in_household { get; set; }
        public string building_sq_footage { get; set; }
        public string heatload { get; set; }
        public string meter_size { get; set; }
        public string furnace_boiler_efficiency { get; set; }
        public string baseload { get; set; }
        public string insulation_package { get; set; }
        public string delivery_pressure { get; set; }
    }
}
