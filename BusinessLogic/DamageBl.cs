using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WM.STORMS.BusinessLayer.Models;
using WM.STORMS.DataAccessLayer;
using WM.Common;
using WM.Common.Interfaces;

namespace WM.STORMS.BusinessLayer.BusinessLogic
{
    public class DamageBl :BaseBl
    { 
        public Damage GetByEntity (WE_T_SF_DAMAGE entity)
        {
            return MapEntityToObject(entity);
        }

        public Damage GetByWorkRequestId(long workRequestId)
        {
            return GetByEntity(unitOfWork.DamageRepo.GetSingle(m => m.CD_WR == workRequestId));
        }


        public void Create(Damage damage)
        {
            CreateDamage(MapObjectToEntity(damage));
        }

        private void CreateDamage(WE_T_SF_DAMAGE entity)
        {
            unitOfWork.DamageRepo.Insert(entity);
            unitOfWork.Save();
        }
                
        public void Update(Damage obj)
        {
            WE_T_SF_DAMAGE entity = MapObjectToEntity(obj);

            if (entity != null)
            {
                unitOfWork.DamageRepo.Update(entity);
            }            
        }

        public Damage MapEntityToObject(WE_T_SF_DAMAGE obj)
        {
            if (obj != null)
            {
                return new Damage
                {
                    ArrivalTime = obj.ARRIVALTIME,
                    Cause = obj.CAUSE,
                    ChargedParty = obj.PARTY_CHARGED,
                    CurrentDate = obj.CURRENTDATE,
                    DamagedAddress = obj.DAMAGED_ADDR,
                    DamagedCity = obj.DAMAGED_CITY,
                    DamagedParty = obj.DAMAGED_PARTY,
                    DamagedPartyName = obj.DAMAGED_PARTY_NAME,
                    DamagedPhone = obj.DAMAGED_PHONE,
                    DamagedState = obj.DAMAGED_STATE,
                    DamageDuration = obj.DAMAGE_DURATION,
                    DamagedZip = obj.DAMAGED_ZIP,
                    DamageGasLeak = obj.DAMAGE_GAS_LEAK,
                    DamageInformation = obj.DAMAGE_INFORMATION,
                    DamageLiability = obj.DAMAGE_LIABILITY,
                    DamageLiabilityAdmit = obj.DAMAGE_LIABILITY_ADMIT,
                    DamageLocation = obj.DAMAGE_LOCATION,
                    DamageOrificeSize = obj.DAMAGE_ORIFICE_SIZE,
                    DamagePressure = obj.DAMAGE_PRESSURE,
                    DamageStored = obj.DAMAGE_STORED,
                    DamageTime = obj.DAMAGE_TIME,
                    DispatchedWorkRequest = obj.DISPATCHED_WR,
                    EmployeeId = obj.EMPID,
                    EvidenceAvailable = obj.EVIDENCE_AVAILABLE,
                    EvidenceCollected = obj.EVIDENCE_COLLECT,
                    LocateArea = obj.LOCATE_AREA,
                    LocateCompany = obj.LOCATE_COMPANY,
                    LocateMarkCorrect = obj.LOCATE_MARK_CORRECT,
                    LocateRequest = obj.LOCATE_REQUEST,
                    LocateTicket = obj.LOCATE_TICKET,
                    Nature = obj.NATURE,
                    PictureCollected = obj.PICTURE_COLLECT,
                    ResponsibleAddress = obj.RESPONSIBLE_ADDR,
                    ResponsibleCity = obj.RESPONSIBLE_CITY,
                    ResponsibleOperator = obj.RESPONSIBLE_OPERATOR,
                    ResponsibleParty = obj.RESPONSIBLE_PARTY,
                    ResponsiblePartyName = obj.RESPONSIBLE_PARTY_NAME,
                    ResponsiblePhone = obj.RESPONSIBLE_PHONE,
                    ResponsibleState = obj.RESPONSIBLE_STATE,
                    ResponsibleSupervisor = obj.RESPONSIBLE_SUPERVISOR,
                    ResponsibleZip = obj.RESPONSIBLE_ZIP,
                    WorkRequest = obj.CD_WR
                };
            }
            return null;
        }

        public WE_T_SF_DAMAGE MapObjectToEntity(Damage obj)
        {
            if (obj != null)
            {
                return new WE_T_SF_DAMAGE
                {
                    ARRIVALTIME = obj.ArrivalTime,
                    CAUSE = obj.Cause,
                    CD_WR = obj.WorkRequest,
                    CURRENTDATE = obj.CurrentDate,
                    DAMAGE_DURATION = obj.DamageDuration,
                    DAMAGE_GAS_LEAK = obj.DamageGasLeak,
                    DAMAGE_INFORMATION = obj.DamageInformation,
                    DAMAGE_LIABILITY = obj.DamageLiability,
                    DAMAGE_LIABILITY_ADMIT = obj.DamageLiabilityAdmit,
                    DAMAGE_LOCATION = obj.DamageLocation,
                    DAMAGE_ORIFICE_SIZE = obj.DamageOrificeSize,
                    DAMAGE_PRESSURE = obj.DamagePressure,
                    DAMAGE_STORED = obj.DamageStored,
                    DAMAGE_TIME = obj.DamageTime,
                    DAMAGED_ADDR = obj.DamagedAddress,
                    DAMAGED_CITY = obj.DamagedCity,
                    DAMAGED_PARTY = obj.DamagedParty,
                    DAMAGED_PARTY_NAME = obj.DamagedPartyName,
                    DAMAGED_PHONE = obj.DamagedPhone,
                    DAMAGED_STATE = obj.DamagedState,
                    DAMAGED_ZIP = obj.DamagedZip,
                    DISPATCHED_WR = obj.DispatchedWorkRequest,
                    EMPID = obj.EmployeeId,
                    EVIDENCE_AVAILABLE = obj.EvidenceAvailable,
                    EVIDENCE_COLLECT = obj.EvidenceCollected,
                    LOCATE_AREA = obj.LocateArea,
                    LOCATE_COMPANY = obj.LocateCompany,
                    LOCATE_MARK_CORRECT = obj.LocateMarkCorrect,
                    LOCATE_REQUEST = obj.LocateRequest,
                    LOCATE_TICKET= obj.LocateTicket,
                    NATURE = obj.Nature,
                    PARTY_CHARGED = obj.ChargedParty,
                    PICTURE_COLLECT = obj.PictureCollected,
                    RESPONSIBLE_ADDR = obj.ResponsibleAddress,
                    RESPONSIBLE_CITY = obj.ResponsibleCity,
                    RESPONSIBLE_OPERATOR = obj.ResponsibleOperator,
                    RESPONSIBLE_PARTY = obj.ResponsibleParty,
                    RESPONSIBLE_PARTY_NAME = obj.ResponsiblePartyName,
                    RESPONSIBLE_PHONE = obj.ResponsiblePhone,
                    RESPONSIBLE_STATE = obj.ResponsibleState,
                    RESPONSIBLE_SUPERVISOR = obj.ResponsibleSupervisor,
                    RESPONSIBLE_ZIP = obj.ResponsibleZip
                };
            }
            return null;
        }
    }
}

