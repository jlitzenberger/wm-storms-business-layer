using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.STORMS.BusinessLayer.Models;
using WM.STORMS.DataAccessLayer;
using WM.Common;
using WM.Common.Interfaces;

namespace WM.STORMS.BusinessLayer.BusinessLogic
{
    public class FieldOrderAttachmentIdBl : BaseBl
    {
        public FieldOrderAttachmentId GetByEntity(WE_T_FIELDORDER_ATTACHMENTID entity)
        {
            return MapEntityToObject(entity);
        }

        public List<FieldOrderAttachmentId> GetByEntities(IEnumerable<WE_T_FIELDORDER_ATTACHMENTID> entities)
        {
            if (entities != null && entities.Count() > 0)
            {
                return entities.Select(m => MapEntityToObject(m)).ToList();
            }

            return null;
        }

        public List<FieldOrderAttachmentId> GetByWorkRequestId(long workRequestId)
        {
            return GetByEntities(unitOfWork.FieldOrderAttachmentIdRepo.Get(m => m.CD_WR == workRequestId));
        }    

        public FieldOrderAttachmentId GetBySequenceNumber(long workRequestId, long sequenceNumber, long interfaceSeq)
        {
            return GetByEntity(unitOfWork.FieldOrderAttachmentIdRepo.GetSingle(m => m.CD_WR == workRequestId && m.CD_SEQ == sequenceNumber && m.INTERFACE_SEQ == interfaceSeq));
        }

        public FieldOrderAttachmentId GetByAttachmentId(string fieldOrderNumber, string attachmentId)
        {
            return GetByEntity(unitOfWork.FieldOrderAttachmentIdRepo.GetSingle(m => m.FIELDORDER_NUMBER == fieldOrderNumber && m.ATTACHMENT_ID == attachmentId));
        }

        public void AddFieldOrderAttachmentId(FieldOrderAttachmentId fieldOrderAttachmentId)
        {
            AddFieldOrderAttachmentId(MapObjectToEntity(fieldOrderAttachmentId));
        }

        private void AddFieldOrderAttachmentId(WE_T_FIELDORDER_ATTACHMENTID entity)
        {
            unitOfWork.FieldOrderAttachmentIdRepo.Insert(entity);
            unitOfWork.Save();
        }

        //public void Update(FieldOrderAttachmentId obj)
        //{
        //    long workRequestId = Convert.ToInt64(obj.WorkRequest);
        //    long documentSequence = Convert.ToInt64(obj.SequenceCode);
        //    long interfaceSequence = Convert.ToInt64(obj.InterfaceSeq);


        //    var entity = unitOfWork.FieldOrderAttachmentIdRepo.GetSingle(m => m.CD_WR == workRequestId && m.CD_SEQ == documentSequence && m.INTERFACE_SEQ == interfaceSequence);
        //    entity = MapObjectToEntity(obj);

        //    //entity.CD_WR = obj.WorkRequest;
        //    entity.FG_DELETED = obj.DeletedFlag;
        //    entity.DS_DOCUMENT = obj.DocumentDescription;
        //    //entity.CD_SEQ = obj.SequenceCode;
        //    entity.ATTACHMENT_ID = obj.AttachmentId;
        //    entity.FIELDORDER_NUMBER = obj.FieldOrderNumber;
        //  //  entity.INTERFACE_SEQ = obj.InterfaceSeq;
        //    entity.NM_DOCUMENT = obj.DocumentName;
        //    entity.TS_UPDATE = obj.TimeStampUpdate;

        //    unitOfWork.FieldOrderAttachmentIdRepo.Update(entity);
        //    unitOfWork.Save();
        //}
        
        //public void Update(FieldOrderAttachmentId obj)
        //{
        //    long workRequestId = Convert.ToInt64(obj.WorkRequest);
        //    long documentSequence = Convert.ToInt64(obj.SequenceCode);

        //    //have to get the entity before updating it
        //    var entity = unitOfWork.FieldOrderAttachmentIdRepo.GetSingle(m => m.CD_WR == workRequestId && m.CD_SEQ == documentSequence);
        //    entity = MapRootObjectToEntity(obj, entity);

        //    unitOfWork.FieldOrderAttachmentIdRepo.Update(entity);
        //    unitOfWork.Save();
        //}

        //public void Update(List<FieldOrderAttachmentId> objs)
        //{
        //    foreach (var item in objs)
        //    {
        //        Update(item);
        //    }
        //}

        public void Update(FieldOrderAttachmentId obj)
        {
            var entity = unitOfWork.FieldOrderAttachmentIdRepo.GetSingle(m => m.CD_WR == obj.WorkRequest && m.CD_SEQ == obj.SequenceCode && m.INTERFACE_SEQ == obj.InterfaceSeq);

            if (entity != null)
            {
                entity.ATTACHMENT_ID = obj.AttachmentId;
                entity.FIELDORDER_NUMBER = obj.FieldOrderNumber;

                unitOfWork.FieldOrderAttachmentIdRepo.Update(entity);
                unitOfWork.Save();
            }
        }

        //public void UpdateFieldOrderAttachmentIdFieldOrderNumber(long workRequestId, long sequenceNumber, string fieldOrderId)
        //{
        //    var entity = unitOfWork.FieldOrderAttachmentIdRepo.GetSingle(m => m.CD_WR == workRequestId && m.CD_SEQ == sequenceNumber);

        //    entity.FIELDORDER_NUMBER = fieldOrderId;

        //    unitOfWork.FieldOrderAttachmentIdRepo.Update(entity);
        //    unitOfWork.Save();
        //}
        
        public void Delete(FieldOrderAttachmentId obj)
        { 
            WE_T_FIELDORDER_ATTACHMENTID entity = MapObjectToEntity(obj);
            if (entity != null)
            {
                unitOfWork.FieldOrderAttachmentIdRepo.Delete(entity.CD_WR, entity.CD_SEQ, entity.INTERFACE_SEQ);
                unitOfWork.Save();
            }                   
        }

        public WE_T_FIELDORDER_ATTACHMENTID MapRootObjectToEntity(FieldOrderAttachmentId obj, WE_T_FIELDORDER_ATTACHMENTID entity)
        {
            if (obj != null)
            {
                entity.CD_WR = obj.WorkRequest;
                entity.FG_DELETED = obj.DeletedFlag;
                entity.DS_DOCUMENT = obj.DocumentDescription;
                entity.CD_SEQ = obj.SequenceCode;
                entity.ATTACHMENT_ID = obj.AttachmentId;
                entity.FIELDORDER_NUMBER = obj.FieldOrderNumber;
                entity.INTERFACE_SEQ = obj.InterfaceSeq;
                entity.NM_DOCUMENT = obj.DocumentName;
                entity.TS_UPDATE = obj.TimeStampUpdate;              

                return entity;
            }
            return null;
        }

        public FieldOrderAttachmentId MapEntityToObject(WE_T_FIELDORDER_ATTACHMENTID entity)
        {
            if (entity != null)
            {
                FieldOrderAttachmentId obj = new FieldOrderAttachmentId();

                obj.AttachmentId = entity.ATTACHMENT_ID;
                obj.DeletedFlag = entity.FG_DELETED;
                obj.DocumentName = entity.NM_DOCUMENT;
                obj.DocumentDescription = entity.DS_DOCUMENT;
                obj.FieldOrderNumber = entity.FIELDORDER_NUMBER;
                obj.SequenceCode = entity.CD_SEQ;
                obj.TimeStampUpdate = entity.TS_UPDATE;
                obj.WorkRequest = entity.CD_WR;
                obj.InterfaceSeq = entity.INTERFACE_SEQ;

                return obj;
            }

            return null;
        }

        public WE_T_FIELDORDER_ATTACHMENTID MapObjectToEntity(FieldOrderAttachmentId obj)
        {
            if (obj != null)
            {
                WE_T_FIELDORDER_ATTACHMENTID entity = new WE_T_FIELDORDER_ATTACHMENTID();

                entity.ATTACHMENT_ID = obj.AttachmentId;
                entity.CD_SEQ = obj.SequenceCode;
                entity.CD_WR = obj.WorkRequest;
                entity.DS_DOCUMENT = obj.DocumentDescription;
                entity.FG_DELETED = obj.DeletedFlag;
                entity.FIELDORDER_NUMBER = obj.FieldOrderNumber;
                entity.NM_DOCUMENT = obj.DocumentName;
                entity.TS_UPDATE = obj.TimeStampUpdate;
                entity.INTERFACE_SEQ = obj.InterfaceSeq;

                return entity;
            }

            return null;
        }
    }
}
