using System;
using System.Collections.Generic;
//using System.Data.EntityClient;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WM.STORMS.BusinessLayer.Models;
using WM.STORMS.DataAccessLayer;
using WM.Common;
using WM.Common.Interfaces;

namespace WM.STORMS.BusinessLayer.BusinessLogic
{
    public class DocumentBlobBl : BaseBl
    {


        public DocumentBlob GetByEntity(TFCBLOB407 entity)
        {
            return MapEntityToObject(entity);
        }   
        
        public DocumentBlob GetDocumentBlob(string BlobId)
        {
            return GetByEntity(unitOfWork.DocumentBlobRepo.GetSingle(m => m.ID_BLOB == BlobId));
        }

        public void Create(DocumentBlob blob)
        {
            CreateDocumentBlob(MapObjectToEntity(blob));
        }

        private void CreateDocumentBlob(TFCBLOB407 entity)
        {
            unitOfWork.DocumentBlobRepo.Insert(entity);
            unitOfWork.Save();
        }

        public DocumentBlob MapEntityToObject(TFCBLOB407 entity)
        {
            if (entity != null)
            {
                DocumentBlob obj = new DocumentBlob();

                obj.Blob = entity.TXT_BLOB;
                obj.BlobId = entity.ID_BLOB;
                obj.CompressionType = entity.TP_COMPRESSION;
                obj.ReapDate = entity.DT_REAP_CHECK;
                obj.UncompressedFileSize = (int)entity.LN_SIZE_UNCOMP;             

                return obj;
            }

            return null;
        }

        public TFCBLOB407 MapObjectToEntity(DocumentBlob obj)
        {
            TFCBLOB407 entity = new TFCBLOB407();        

            entity = MapRootObjectToEntity(obj, entity);
          
            return entity;
        }

        public TFCBLOB407 MapRootObjectToEntity(DocumentBlob obj, TFCBLOB407 entity)
        {
            entity.TXT_BLOB = obj.Blob;            
            entity.DT_REAP_CHECK = obj.ReapDate;
            entity.ID_BLOB = obj.BlobId;
            entity.LN_SIZE_UNCOMP = obj.UncompressedFileSize;
            entity.TP_COMPRESSION = obj.CompressionType;
            
            return entity;
        }

        // *** if this is added, it would break the CAD to Storms document post

        //private long GetBlobId()
        //{
        //    return Convert.ToInt64(unitOfWork.GenericSqlRepo.RunRawSql<Decimal>(" select QWM37BLOB.nextval FROM dual").ToList()[0]);
        //}       

    }
}