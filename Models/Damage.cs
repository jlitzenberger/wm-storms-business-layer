using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WM.STORMS.BusinessLayer.Models
{
    public class Damage
    {
        public long WorkRequest { get; set; }
        public string EmployeeId { get; set; }
        public Nullable<System.DateTime> CurrentDate { get; set; }
        public string ArrivalTime { get; set; }
        public string DamagedAddress { get; set; }
        public string DamagedCity { get; set; }
        public string DamagedState { get; set; }
        public string DamagedZip { get; set; }
        public string DamagedPhone { get; set; }
        public string DamagedParty { get; set; }
        public string ResponsibleParty { get; set; }
        public string ResponsibleOperator { get; set; }
        public string ResponsibleAddress { get; set; }
        public string ResponsibleCity { get; set; }
        public string ResponsibleState { get; set; }
        public string ResponsibleZip { get; set; }
        public string ResponsiblePhone { get; set; }
        public string DamageLiabilityAdmit { get; set; }
        public string DamageLiability { get; set; }
        public string ChargedParty { get; set; }
        public string LocateRequest { get; set; }
        public string LocateTicket { get; set; }
        public string LocateCompany { get; set; }
        public string LocateArea { get; set; }
        public string LocateMarkCorrect { get; set; }
        public string DamageGasLeak { get; set; }
        public string DamageOrificeSize { get; set; }
        public string DamagePressure { get; set; }
        public string DamageDuration { get; set; }
        public string Nature { get; set; }
        public string Cause { get; set; }
        public string EvidenceAvailable { get; set; }
        public string PictureCollected { get; set; }
        public string EvidenceCollected { get; set; }
        public string DamageInformation { get; set; }
        public string DamageStored { get; set; }
        public string DamageLocation { get; set; }
        public string ResponsibleSupervisor { get; set; }
        public string ResponsiblePartyName { get; set; }
        public string DamageTime { get; set; }
        public string DamagedPartyName { get; set; }
        public Nullable<int> DispatchedWorkRequest { get; set; }
    }
}
