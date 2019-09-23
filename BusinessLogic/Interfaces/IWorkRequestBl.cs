using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WM.STORMS.BusinessLayer.Models;

namespace WM.STORMS.BusinessLayer
{
    public interface IWorkRequestBl
    {
        WorkRequest GetWorkRequest(long workRequestId);
        long? CreateWorkRequest(WorkRequest workRequest);
        List<WorkRequest> GetLeakWorkRequestsByRange(DateTime startDate, DateTime endDate);
        WorkRequest GetWorkRequestByWorkPacket(long workPacketId);
    }
}
