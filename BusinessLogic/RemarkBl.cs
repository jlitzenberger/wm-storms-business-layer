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
    public class RemarkBl : BaseBl
    {


        public Remark GetByEntity(TWMREMARK entity)
        {
            return MapEntityToObject(entity);
        }

        public List<Remark> GetByEntities(IEnumerable<TWMREMARK> entities)
        {
            if (entities != null && entities.Count() > 0)
            {
                return entities.Select(m => MapEntityToObject(m)).ToList();
            }

            return null;
        }

        public List<Remark> GetByWorkRequestId(long workRequestId)
        {
            return GetByEntities(unitOfWork.RemarkRepo.Get(m => m.CD_WR == workRequestId));
        }

        public Remark GetByWorkRequestIdCreatedDateSeq(long workRequestId, DateTime createdDate, long seq)
        {
            return GetByEntity(unitOfWork.RemarkRepo.GetSingle(m => m.CD_WR == workRequestId && m.TS_REMARK == createdDate && m.CD_SEQ == seq));
        }

        public void AddRemark(Remark remark)
        {
            AddRemark(MapObjectToIfEntity(remark));
        }

        private void AddRemark(TWMIFMOBREMARK entity)
        {
            unitOfWork.IfMobRemarkRepo.Insert(entity);
            unitOfWork.Save();
        }

        public Remark MapEntityToObject(TWMREMARK entity)
        {
            if (entity != null)
            {
                Remark obj = new Remark();

                obj.CreationDate = entity.TS_REMARK;
                obj.CreatorID = entity.ID_OPER;
                obj.District = entity.CD_DIST;
                obj.RemarkText = entity.TXT_REMARK;
                obj.Seq = (int)entity.CD_SEQ;
                obj.Type = entity.TP_REMARK;
                obj.WorkRequest = (int)entity.CD_WR;

                return obj;
            }

            return null;
        }

        public TWMIFMOBREMARK MapObjectToIfEntity(Remark obj)
        {
            if (obj != null)
            {
                TWMIFMOBREMARK entity = new TWMIFMOBREMARK();

                entity.TS_REMARK = obj.CreationDate;
                entity.ID_OPER = obj.CreatorID;
                entity.CD_DIST = obj.District;
                entity.FG_ERROR = "N";
                entity.TXT_REMARK = obj.RemarkText;
                entity.CD_SEQ = obj.Seq;
                entity.TP_REMARK = obj.Type;
                entity.CD_WR = obj.WorkRequest;

                return entity;
            }

            return null;
        }
    }
}
