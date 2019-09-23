using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Common;
using WM.STORMS.BusinessLayer.Models;
using WM.STORMS.DataAccessLayer;

namespace WM.STORMS.BusinessLayer.BusinessLogic
{

    public class JobTypeBl : BaseBl
    {


        public List<JobType> GetAll()
        {
            return unitOfWork.JobTypeRepo.Get(m => m.FG_ACTIVE == "Y").Select(m => MapEntityToObject(m)).ToList();
        }

        public JobType GetById(string id)
        {
            return MapEntityToObject(unitOfWork.JobTypeRepo.GetSingle(m => m.TP_JOB == id && m.FG_ACTIVE == "Y"));
        }

        public List<JobType> GetByCrewHq(string crewHq)
        {
            IEnumerable<TWMCREWHQJOBTYPE> crewHqJobTypes = unitOfWork.CrewHqJobTypeRepo.Get(m => m.CD_CREWHQ == crewHq && m.FG_ACTIVE == "Y" && m.TWMCREWHQ.FG_ACTIVE == "Y");
            if (crewHqJobTypes != null && crewHqJobTypes.Count() > 0)
            {
                return crewHqJobTypes.Select(m => MapEntityToObject(m.TWMJOBTYPE)).ToList();
            }

            return null;
        }

        private JobType MapEntityToObject(TWMJOBTYPE obj)
        {
            if (obj != null)
            {
                return new JobType
                {
                    Name = obj.TP_JOB,
                    Description = obj.DS_JOB_TYPE
                };
            }

            return null;
        }
    }
}
