using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WM.STORMS.BusinessLayer.Models
{
    public class Crew
    {
        public string CrewId { get; set; }
        public string CrewClassCode { get; set; }
        public string CrewHeadquarters { get; set; }
        public string EntityCode { get; set; }
        public string CrewDescription { get; set; }
        public string ActiveFlag { get; set; }
        public Nullable<int> EmployeeQuantity { get; set; }
        public string SectionCode { get; set; }
        public decimal CostPerHour { get; set; }
        public System.DateTime TimeStampUpdate { get; set; }
        public string ShiftCode { get; set; }
        public Nullable<System.DateTime> ShiftStartDate { get; set; }
        public string CrewText { get; set; }
        public Nullable<int> ResourceId { get; set; }
        public string VerifyRequiredFlag { get; set; }
    }
}
