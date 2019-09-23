using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WM.STORMS.BusinessLayer.Models
{
    public class Equipment
    {
        public string EquipmentId { get; set; }
        public string EquipmentDescription { get; set; }
        public string CustomerType { get; set; }
        public bool Active { get; set; }

        public Equipment()
        {
        }
    }
}
