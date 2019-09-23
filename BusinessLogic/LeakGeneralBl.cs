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
    public class LeakGeneralBl : BaseBl
    {


        public LeakGeneral GetByEntity(WE_T_SF_LEAK_GENERAL entity)
        {
            return MapEntityToObject(entity);
        }

        public LeakGeneral GetByWorkRequestId(long workRequestId)
        {
            return GetByEntity(unitOfWork.LeakGeneralRepo.GetSingle(m => m.CD_WR == workRequestId));
        }

        public void Create(LeakGeneral leakGeneral)
        {
            CreateLeakGeneral(MapObjectToEntity(leakGeneral));
        }

        private void CreateLeakGeneral(WE_T_SF_LEAK_GENERAL entity)
        {
            unitOfWork.LeakGeneralRepo.Insert(entity);
            unitOfWork.Save();
        }

        public void Update(LeakGeneral obj)
        {
            WE_T_SF_LEAK_GENERAL entity = MapObjectToEntity(obj);

            if (entity != null)
            {
                unitOfWork.LeakGeneralRepo.Update(entity);
            }
        }

        public LeakGeneral MapEntityToObject(WE_T_SF_LEAK_GENERAL obj)
        {
            if (obj != null)
            {
                return new LeakGeneral
                {
                    LeakFoundIndicator = obj.IND_LEAK_FOUND,
                    LeakGradeCode = obj.CD_LEAK_GRADE,
                    MainValveNumber = obj.NO_MAIN_VALVE,
                    SequenceCode = obj.CD_SEQ,                 
                    WorkRequest = obj.CD_WR
                };
            }
            return null;
        }

        public WE_T_SF_LEAK_GENERAL MapObjectToEntity(LeakGeneral obj)
        {
            if (obj != null)
            {
                return new WE_T_SF_LEAK_GENERAL
                {
                    CD_SEQ = obj.SequenceCode,
                    CD_LEAK_GRADE = obj.LeakGradeCode,
                    NO_MAIN_VALVE = obj.MainValveNumber,
                    IND_LEAK_FOUND = obj.LeakFoundIndicator,
                    CD_WR = obj.WorkRequest
                };
            }
            return null;
        }

    }
}
