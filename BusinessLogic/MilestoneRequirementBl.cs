using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WM.Common.Interfaces;
using WM.STORMS.BusinessLayer.Models;
using WM.STORMS.DataAccessLayer;
using WM.Common;

namespace WM.STORMS.BusinessLayer.BusinessLogic
{
    public class MilestoneRequirementBl : BaseBl
    {


        public MilestoneRequirement GetByEntity(TWMMILESTONERQMT entity)
        {
            return MapEntityToObject(entity);
        }

        public List<MilestoneRequirement> GetByEntities(IEnumerable<TWMMILESTONERQMT> entities)
        {
            if (entities != null && entities.Count() > 0)
            {
                return entities.Select(m => MapEntityToObject(m)).ToList();
            }

            return null;
        }

        public MilestoneRequirement GetMilestoneRequirement(long workRequestId, int requirementId)
        {
            MilestoneRequirement obj = GetByEntity(unitOfWork.MilestoneRqmtRepo.GetSingle(m => m.CD_WR == workRequestId
                                                                                    && m.CD_RQMT == requirementId));

            return obj;
        }

        public List<MilestoneRequirement> GetMilestoneRequirements(long workRequestId)
        {
            return GetByEntities(unitOfWork.MilestoneRqmtRepo.Get(m => m.CD_WR == workRequestId));
        }

        public void Insert(MilestoneRequirement obj)
        {
            var entity = new TWMIFREQUIREMENT
            {
                CD_ACTION = "A",
                CD_DIST = obj.District,
                CD_RQMT = Convert.ToInt32(obj.RequirementId),
                CD_SEQ = 1,
                CD_SEQ_ERROR_RUN = null,
                CD_STATUS = obj.Status,
                CD_WR = Convert.ToInt64(obj.WorkRequestId),
                DS_MILESTONE_RQMT = obj.Description,
                FG_ERROR = "N",
                TS_IFREQUIREMENT = DateTime.Now
            };

            unitOfWork.IfMilestoneRqmtRepo.Insert(entity);
            unitOfWork.Save();
        }

        //public void Update(MilestoneRequirement obj)
        //{
        //    long workRequestId = Convert.ToInt64(obj.WorkRequestId);
        //    int requirementId = Convert.ToInt32(obj.RequirementId);

        //    var entity = unitOfWork.MilestoneRqmtRepo.GetSingle(m => m.CD_WR == workRequestId && m.CD_DIST == obj.District && m.CD_RQMT == requirementId);
        //    entity = MapObjectToEntity(obj);

        //    //entity.CD_WR = (long)int.Parse(obj.WorkRequestId);
        //    //entity.CD_DIST = obj.District;
        //    //entity.CD_RQMT = int.Parse(obj.RequirementId);
        //    //entity.DS_MILESTONE_RQMT = obj.Description;
        //    entity.ST_RQMT = obj.Status;
        //    entity.ID_OPER_RESPONSIBLE = obj.ResponsibleId;
        //    entity.ID_OPER_LAST_UPDTD = obj.LastUpdatedId;
        //    entity.TS_OPER_LAST_UPDTD = obj.LastUpdatedDate;
        //    entity.DT_ASSIGNED = obj.AssignedDate;
        //    entity.FG_LEAD_TIME = obj.FG_LEAD_TIME;
        //    entity.FG_WORK_QUEUE = obj.FG_WORK_QUEUE;
        //    entity.FG_SERVICE_STD = obj.FG_SERVICE_STD;

        //    unitOfWork.MilestoneRqmtRepo.Update(entity);
        //    unitOfWork.Save();
        //}

        public void Update(MilestoneRequirement obj)
        {
            long workRequestId = Convert.ToInt64(obj.WorkRequestId);
            int requirementId = Convert.ToInt32(obj.RequirementId);
            var entity = unitOfWork.MilestoneRqmtRepo.GetSingle(m => m.CD_WR == workRequestId && m.CD_DIST == obj.District && m.CD_RQMT == requirementId);

            if (entity != null)
            {
              //  entity = MapObjectToEntity(obj);
                entity.ST_RQMT = obj.Status;
                entity.DS_MILESTONE_RQMT = obj.Description;
                entity.ID_OPER_LAST_UPDTD = obj.LastUpdatedId;

                if (entity != null)
                {
                    unitOfWork.MilestoneRqmtRepo.Update(entity);
                    unitOfWork.Save();
                }
            }
        }
        public void Update(List<MilestoneRequirement> objs)
        {
            foreach (var item in objs)
            {
                Update(item);
            }
        }

        public void UpdateMilestoneRequirementStatus(long workRequestId, string district, int requirementId, string status)
        {
            var entity = unitOfWork.MilestoneRqmtRepo.GetSingle(m => m.CD_WR == workRequestId && m.CD_DIST == district && m.CD_RQMT == requirementId);

            if (entity != null)
            {
                entity.ST_RQMT = status;

                unitOfWork.MilestoneRqmtRepo.Update(entity);
                unitOfWork.Save();
            }
        }

        public void UpdateMilestoneRequirementOperator(long workRequestId, string district, int requirementId, string userId, string operatorId)
        {
            var entity = unitOfWork.MilestoneRqmtRepo.GetSingle(m => m.CD_WR == workRequestId && m.CD_DIST == district && m.CD_RQMT == requirementId);

            //entity.ID_OPER_RESPONSIBLE = operatorId;

            unitOfWork.MilestoneRqmtRepo.Update(entity);
            unitOfWork.Save();
        }

        public MilestoneRequirement MapEntityToObject(TWMMILESTONERQMT entity)
        {
            if (entity != null)
            {
                MilestoneRequirement obj = new MilestoneRequirement();

                obj.WorkRequestId = entity.CD_WR.ToString();
                obj.District = entity.CD_DIST;

                obj.RequirementId = entity.CD_RQMT.ToString();
                obj.Description = entity.DS_MILESTONE_RQMT;
                obj.Status = entity.ST_RQMT;
                obj.ResponsibleId = entity.ID_OPER_RESPONSIBLE;
                obj.LastUpdatedId = entity.ID_OPER_LAST_UPDTD;
                obj.LastUpdatedDate = entity.TS_OPER_LAST_UPDTD;
                obj.AssignedDate = entity.DT_ASSIGNED;

                obj.FG_LEAD_TIME = entity.FG_LEAD_TIME;
                obj.FG_WORK_QUEUE = entity.FG_WORK_QUEUE;
                obj.FG_SERVICE_STD = entity.FG_LEAD_TIME;

                return obj;
            }

            return null;
        }

        public TWMMILESTONERQMT MapObjectToEntity(MilestoneRequirement obj)
        {
            if (obj != null)
            {
                TWMMILESTONERQMT entity = new TWMMILESTONERQMT();
                entity.CD_WR = (long)int.Parse(obj.WorkRequestId);
                entity.CD_DIST = obj.District;

                entity.CD_RQMT = int.Parse(obj.RequirementId);
                entity.DS_MILESTONE_RQMT = obj.Description;
                entity.ST_RQMT = obj.Status;
                entity.ID_OPER_RESPONSIBLE = obj.ResponsibleId;
                entity.ID_OPER_LAST_UPDTD = obj.LastUpdatedId;
                entity.TS_OPER_LAST_UPDTD = obj.LastUpdatedDate;
                entity.DT_ASSIGNED = obj.AssignedDate;

                entity.FG_LEAD_TIME = obj.FG_LEAD_TIME;
                entity.FG_WORK_QUEUE = obj.FG_WORK_QUEUE;
                entity.FG_SERVICE_STD = obj.FG_SERVICE_STD;

                return entity;
            }

            return null;
        }
    }
}