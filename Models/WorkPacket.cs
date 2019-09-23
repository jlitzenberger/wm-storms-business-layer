using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WM.STORMS.DataAccessLayer;

namespace WM.STORMS.BusinessLayer.Models
{
    public class WorkPacket
    {
        public string WorkRequestId { get; set; }
        public string WorkPacketId { get; set; }
        public string District { get; set; }
        public string Name { get; set; }
        public string CrewClass { get; set; }
        public string Crew { get; set; }
        public string CrewCompleted { get; set; }
        public decimal TotalLaborHours { get; set; }
        public decimal TotalLaborHoursConstruction { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public string ResolutionCode { get; set; }
        public DateTime? CompletionDate { get; set; }
       
        public List<WorkPacketHistory> WorkPacketHistories { get; set; }
        public List<CU> CUs { get; set; }
        public List<CUAsb> CUAsbs { get; set; }
      
        public WorkPacket()
        {
            //CUs = new List<CU>();
        }
    }
}
