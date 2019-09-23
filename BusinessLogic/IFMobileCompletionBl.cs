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
    public class IFMobileCompletionBl : BaseBl
    {
     

        public void Create(IFMobileCompletion obj)
        {
            unitOfWork.IfMobCompletionRepo.Insert(MapObjectToEntity(obj));
            unitOfWork.Save();
        }
     
        public IFMobileCompletion GetByEntity(TWMIFMOBCOMP_GP entity)
        {
            return MapEntityToObject(entity);
        }

        public List<IFMobileCompletion> GetByEntities(IEnumerable<TWMIFMOBCOMP_GP> entities)
        {
            if (entities != null && entities.Count() > 0)
            {
                return entities.Select(m => MapEntityToObject(m)).ToList();
            }

            return null;
        }

        public List<IFMobileCompletion> GetAll()
        {                      
            return GetByEntities(unitOfWork.IfMobCompletionRepo.GetAll());
        }

        public IFMobileCompletion GetMobileCompletionByWorkpacket(long workpacketId)
        {
            IFMobileCompletion obj = GetByEntity(unitOfWork.IfMobCompletionRepo.GetSingle(m => m.CD_WORKPACKET == workpacketId));

            return obj;
        }

        public void Update(IFMobileCompletion obj)
        {
            long workRequestId = Convert.ToInt64(obj.WorkRequest);
            long workPacketId = Convert.ToInt64(obj.WorkPacket);
            DateTime sequenceDate = obj.TimeStampMobileCompletion;

            //have to get the entity before updating it
            var entity = unitOfWork.IfMobCompletionRepo.GetSingle(m => m.CD_WORKPACKET == workPacketId && m.TS_IFMOBCOMP_GP == sequenceDate);

            if (entity != null)
            {
                //map the enitity with out instantiating a new one
                entity = MapRootObjectToEntity(obj, entity);

                unitOfWork.IfMobCompletionRepo.Update(entity);
                unitOfWork.Save();
            }
        }

        public TWMIFMOBCOMP_GP MapRootObjectToEntity(IFMobileCompletion obj, TWMIFMOBCOMP_GP entity)
        {
            if (obj != null)
            {
                entity.CD_WR = Convert.ToInt64(obj.WorkRequest);
                entity.CD_WORKPACKET = Convert.ToInt64(obj.WorkPacket);
                entity.CD_DIST = obj.District;
                entity.CD_RESOLUTION = obj.ResolutionCode;
                entity.DT_COMPLETE = obj.CompletionDate;
                entity.CD_CREW_COMPLETED = obj.CompletingCrewId;
                entity.CD_JOB = obj.JobCode;
                entity.CD_SEQ = obj.SequenceCode;
                entity.CD_SEQ_ERROR_RUN = obj.ErrorRunSequenceCode;
                entity.FG_ERROR = "N";
                entity.TP_JOB = obj.JobType;
                entity.TS_IFMOBCOMP_GP = obj.TimeStampMobileCompletion;

                return entity;
            }

            return null;
        }





        public TWMIFMOBCOMP_GP MapObjectToEntity(IFMobileCompletion obj)
        {
            if (obj != null)
            {
                return new TWMIFMOBCOMP_GP
                {
                    CD_DIST = obj.District,
                    CD_WR = obj.WorkRequest,
                    CD_CREW_COMPLETED = obj.CompletingCrewId,
                    CD_JOB = obj.JobCode,
                    CD_RESOLUTION = obj.ResolutionCode,
                    CD_SEQ_ERROR_RUN = obj.ErrorRunSequenceCode,
                    CD_WORKPACKET = obj.WorkPacket,
                    DT_COMPLETE = obj.CompletionDate,
                    TP_JOB = obj.JobType,
                    TS_IFMOBCOMP_GP = obj.TimeStampMobileCompletion ,
                    FG_ERROR = obj.ErrorFlag,
                    CD_SEQ = GetIfSequenceNo()
                };
            }
            return null;
        }

        public IFMobileCompletion MapEntityToObject(TWMIFMOBCOMP_GP obj)
        {
            if (obj != null)
            {
                return new IFMobileCompletion
                {
                     District = obj.CD_DIST,
                     WorkRequest = obj.CD_WR,
                     CompletingCrewId = obj.CD_CREW_COMPLETED,
                     JobCode = obj.CD_JOB,
                     ResolutionCode = obj.CD_RESOLUTION,
                     ErrorRunSequenceCode = obj.CD_SEQ_ERROR_RUN,
                     WorkPacket = obj.CD_WORKPACKET,
                     CompletionDate = obj.DT_COMPLETE,
                     JobType = obj.TP_JOB,
                     TimeStampMobileCompletion = obj.TS_IFMOBCOMP_GP,
                     ErrorFlag = obj.FG_ERROR,
                     SequenceCode = obj.CD_SEQ                      
                };
            }
            return null;
        }

        private long GetIfSequenceNo()
        {
            return Convert.ToInt64(unitOfWork.GenericSqlRepo.RunRawSql<Decimal>(" select WE_S_SO_EXTDSG.nextval FROM dual").ToList()[0]);          
        }
    }
}
