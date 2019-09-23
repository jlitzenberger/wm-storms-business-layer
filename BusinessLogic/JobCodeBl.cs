using System;
using System.Collections.Generic;
//using System.Data.EntityClient;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WM.STORMS.BusinessLayer.Models;
using WM.STORMS.DataAccessLayer;
using WM.Common;
using WM.Common.Interfaces;

namespace WM.STORMS.BusinessLayer.BusinessLogic
{
    public class JobCodeBl : BaseBl
    {


        public JobCode GetByEntity(TWMJOBCODE entity)
        {
            return MapEntityToObject(entity);
        }

        public List<JobCode> GetByEntities(IEnumerable<TWMJOBCODE> entities)
        {
            if (entities != null && entities.Count() > 0)
            {
                return entities.Select(m => MapEntityToObject(m)).ToList();
            }

            return null;
        }

        public List<JobCode> GetAll()
        {
            return GetByEntities(unitOfWork.JobCodeRepo.GetAll());
        }

        public JobCode GetById(string jobCode)
        {
            return GetByEntity(unitOfWork.JobCodeRepo.GetSingle(m => m.CD_JOB == jobCode));
        }

        public JobCode MapEntityToObject(TWMJOBCODE entity)
        {
            if (entity != null)
            {
                JobCode obj = new JobCode();

                obj.CD_JOB = entity.CD_JOB;
                obj.DS_JOB = entity.DS_JOB;
                obj.DT_DISCONTINUED = entity.DT_DISCONTINUED;
                obj.DT_EFFECTIVE = entity.DT_EFFECTIVE;
                obj.DY_PRIOR = entity.DY_PRIOR;
                obj.DY_TO_REINITIATE = entity.DY_TO_REINITIATE;
                obj.HR_EST_TIME = entity.HR_EST_TIME;
                obj.TP_JOB = entity.TP_JOB;
                obj.FG_FAC_DESIGN = entity.FG_FAC_DESIGN;
                obj.FG_FAC_ASBUILT = entity.FG_FAC_ASBUILT;
                obj.FG_CHANGE_EST_TIME = entity.FG_CHANGE_EST_TIME;
                obj.CD_EXC_MTHD = entity.CD_EXC_MTHD;
                obj.IND_INITIATION = entity.CD_EXC_MTHD;
                obj.FG_AUTO_JOB_COST = entity.FG_AUTO_JOB_COST;
                obj.TP_WORKS = entity.TP_WORKS;
                obj.DY_SERVICE_STD = entity.DY_SERVICE_STD;
                obj.FG_MOBILE_INIT = entity.FG_MOBILE_INIT;
                obj.QT_OFFSET = entity.QT_OFFSET;
                obj.CD_APPTPROFILE = entity.CD_APPTPROFILE;
                obj.ID_CAPABILITY = entity.ID_CAPABILITY;

                obj.CuJobCodes = entity.TWMCUJOBCODEs != null && entity.TWMCUJOBCODEs.Count > 0 ? new CuJobCodeBl().Get(entity.TWMCUJOBCODEs) : null;

                return obj;
            }

            return null;
        }
    }
}
