using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WM.STORMS.BusinessLayer.Models;

namespace WM.STORMS.BusinessLayer.Interfaces
{
    public interface IAttachmentBl
    {
        List<Attachment> GetAttachmentsByWorkRequestId(int workRequestId);
        List<Attachment> GetAttachmentsByWorkRequestIdType(int workRequestId, bool mobile);
    }
}
