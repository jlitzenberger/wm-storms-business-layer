using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WM.STORMS.DataAccessLayer;

namespace WM.STORMS.BusinessLayer.Models
{
    public class Remark
    {
        public DateTime CreationDate;
        public string CreatorID;
        public string District;
        public string RemarkText;
        public int Seq;
        public string Type;
        public long WorkRequest;

        public Remark()
        {
        }
    }
}
