using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WM.STORMS.DataAccessLayer;
using System.Data;
using System.Data.Objects;
using WM.STORMS.BusinessLayer.Models;

namespace WM.STORMS.BusinessLayer {
    public class BusinessLayer : IBusinessLayer
    {
        //private readonly IWorkRequestRepository _wrRepository;

        //public BusinessLayer()
        //{
        //    _wrRepository = new WorkRequestRepository(OriginType.STORMS, EnvironmentType.DEV);
        //}

        //public WorkRequest GetWorkRequest(long WorkRequestNumber)
        //{
        //    WorkRequest myJob = new WorkRequest();

        //    TWMWR wr = _wrRepository.Get(m => m.CD_WR == WorkRequestNumber).FirstOrDefault();

        //    myJob.workrequest = (int)wr.CD_WR;

        //    return myJob;
        //}
        public WorkRequest GetWorkRequest(long WorkRequestNumber)
        {
            throw new NotImplementedException();
        }
    }
}
