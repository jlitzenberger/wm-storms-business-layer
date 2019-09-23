using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WM.STORMS.BusinessLayer.Models
{
    public class FieldOrderAttachmentId
    {
        public long WorkRequest { get; set; }
        public long SequenceCode { get; set; }
        public string DocumentDescription { get; set; }
        public string DocumentName { get; set; }
        public Nullable<System.DateTime> TimeStampUpdate { get; set; }
        public string AttachmentId { get; set; }
        public string DeletedFlag { get; set; }
        public string FieldOrderNumber { get; set; }
        public long InterfaceSeq { get; set; }

        public FieldOrderAttachmentId()
        {
        }
    }
}
