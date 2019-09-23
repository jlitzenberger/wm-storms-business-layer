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
    public class DamageReportBl : BaseBl
    {   
        public DamageReport GetByEntity (WE_T_SF_DAMAGE_RPT entity)
        {
            return MapEntityToObject(entity);
        }

        public DamageReport GetByWorkRequestId(long workRequestId)
        {
            return GetByEntity(unitOfWork.DamageReportRepo.GetSingle(m => m.CD_WR == workRequestId));
        }

        public void Create(DamageReport damageReport)
        {
            CreateDamageReport(MapObjectToEntity(damageReport));
        }

        private void CreateDamageReport(WE_T_SF_DAMAGE_RPT entity)
        {
            unitOfWork.DamageReportRepo.Insert(entity);
            unitOfWork.Save();
        }
        
        public void Update(DamageReport obj)
        {
            WE_T_SF_DAMAGE_RPT entity = MapObjectToEntity(obj);

            if (entity != null)
            {
                unitOfWork.DamageReportRepo.Update(entity);
            }            
        }

        public DamageReport MapEntityToObject(WE_T_SF_DAMAGE_RPT obj)
        {
            if (obj != null)
            {
                return new DamageReport
                {
                    WitnessAddress = obj.WITNESS_ADDR,
                    WitnessCity = obj.WITNESS_CITY,
                    WitnessName = obj.WITNESS_NAME,
                    WitnessPhone = obj.WITNESS_PHONE,
                    WitnessSequenceNumber = obj.WITNESS_SEQ,
                    WitnessState = obj.WITNESS_STATE,
                    WitnessZip = obj.WITNESS_ZIP,
                    WorkRequest = obj.CD_WR                      
                };
            }
            return null;
        }

        public WE_T_SF_DAMAGE_RPT MapObjectToEntity(DamageReport obj)
        {
            if (obj != null)
            {
                return new WE_T_SF_DAMAGE_RPT
                {
                    CD_WR = obj.WorkRequest,
                    WITNESS_ADDR = obj.WitnessAddress,
                    WITNESS_CITY = obj.WitnessCity,
                    WITNESS_NAME = obj.WitnessName,
                    WITNESS_PHONE = obj.WitnessPhone,
                    WITNESS_SEQ = obj.WitnessSequenceNumber,
                    WITNESS_STATE = obj.WitnessState,
                    WITNESS_ZIP = obj.WitnessZip
                };
            }
            return null;
        }
   
    }
}
