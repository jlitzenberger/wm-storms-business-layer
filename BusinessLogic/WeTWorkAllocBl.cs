using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WM.STORMS.BusinessLayer.Models;
using WM.STORMS.DataAccessLayer;
using WM.Common;
using WM.Common.Interfaces;

namespace WM.STORMS.BusinessLayer.BusinessLogic
{
    public class WeTWorkAllocBl : BaseBl
    {        
        public WeTWorkAlloc GetByEntity(WE_T_SO_R_WORKALLOC entity)
        {
            return MapEntityToObject(entity);
        }

        public List<WeTWorkAlloc> GetByEntities(IEnumerable<WE_T_SO_R_WORKALLOC> entities)
        {
            if (entities != null && entities.Count() > 0)
            {
                return entities.Select(m => MapEntityToObject(m)).ToList();
            }

            return null;
        }

        public List<WeTWorkAlloc> GetByWorkpacketId(long workpacketId)
        {
            return GetByEntities(unitOfWork.WeTWorkAllocRepo.Get(m => m.CD_WORKPACKET == workpacketId));
        }

        public void Create(WeTWorkAlloc weTWorkAlloc)
        {
            CreateWorkAlloc(MapObjectToEntity(weTWorkAlloc));
        }

        private void CreateWorkAlloc(WE_T_SO_R_WORKALLOC entity)
        {
            unitOfWork.WeTWorkAllocRepo.Insert(entity);
            unitOfWork.Save();
        }

        public void Update(WeTWorkAlloc obj)
        {
            WE_T_SO_R_WORKALLOC entity = MapObjectToEntity(obj);

            if (entity != null)
            {
                unitOfWork.WeTWorkAllocRepo.Update(entity);
                unitOfWork.Save();
            }
        }

    
        public WeTWorkAlloc MapEntityToObject(WE_T_SO_R_WORKALLOC entity)
        {
            if (entity != null)
            {
                WeTWorkAlloc obj = new WeTWorkAlloc();

                obj.CD_WORKPACKET = entity.CD_WORKPACKET;
                obj.CD_WR = entity.CD_WR;
                obj.DT_SCHED = entity.DT_SCHED;
                obj.DT_WORK = entity.DT_WORK;
                obj.FG_DESCHEDULE = entity.FG_DESCHEDULE;
                obj.FG_SENTSTORMS = entity.FG_SENTSTORMS;
                obj.HR_ALLOC = entity.HR_ALLOC;
                obj.HR_LBL_REM = entity.HR_LBL_REM;
                obj.HR_LBR = entity.HR_LBR;
                obj.ID_CREW = entity.ID_CREW;
                obj.ID_CREWRESCHED = entity.ID_CREWRESCHED;

                return obj;
            }

            return null;
        }

        public WE_T_SO_R_WORKALLOC MapObjectToEntity(WeTWorkAlloc obj)
        {
            if (obj != null)
            {
                WE_T_SO_R_WORKALLOC entity = new WE_T_SO_R_WORKALLOC();

                entity.CD_WORKPACKET = obj.CD_WORKPACKET;
                entity.CD_WR = obj.CD_WR;
                entity.DT_SCHED = obj.DT_SCHED;
                entity.DT_WORK = obj.DT_WORK;
                entity.FG_DESCHEDULE = obj.FG_DESCHEDULE;
                entity.FG_SENTSTORMS = obj.FG_SENTSTORMS;
                entity.HR_ALLOC = obj.HR_ALLOC;
                entity.HR_LBL_REM = obj.HR_LBL_REM;
                entity.HR_LBR = obj.HR_LBR;
                entity.ID_CREW = obj.ID_CREW;
                entity.ID_CREWRESCHED = obj.ID_CREWRESCHED;

                return entity;
            }

            return null;
        }
    }
}
