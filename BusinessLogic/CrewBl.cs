using System.Collections.Generic;
using System.Linq;
using WM.Common;
using WM.STORMS.BusinessLayer.Models;
using WM.STORMS.DataAccessLayer;

namespace WM.STORMS.BusinessLayer.BusinessLogic
{
    public class CrewBl :BaseBl
    {
        public List<Crew> GetAll(string crewText = null)
        {
            return GetByEntities(unitOfWork.CrewRepo.Get(m => m.FG_ACTIVE == "Y" && (m.TXT_CREW == crewText || crewText == null)));
        }

        public Crew GetById(string crewId)
        {
            return MapEntityToObject(unitOfWork.CrewRepo.GetSingle(m => m.CD_CREW == crewId && m.FG_ACTIVE == "Y"));
        }
        public List<Crew> GetByEntities(IEnumerable<TWMCREW> entities)
        {
            if (entities != null && entities.Count() > 0)
            {
                return entities.Select(m => MapEntityToObject(m)).ToList();
            }

            return null;
        }

        private Crew MapEntityToObject(TWMCREW obj)
        {
            if (obj != null)
            {
                return new Crew
                {
                    ActiveFlag = obj.FG_ACTIVE,
                    CostPerHour = obj.HR_COST,
                    CrewClassCode = obj.CD_CREW_CLASS,
                    CrewDescription = obj.DS_CREW,
                    CrewHeadquarters = obj.CD_CREWHQ,
                    CrewId = obj.CD_CREW,
                    CrewText = obj.TXT_CREW,
                    EmployeeQuantity = obj.QT_EMPL,
                    EntityCode = obj.CD_ENTITY,
                    ResourceId = obj.ID_RESOURCE,
                    SectionCode = obj.CD_SECTION,
                    ShiftCode = obj.CD_SHIFT,
                    ShiftStartDate = obj.DT_SHIFT_START,
                    TimeStampUpdate = obj.TS_UPDATE,
                    VerifyRequiredFlag = obj.FG_VERIFY_RQD                    
                };
            }
            return null;
        }
    }
}
