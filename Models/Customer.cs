using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WM.STORMS.DataAccessLayer;

namespace WM.STORMS.BusinessLayer.Models
{
    public class Customer
    {
        public long WorkRequestId { get; set; }
        public Address Address { get; set; }
        public Phones Phones { get; set; }
        public string Name { get; set; }
        public string CustomerType { get; set; }
        public string SSN { get; set; }
        public string EffectiveMoveInDate { get; set; }
        public string DogCode { get; set; }
        public string CustomerID { get; set; }
        public string PremiseID { get; set; }
        public string PhoneNumber { get; set; }
    }
}
