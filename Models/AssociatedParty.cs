using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WM.STORMS.DataAccessLayer;

namespace WM.STORMS.BusinessLayer.Models
{
    public class AssociatedParty
    {
        public long WorkRequestId { get; set; }
        public long Seq { get; set; }
        public Address Address { get; set; }
        public Phones Phones { get; set; }
        public string EntityType { get; set; }
    }
}
