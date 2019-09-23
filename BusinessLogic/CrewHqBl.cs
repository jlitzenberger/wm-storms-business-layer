using System.Collections.Generic;
using System.Linq;
using WM.Common;
using WM.STORMS.BusinessLayer.Models;
using WM.STORMS.DataAccessLayer;

namespace WM.STORMS.BusinessLayer.BusinessLogic
{
    public class CrewHqBl : BaseBl
    {


        public List<CrewHq> GetAll()
        {
            return unitOfWork.CrewHqRepo.Get(m => m.FG_ACTIVE == "Y").Select(m => MapEntityToObject(m)).ToList();
        }

        public CrewHq GetById(string crewHqId)
        {
            return MapEntityToObject(unitOfWork.CrewHqRepo.GetSingle(m => m.CD_CREWHQ == crewHqId && m.FG_ACTIVE == "Y"));
        }

        public List<CrewHq> GetByJobType(string jobType)
        {
            IEnumerable<TWMCREWHQJOBTYPE> crewHqs = unitOfWork.CrewHqJobTypeRepo.Get(m => m.TP_JOB == jobType && m.FG_ACTIVE == "Y" && m.TWMCREWHQ.FG_ACTIVE == "Y");
            if (crewHqs.Count() > 0)
            {
                return crewHqs.Select(m => MapEntityToObject(m.TWMCREWHQ)).ToList();
            }

            return null;
        }

        private CrewHq MapEntityToObject(TWMCREWHQ obj)
        {
            if (obj != null)
            {
                return new CrewHq
                {
                    Description = obj.DS_CREWHQ,
                    Name = obj.CD_CREWHQ,
                };
            }
            return null;
        }
    }
}
