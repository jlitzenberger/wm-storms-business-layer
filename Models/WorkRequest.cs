using System;
using System.Collections.Generic;

namespace WM.STORMS.BusinessLayer.Models
{
    public class WorkRequest    
    {
        public string JobDescription { get; set; }
        public DateTime? RequiredDate { get; set; }
        public long WorkRequestId { get; set; }
        public string CrewHeadquarter { get; set; }
        public string AssignedTo { get; set; }
        public string WorkType { get; set; }
        public string JobCode { get; set; }
        public string JobType { get; set; }
        public string Latitude { get; set; }  //Not needed here..in the Geo model XCoordinate
        public string Longitute { get; set; } //Not needed here..in the Geo model YCoordinate
        public string ReviewDate { get; set; }
        public string SuprevisorID { get; set; }
        public string WorkCompletedDate { get; set; }
        public string WorkRequestClosedDate { get; set; }
        //public string WorkRequestStatus { get; set; }
        public string Project { get; set; }
        public string ProjectName { get; set; }
        public string IOInstallation { get; set; }
        public string IORemoval { get; set; }
        public string IOTransfer { get; set; }
        public string IORevenue { get; set; }
        public string Priority { get; set; }
        public string MeterNo { get; set; }
        public long WorkRequestRelatedTo { get; set; }

        public string ExternalJobNumber { get; set; }
        public string ExternalSystemId { get; set; }
        public decimal? HrEstTime { get; set; }

        public DateTime? DateCanceled { get; set; }
        public string Status { get; set; }
        public bool Canceled { get; set; }
        public string District { get; set; }
        public string Resolution { get; set; }

        public string ContactName { get; set; }  // not the same as customer  
        public string ContactPhone { get; set; }

        public string MobilizedWr { get; set; }

        public List<Design> Specifications { get; set; }  
        public ExtraDetails ExtraDetails { get; set; }
        public Address Address { get; set; }
        public Geo Geo { get; set; }
        public List<Customer> Customers { get; set; }        
        public List<AssociatedParty> AssociatedParties { get; set; }
        public FieldReports FieldReports { get; set; }
        public List<Remark> Remarks { get; set; }
        public List<WorkPacket> WorkPackets { get; set; }
        public List<Document> Documents { get; set; }
        public List<MilestoneRequirement> MilestoneRequirements { get; set; }
        public List<Premise> Premises { get; set; }
        public List<GasLoadDetail> GasLoadDetails { get; set; }
        public List<PointAsb> PointAsbs { get; set; }   
        public List<ExceptionCondition> ExceptionConditions { get; set; }

        public WorkRequest()
        {
            //Specifications = new List<Design>();
            //AsBuilt = new Design();
            //ExtraDetails = new ExtraDetails();
            //Address = new Address();
            //Geo = new Geo();
            //Customers = new List<Customer>();
            //Contact = new Customer();
            //AssociatedParties = new List<AssociatedParty>();
            //Remarks = new List<Remark>();
            //FieldReports = new FieldReports();
            //WorkPackets = new List<WorkPacket>();
            //Documents = new List<Document>();
            //MilestoneRequirements = new List<MilestoneRequirement>();
            //Premises = new List<Premise>();
        }

    }
}
