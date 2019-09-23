using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WM.STORMS.BusinessLayer.Models;

namespace WM.STORMS.BusinessLayer
{
    public interface ICUBl
    {
        List<CU> GetPointCUs(Point point);
    }
}
