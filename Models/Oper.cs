using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WM.STORMS.DataAccessLayer;

namespace WM.STORMS.BusinessLayer.Models
{
    public class Oper
    {
        public string OperatorId { get; set; }
        public string EmployeeId { get; set; }
        public string Company { get; set; }
        public string CrewHq { get; set; }
        public string District { get; set; }
        public string Division { get; set; }
        public string IsActive { get; set; }      
        public string OperatorName { get; set; }
        public string LoginId { get; set; }
        public string JobDescription { get; set; }
        public OperatorPassword OperatorPassword { get; set; }
        public string EmailId { get; set; } //Could be possibly Split into Phone Number and Fax because that is what it seems like it in there 

        public Oper() { }
    }
}
