using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WM.STORMS.BusinessLayer.Models;
using WM.STORMS.DataAccessLayer;

namespace WM.STORMS.BusinessLayer {
    public interface IBusinessLayer {
        
        WorkRequest GetWorkRequest(long WorkRequestNumber);

    }
}
