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
    public class WorkPacketBl : BaseBl, IBusinessLogic<WorkPacket, TWMWORKPACKET>
    {


        //public WorkPacket __GetByEntity(TWMWORKPACKET entity)
        //{
        //    if (entity != null)
        //    {
        //        WorkPacket obj = MapEntityToObject(entity);

        //        return obj;
        //    }

        //    return null;
        //}
        //public List<WorkPacket> __GetByEntities(IEnumerable<TWMWORKPACKET> entities)
        //{
        //    if (entities != null && entities.Count() > 0)
        //    {
        //        return entities.Select(m => MapEntityToObject(m)).ToList();
        //    }

        //    return null;
        //}

        public List<WorkPacket> GetByWorkRequestId(long workRequestId)
        {
            return Get(unitOfWork.WorkpacketRepo.Get(m => m.CD_WR == workRequestId));
        }

        public WorkPacket GetByWorkRequestIdWorkPacketId(long workRequestId, long id)
        {
            return Get(unitOfWork.WorkpacketRepo.GetSingle(m => m.CD_WR == workRequestId && m.CD_WORKPACKET == id));
        }

        public WorkPacket Get(TWMWORKPACKET entity)
        {
            if (entity != null)
            {
                WorkPacket obj = MapEntityToObject(entity);

                return obj;
            }

            return null;
        }

        public List<WorkPacket> Get(IEnumerable<TWMWORKPACKET> entities)
        {
            if (entities != null && entities.Count() > 0)
            {
                return entities.Select(m => MapEntityToObject(m)).ToList();
            }

            return null;
        }

        public List<WorkPacket> Get()
        {
            throw new NotImplementedException();
        }

        public void Create(WorkPacket obj)
        {
            throw new NotImplementedException();
        }

        public void Create(List<WorkPacket> objs)
        {
            throw new NotImplementedException();
        }

  

        public void Update(WorkPacket obj)
        {
            long workRequestId = Convert.ToInt64(obj.WorkRequestId);
            long workPacketId = Convert.ToInt64(obj.WorkPacketId);

            //have to get the entity before updating it
            var entity = unitOfWork.WorkpacketRepo.GetSingle(m => m.CD_WR == workRequestId && m.CD_WORKPACKET == workPacketId);

            //map the enitity with out instantiating a new one
            entity = MapRootObjectToEntity(obj, entity);


            unitOfWork.WorkpacketRepo.Update(entity);
            unitOfWork.Save();
        }

  

        public void Update(List<WorkPacket> objs)
        {
            throw new NotImplementedException();
        }

        public void Delete(params object[] keys)
        {
            throw new NotImplementedException();
        }

        public void Delete(WorkPacket obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(List<WorkPacket> objs)
        {
            throw new NotImplementedException();
        }

        public void Insert(WorkPacket obj)
        {
            throw new NotImplementedException();
        }

        public void Insert(List<WorkPacket> objs)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<WorkPacketHistory> MapHistEntitiesToObjects(IEnumerable<TWMWORKPACKETHIST> entities)
        {
            List<WorkPacketHistory> objs = new List<WorkPacketHistory>();

            foreach (var item in entities)
            {
                objs.Add(MapHistEntityToObject(item));
            }

            return objs;
        }
        public WorkPacketHistory MapHistEntityToObject(TWMWORKPACKETHIST entity)
        {
            if (entity != null)
            {
                WorkPacketHistory obj = new WorkPacketHistory();

                obj.WorkPacketId = entity.CD_WORKPACKET;
                obj.Sequence  = entity.CD_WPKHIST_SEQ;
                obj.Status  = entity.CD_WPKTSTATUS;
                obj.Reason  = entity.CD_REASON;
                obj.RecordedOperator  = entity.ID_OPER_RECORDED;
                obj.DateTimeRecorded  = entity.TS_RECORDED;
                obj.DateTimeStatusReached  = entity.DT_STATUS_REACHED;
                obj.DateTimeStatusEndEstimate = entity.DT_STATUS_END_EST;

                return obj;
            }

            return null;
        }
        public TWMWORKPACKETHIST MapHistRootObjectToEntity(WorkPacketHistory obj, TWMWORKPACKETHIST entity)
        {
            if (obj != null)
            {
                entity.CD_WORKPACKET = obj.WorkPacketId;
                entity.CD_WPKHIST_SEQ = obj.Sequence;
                entity.CD_WPKTSTATUS = obj.Status;
                entity.CD_REASON = obj.Reason;
                entity.ID_OPER_RECORDED = obj.RecordedOperator;
                entity.TS_RECORDED = obj.DateTimeRecorded ;
                entity.DT_STATUS_REACHED = obj.DateTimeStatusReached;
                entity.DT_STATUS_END_EST = obj.DateTimeStatusEndEstimate;

                return entity;
            }

            return null;
        }

              
           
        public WorkPacket MapEntityToObject(TWMWORKPACKET entity)
        {
            if (entity != null)
            {
                WorkPacket obj = new WorkPacket();

                obj.WorkRequestId = entity.CD_WR.ToString();
                obj.WorkPacketId = entity.CD_WORKPACKET.ToString();
                obj.District = entity.CD_DIST;
                obj.Name = entity.NM_WORKPACKET;
                obj.CrewClass = entity.CD_CREW_CLASS;
                obj.Crew = entity.CD_CREW;
                obj.CrewCompleted = entity.CD_CREW_COMPLETED;
                obj.TotalLaborHours = entity.HR_LBR;
                obj.TotalLaborHoursConstruction = (decimal)entity.HR_LBR_CONSTRUCT;
             	obj.ScheduledDate = entity.DT_SCHED;
                obj.ResolutionCode = entity.CD_RESOLUTION;
                obj.CompletionDate = entity.DT_COMPLETE;
            
                if (entity.TWMCUPOINT_ESTs != null && entity.TWMCUPOINT_ESTs.Count > 0)
                {
                    obj.CUs = new CUBl().Get(entity.TWMCUPOINT_ESTs);
                }
                if (entity.TWMWORKPACKETHISTs != null && entity.TWMWORKPACKETHISTs.Count > 0)
                {
                    obj.WorkPacketHistories = MapHistEntitiesToObjects(entity.TWMWORKPACKETHISTs).ToList();
                }

                if (entity.TWMCUPOINT_ASBs != null && entity.TWMCUPOINT_ASBs.Count > 0)
                {
                    obj.CUAsbs = new CUAsbBl().GetByWorkRequestId(entity.CD_WR);
                }               

                return obj;
            }

            return null;
        }
        public IEnumerable<WorkPacket> MapEntitiesToObjects(IEnumerable<TWMWORKPACKET> entities)
        {
            throw new NotImplementedException();
        }
        public TWMWORKPACKET MapObjectToEntity(WorkPacket obj)
        {
            if (obj != null)
            {
                TWMWORKPACKET entity = new TWMWORKPACKET();

                return MapRootObjectToEntity(obj, entity);
            }

            return null;
        }
        public IEnumerable<TWMWORKPACKET> MapObjectsToEntities(IEnumerable<WorkPacket> objs)
        {
            throw new NotImplementedException();
        }

        public TWMWORKPACKET MapRootObjectToEntity(WorkPacket obj, TWMWORKPACKET entity)
        {
            //workpackets can only be modified...scheduled, descheduled, completed
            
            if (obj != null)
            {
                entity.CD_WR = Convert.ToInt64(obj.WorkRequestId);
                entity.CD_WORKPACKET = Convert.ToInt64(obj.WorkPacketId);
                entity.CD_DIST = obj.District;
                entity.NM_WORKPACKET = obj.Name;
                entity.CD_CREW_CLASS = obj.CrewClass;
                // Do not update the crew via the workpacket update.  It should only be updated using the twmifschedule table
          //      entity.CD_CREW = obj.Crew;
          //      entity.CD_CREW_COMPLETED = obj.CrewCompleted;
                entity.HR_LBR = obj.TotalLaborHours;
                entity.HR_LBR_CONSTRUCT = obj.TotalLaborHoursConstruction;
          //      entity.CD_RESOLUTION = obj.ResolutionCode;

                // Do not update the schedule date via the workpacket update.  It should only be updated using the twmifschedule table
            //    entity.DT_SCHED = obj.ScheduledDate;
          //      entity.DT_COMPLETE = obj.CompletionDate;
           
                //if (obj.CUs != null && obj.CUs.Count > 0)
                //{
                //    int i = 0;
                //    foreach (var item in obj.CUs)
                //    {
                //        entity.TWMCUPOINT_ESTs.Add(new CUBl().MapRootObjectToEntity(item, entity.TWMCUPOINT_ESTs.ToList()[i]));
                //        i++;
                //    }
                //}

                //if (obj.CUAsbs != null && obj.CUAsbs.Count > 0)
                //{
                //    int i = 0;
                //    foreach (var item in obj.CUAsbs)
                //    {
                //        entity.TWMCUPOINT_ASBs.Add(new CUAsbBl().MapRootObjectToEntity(item, entity.TWMCUPOINT_ASBs.ToList()[i]));
                //        i++;
                //    }
                //}

                //if (obj.WorkPacketHistories != null && obj.WorkPacketHistories.Count > 0)
                //{
                //    int i = 0;
                //    foreach (var item in obj.WorkPacketHistories)
                //    {
                //        entity.TWMWORKPACKETHISTs.Add(MapHistRootObjectToEntity(item, entity.TWMWORKPACKETHISTs.ToList()[i]));
                //        i++;
                //    }
                //}

                return entity;
            }

            return null;
        }
    }
}
