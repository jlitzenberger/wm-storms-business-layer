using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WM.STORMS.BusinessLayer.Models
{
    public class MilestoneRequirement
    {
        public string District { get; set; }
        public string WorkRequestId { get; set; }
        public string RequirementId { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string ResponsibleId { get; set; }
        public string LastUpdatedId { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public DateTime? AssignedDate { get; set; }

        public string FG_LEAD_TIME { get; set; }
        public string FG_WORK_QUEUE { get; set; }
        public string FG_SERVICE_STD { get; set; }
    }
}
