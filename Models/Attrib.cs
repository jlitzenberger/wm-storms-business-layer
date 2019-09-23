using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WM.STORMS.BusinessLayer.Models
{
    public class Attrib
    {
        public string AttributeCode { get; set; }
        public string DataTypeIndicator { get; set; }
        public Nullable<int> DatafieldLength { get; set; }
        public string FieldNameText { get; set; }
        public string AttributeIndicator { get; set; }
        public string CollectionIndicator { get; set; }
        public string ValidationIndicator { get; set; }
        public string RequiredFlag { get; set; }
        public string TargetTableText { get; set; }
        public string ActiveFlag { get; set; }
        public string TargetColumnText { get; set; }
        public System.DateTime LastChangedTimeStamp { get; set; }

        public Attrib()
        {
        }
    }
}
