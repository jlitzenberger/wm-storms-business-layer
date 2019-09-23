using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WM.STORMS.BusinessLayer.Models
{
    public class CuJobCodePointTransformer
    {
        public long WorkRequestId { get; set; }
        public List<CuJobCode> CuJobCodes { get; set; }
    }
}
