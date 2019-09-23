using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WM.STORMS.DataAccessLayer;

namespace WM.STORMS.BusinessLayer.Models
{
    public class DocumentBlob
    {
        public string BlobId { get; set; }
        public byte[] Blob { get; set; }
        public int UncompressedFileSize { get; set; }
        public string CompressionType { get; set; }
        public DateTime ReapDate { get; set; }

        public DocumentBlob() { }

        public virtual Document Document { get; set; }

        //public DocumentBlob MapToEntity(TFCBLOB407 entity)
        //{
        //    DocumentBlob obj = new DocumentBlob();

        //    obj.BlobId = entity.ID_BLOB;
        //    obj.Blob = (byte[])entity.TXT_BLOB;
        //    obj.UncompressedFileSize = (int)entity.LN_SIZE_UNCOMP;
        //    obj.CompressionType = entity.TP_COMPRESSION;
        //    obj.ReapDate = entity.DT_REAP_CHECK;

        //    return obj;
        //} 
    }
}
