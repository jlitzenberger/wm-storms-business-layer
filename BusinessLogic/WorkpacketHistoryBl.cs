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
    public class WorkPacketHistoryBl : BaseBl
    {

        public WorkPacketHistory GetByEntity(TWMWORKPACKETHIST entity)
        {
            return MapEntityToObject(entity);
        }

        public List<WorkPacketHistory> GetByEntities(IEnumerable<TWMWORKPACKETHIST> entities)
        {
            if (entities != null && entities.Count() > 0)
            {
                return entities.Select(m => MapEntityToObject(m)).ToList();
            }

            return null;
        }
    
        public List<WorkPacketHistory> GetByWorkPacketId(long workPacketId)
        {
            return GetByEntities(unitOfWork.WorkpacketHistRepo.Get(m => m.CD_WORKPACKET == workPacketId));
        }

        public void AddHistory(WorkPacketHistory workPacketHistory)
        {
            AddHistory(MapObjectToEntity(workPacketHistory));
        }

        private void AddHistory(TWMWORKPACKETHIST entity)
        {
            unitOfWork.WorkpacketHistRepo.Insert(entity);
            unitOfWork.Save();
        }           
              
        public WorkPacketHistory MapEntityToObject(TWMWORKPACKETHIST entity)
        {
            if (entity != null)
            {
                WorkPacketHistory obj = new WorkPacketHistory();
                obj.DateTimeRecorded = entity.TS_RECORDED;
                obj.DateTimeStatusEndEstimate = entity.DT_STATUS_END_EST;
                obj.DateTimeStatusReached = entity.DT_STATUS_REACHED;
                obj.Reason = entity.CD_REASON;
                obj.RecordedOperator = entity.ID_OPER_RECORDED;
                obj.Sequence = entity.CD_WPKHIST_SEQ;                    
                obj.WorkPacketId = entity.CD_WORKPACKET;
                obj.DateTimeRecorded = entity.TS_RECORDED;

                return obj;
            }

            return null;
        }

        public TWMWORKPACKETHIST MapObjectToEntity(WorkPacketHistory obj)
        {
            if (obj != null)
            {
                TWMWORKPACKETHIST entity = new TWMWORKPACKETHIST();

                entity.CD_REASON = obj.Reason;
                entity.CD_WORKPACKET = obj.WorkPacketId;
                entity.CD_WPKHIST_SEQ = GetSequenceNo();
                entity.CD_WPKTSTATUS = obj.Status;
                entity.DT_STATUS_END_EST = obj.DateTimeStatusEndEstimate;
                entity.DT_STATUS_REACHED = obj.DateTimeStatusReached;
                entity.ID_OPER_RECORDED = obj.RecordedOperator;
                entity.TS_RECORDED = obj.DateTimeRecorded;

                return entity;
            }

            return null;
        }

        private long GetSequenceNo()
        {
            return Convert.ToInt64(unitOfWork.GenericSqlRepo.RunRawSql<Decimal>(" select we_s_twmworkpacket_hist_seq.nextval FROM dual").ToList()[0]);
        }
    }
}