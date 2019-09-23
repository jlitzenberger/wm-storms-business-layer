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
    public class IFWorkpacketScheduleBl : BaseBl
    {
     
        //
        public List<IFWorkpacketSchedule> Get(List<TWMIFSCHEDULE> ifSchedules)
        {
            if (ifSchedules != null && ifSchedules.Count > 0)
            {
                ifSchedules = ifSchedules.Where(m => m.FG_ERROR == "N").ToList();
                return ifSchedules.Select(m => MapEntityToObject(m)).ToList();
            }

            return null;
        }

       
        //public List<Area> GetAll()
        //{
        //    return unitOfWork.AreaRepo.Get(m => m.FG_ACTIVE == "Y").Select(m => MapEntityToObject(m)).ToList();
        //}

        public List<IFWorkpacketSchedule> GetAll()
        {
            return unitOfWork.IfWorkpacketScheduleRepo.Get(m => m.FG_ERROR == "Y").Select(m => MapEntityToObject(m)).ToList();
        }

        public IFWorkpacketSchedule GetById(long ifworkpacketId)
        {
            return MapEntityToObject(unitOfWork.IfWorkpacketScheduleRepo.GetSingle(m => m.CD_WORKPACKET == ifworkpacketId));
        }     

        //

        public void Create(IFWorkpacketSchedule obj)
        {
            unitOfWork.IfWorkpacketScheduleRepo.Insert(MapObjectToEntity(obj));
            unitOfWork.Save();
        }
     
        public IFWorkpacketSchedule GetByEntity(TWMIFSCHEDULE entity)
        {
            return MapEntityToObject(entity);
        }

        public List<IFWorkpacketSchedule> GetByEntities(IEnumerable<TWMIFSCHEDULE> entities)
        {
            if (entities != null && entities.Count() > 0)
            {
                return entities.Select(m => MapEntityToObject(m)).ToList();
            }

            return null;
        }
             
        public void UpdateIfScheduleErrorStatus(long workPacketId)
        {
            var entity = unitOfWork.IfWorkpacketScheduleRepo.GetSingle(m => m.CD_WORKPACKET == workPacketId);

            if (entity != null)
            {
                entity.FG_ERROR = "N";

                unitOfWork.IfWorkpacketScheduleRepo.Update(entity);
                unitOfWork.Save();
            }
        }

        public TWMIFSCHEDULE MapObjectToEntity(IFWorkpacketSchedule obj)
        {
            if (obj != null)
            {
                return new TWMIFSCHEDULE
                {
                    CD_CREW = obj.CrewId,
                    CD_CREW_CLASS = obj.CrewClassCode,
                    CD_SEQ_ERROR = GetIfSequenceNo(),
                    DT_SCHED = obj.ScheduleDate,
                    DT_WORK = obj.WorkDate,
                    FG_CREW_MUSTDO = obj.CrewMustDoFlag,
                    FG_HOLD_RESCHEDULE = obj.RescheduleHoldFlag,
                    HR_REMAINING = obj.RemainingHours,
                    TS_SCHEDULE = obj.TimeStamp,
                    CD_DIST = obj.District,
                    CD_SEQ_ERROR_RUN = obj.ErrorRunSequenceCode,
                    CD_WORKPACKET = obj.WorkPacket,
                    CD_WR = obj.WorkRequest,
                    FG_ERROR = obj.ErrorFlag
                };
            }
            return null;
        }

        public IFWorkpacketSchedule MapEntityToObject(TWMIFSCHEDULE obj)
        {
            if (obj != null)
            {
                return new IFWorkpacketSchedule
                {
                    CrewClassCode = obj.CD_CREW_CLASS,
                    CrewId = obj.CD_CREW,
                    CrewMustDoFlag = obj.FG_CREW_MUSTDO,
                    ErrorSequenceCode = obj.CD_SEQ_ERROR,
                    RemainingHours = obj.HR_REMAINING,
                    RescheduleHoldFlag = obj.FG_HOLD_RESCHEDULE,
                    ScheduleDate = obj.DT_SCHED,
                    TimeStamp = obj.TS_SCHEDULE,
                    WorkDate = obj.DT_WORK,
                    District = obj.CD_DIST,
                    ErrorFlag = obj.FG_ERROR,
                    ErrorRunSequenceCode = obj.CD_SEQ_ERROR_RUN,
                    WorkPacket = obj.CD_WORKPACKET,
                    WorkRequest = obj.CD_WR
                };
            }
            return null;
        }
                
        private long GetIfSequenceNo()
        {
            return Convert.ToInt64(unitOfWork.GenericSqlRepo.RunRawSql<Decimal>(" select we_s_so_scheduleseq.nextval FROM dual").ToList()[0]);          
        }       
    }
}
