using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using WM.STORMS.BusinessLayer.Models;
using WM.STORMS.DataAccessLayer;
using WM.Common;

namespace WM.STORMS.BusinessLayer.BusinessLogic
{
    public class DocumentBl : BaseBl
    {

     
        public Document Get(TWMWRDOCUMENT entity)
        {
            if (entity != null)
            {
                Document obj = MapEntityToObject(entity);

                return obj;
            }

            return null;
        }
   
        public List<Document> Get(IEnumerable<TWMWRDOCUMENT> entities)
        {
            IEnumerable<Document> objs = MapEntitiesToObjects(entities);

            if (objs != null)
            {
                return objs.ToList();
            }

            return null;
        }

        public List<Document> GetDocuments(long workRequestId, string mobile)
        {
            List<Document> obj = Get(unitOfWork.DocumentRepo.Get(m => m.CD_WR == workRequestId && (m.FG_MOBILE == mobile || mobile == null)));

            return obj;
        }

        public Document GetDocumentBySeqId(long workRequestId, long id)
        {
            Document obj = Get(unitOfWork.DocumentRepo.GetSingle(m => m.CD_WR == workRequestId && m.CD_SEQ == id));

            return obj;
        }

        public List<Document> GetByEntities(IEnumerable<TWMWRDOCUMENT> entities)
        {
            if (entities != null && entities.Count() > 0)
            {
                return entities.Select(m => MapEntityToObject(m)).ToList();
            }

            return null;
        }

        //public List<GasLoadDetail> GetGasLoadDetails(long workRequestId, string tpCustomer = null, string proposed = null, string tpEquip = null)
        //{
        //    return GetByEntities(unitOfWork.GasLoadDetRepo.Get(m => m.CD_WR == workRequestId && (m.TP_CUSTOMER == tpCustomer || tpCustomer == null) && (m.CD_DEL_PRES_PROPOSED == proposed || proposed == null) && (m.TP_EQUIP == tpEquip || tpEquip == null)));
        //}

        
        public Document MapEntityToObject(TWMWRDOCUMENT entity)
        {
            Document obj = new Document();

            obj.WorkRequestId = entity.CD_WR;
            obj.BlobId = entity.ID_BLOB;        
            obj.DocumentDescription = entity.DS_DOCUMENT;       
            obj.DocumentName = entity.NM_DOCUMENT;
            obj.MobileFlag = entity.FG_MOBILE;
            obj.SeqId = entity.CD_SEQ;
            obj.TpDocument = entity.TP_DOCUMENT;
            obj.UpdateDate = Convert.ToDateTime(entity.TS_UPDATE);
            obj.WorkPacketId = entity.CD_WORKPACKET;
            obj.Path = entity.ID_PATH;
            
            obj.DocumentBlob = entity.TFCBLOB407 != null ? new  DocumentBlobBl().GetDocumentBlob(entity.ID_BLOB) : null; 
                       
            return obj;
        }

        public IEnumerable<Document> MapEntitiesToObjects(IEnumerable<TWMWRDOCUMENT> entities)
        {
            List<Document> objs = new List<Document>();

            foreach (var item in entities)
            {
                objs.Add(MapEntityToObject(item));
            }

            return objs;
        }

        public TWMWRDOCUMENT MapObjectToEntity(Document obj)
        {
            TWMWRDOCUMENT entity = new TWMWRDOCUMENT();          
              
            entity = MapRootObjectToEntity(obj, entity);

            return entity;
        }

        public IEnumerable<TWMWRDOCUMENT> MapObjectsToEntities(IEnumerable<Document> objs)
        {
            List<TWMWRDOCUMENT> entities = new List<TWMWRDOCUMENT>();

            foreach (var item in objs)
            {
                entities.Add(MapObjectToEntity(item));
            }

            return entities;
        }

        public TWMWRDOCUMENT MapRootObjectToEntity(Document obj, TWMWRDOCUMENT entity)
        {       
            entity.CD_WR = obj.WorkRequestId;
            entity.FG_MOBILE = obj.MobileFlag;
            entity.CD_SEQ = GetDocumentId();
            entity.CD_WORKPACKET = obj.WorkPacketId;         
            entity.DS_DOCUMENT = obj.DocumentDescription;
            entity.NM_DOCUMENT = obj.DocumentName;
            entity.TP_DOCUMENT = obj.TpDocument;
            entity.TS_UPDATE = obj.UpdateDate;
            entity.ID_BLOB = obj.BlobId;
            entity.ID_PATH = obj.Path;
     
            return entity;
        }
       
        public Document GetDocument(long workRequestId, int seqId)
        {
            return new Document().MapToEntity(unitOfWork.DocumentRepo.Get(m => m.CD_WR == workRequestId && m.CD_SEQ == seqId).FirstOrDefault());
        }

        public List<Document> GetDocumentsByWorkRequestId(long workRequestId)
        {
            List<Document> obj = new Document().MapToEntities(unitOfWork.DocumentRepo.Get(m => m.CD_WR == workRequestId));
            return obj;
        }
        public List<Document> GetDocumentsByWorkRequestIdType(long workRequestId, bool mobile)
        {
            List<Document> obj = new Document().MapToEntities(unitOfWork.DocumentRepo.Get(m => m.CD_WR == workRequestId && m.FG_MOBILE == (mobile == true ? "Y" : "N")));
            return obj;
        }

        public void Create(List<Document> objs)
        {
            foreach (var item in objs)
            {
                Create(item);
            }
        }     

        public Document Create(Document document)
        {
            return CreateDocument(MapObjectToEntity(document));
        }

        private Document CreateDocument(TWMWRDOCUMENT entity)
        {
            unitOfWork.DocumentRepo.Insert(entity);
            unitOfWork.Save();

            return MapEntityToObject(entity);
        }

        public void DeleteByWorkRequest(long workRequestId)
        {
            List<Document> objs = GetByEntities(unitOfWork.DocumentRepo.Get(m => m.CD_WR == workRequestId));

            foreach (var item in objs)
            {
                Delete(item);
            }        
        }

        public void Delete(List<Document> objs)
        {
            foreach (var item in objs)
            {
                Delete(item);
            }
        }
                
        public void Delete(Document obj)
        {
            Delete(obj);
        }
        
        public byte[] DeserializeDocumentToByteArray(Document document)
        {
            MemoryStream ms = GetDocumentByteArray(document);

            return ms.ToArray();
        }
        public byte[] DeserializeDocumentToByteArray(long workRequestId, int seqId)
        {
            MemoryStream ms = GetDocumentByteArray(GetDocument(workRequestId, seqId));

            return ms.ToArray();
        }
        public void DeserializeDocumentToNewFile(Document document, FileInfo newFileInfo)
        {
            MemoryStream ms = GetDocumentByteArray(document);

            using (FileStream fs = new FileStream(newFileInfo.FullName, FileMode.Create, FileAccess.Write))
            {
                using (ms)
                {
                    fs.Write((byte[])ms.ToArray(), 0, (int)ms.Length);
                }
            }
        }
        public void DeserializeDocumentToOriginalFile(long workRequestId, int seqId, DirectoryInfo fileLocaton)
        {
            Document document = GetDocument(workRequestId, seqId);

            MemoryStream ms = GetDocumentByteArray(document);

            using (FileStream fs = new FileStream(fileLocaton + document.DocumentName, FileMode.Create, FileAccess.Write))
            {
                using (ms)
                {
                    fs.Write((byte[])ms.ToArray(), 0, (int)ms.Length);
                }
            }
        }

        private MemoryStream GetDocumentByteArray(Document document)
        {
            MemoryStream ms = new MemoryStream();

            using (InflaterInputStream iis = new InflaterInputStream(new MemoryStream((Byte[])document.DocumentBlob.Blob)))
            {
                int size = 2048;
                byte[] byteArray = new byte[size - 1];

                do
                {
                    size = iis.Read(byteArray, 0, byteArray.Length);
                    ms.Write(byteArray, 0, size);
                }
                while (size > 0);
            }

            return ms;
        }

        private long GetDocumentId()
        {
            return Convert.ToInt64(unitOfWork.GenericSqlRepo.RunRawSql<Decimal>(" select QWM02WRDOC.nextval FROM dual").ToList()[0]);
        }     
    }
}
