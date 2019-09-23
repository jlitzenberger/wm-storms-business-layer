using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WM.STORMS.BusinessLayer.Models
{
    public class OperatorPassword
    {
        public string IdOper { get; set; }
        public string Password { get; set; }
        public string ChangedAtLogin { get; set; }
        public DateTime DateChanged { get; set; }

    }
}
