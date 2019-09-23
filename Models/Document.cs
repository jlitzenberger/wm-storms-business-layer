using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WM.STORMS.DataAccessLayer;

namespace WM.STORMS.BusinessLayer.Models
{
    public class Document
    {
        public long WorkRequestId { get; set; }
        public long SeqId { get; set; }
        public string DocumentDescription { get; set; }       
        public string DocumentName { get; set; }
        public long? WorkPacketId { get; set; }
        public string MobileFlag { get; set; }
        public string BlobId { get; set; }
        public DateTime UpdateDate { get; set; }
        public string TpDocument { get; set; }
        public string Path { get; set; }

        public DocumentBlob DocumentBlob { get; set; }

        public Document() { }

        public Document MapToEntity(TWMWRDOCUMENT entity)
        {
            Document obj = new Document();

            obj.WorkRequestId = entity.CD_WR;
            obj.SeqId = entity.CD_SEQ;
            obj.DocumentDescription = entity.DS_DOCUMENT;        
            obj.DocumentName = entity.NM_DOCUMENT;
            obj.WorkPacketId = entity.CD_WORKPACKET;
         //   obj.MobileFlag = entity.FG_MOBILE.ToString() == "Y" ? true : false; ;
            obj.MobileFlag = entity.FG_MOBILE;            
            obj.Path = entity.ID_PATH;
            obj.BlobId = entity.ID_BLOB;
            obj.UpdateDate = Convert.ToDateTime(entity.TS_UPDATE.ToString());
            obj.TpDocument = entity.TP_DOCUMENT;

            return obj;
        }

        public List<Document> MapToEntities(IEnumerable<TWMWRDOCUMENT> entities)
        {
            List<Document> objs = new List<Document>();

            foreach (var item in entities)
            {
                objs.Add(MapToEntity(item));
            }

            return objs;
        }
    }
}
