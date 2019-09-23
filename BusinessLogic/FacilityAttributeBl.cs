using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Common;
using WM.Common.Interfaces;
using WM.STORMS.BusinessLayer.Models;
using WM.STORMS.DataAccessLayer;

namespace WM.STORMS.BusinessLayer.BusinessLogic
{
    public class FacilityAttributeBl : BaseBl
    {


        public FacilityAttribute Get(TWMFACATTRIB entity)
        {
            if (entity != null)
            {
                FacilityAttribute obj = MapEntityToObject(entity);

                return obj;
            }

            return null;
        }

        public List<FacilityAttribute> Get(IEnumerable<TWMFACATTRIB> entities)
        {
            IEnumerable<FacilityAttribute> objs = MapEntitiesToObjects(entities);

            if (objs != null)
            {
                return objs.ToList();
            }

            return null;
        }

        
       
        public List<FacilityAttribute> GetByEntities(IEnumerable<TWMFACATTRIB> entities)
        {
            if (entities != null && entities.Count() > 0)
            {
                return entities.Select(m => MapEntityToObject(m)).ToList();
            }

            return null;
        }

        public List<FacilityAttribute> GetAllByDate(DateTime changeDate)
        {
            return GetByEntities(unitOfWork.FacilityAttributeRepo.Get(m => m.TS_LAST_CHANGED >= changeDate).ToList()); 
        }    

     
        //public FacilityAttribute GetByName(string attributeName)
        //{
        //    return MapEntityToObject(unitOfWork.FacilityAttributeRepo.GetSingle(m => m.CD_ATTRIBUTE == attributeName));
        //}


        //public FacilityAttribute GetByEntity(TWMFACATTRIB entity)
        //{
        //    return MapEntityToObject(entity);
        //}

        //public List<FacilityAttribute> GetByEntities(IEnumerable<TWMFACATTRIB> entities)
        //{
        //    if (entities != null && entities.Count() > 0)
        //    {
        //        return entities.Select(m => MapEntityToObject(m)).ToList();
        //    }

        //    return null;
        //}
      
        //public FacilityAttribute GetFacilityAttribute(string FacilityName, string AttributeName, string action)
        //{
        //    return Get(unitOfWork.FacilityAttributeRepo.GetSingle(m => m.CD_FACILITY == FacilityName && m.CD_ATTRIBUTE == FacilityName && m.IND_ACTION == action));
        //}

        public FacilityAttribute MapEntityToObject(TWMFACATTRIB entity)
        {
            FacilityAttribute obj = new FacilityAttribute();

            obj.AttributeName = entity.CD_ATTRIBUTE;
            obj.FacilityName = entity.CD_FACILITY;
            obj.Action = entity.IND_ACTION;
            obj.Validation = entity.CD_VALIDATION;
            obj.FlagActive = entity.FG_ACTIVE;
            obj.IDSeq = entity.CD_SEQ.ToString();
            obj.LastChanged = entity.TS_LAST_CHANGED;

            return obj;
        }

        public IEnumerable<FacilityAttribute> MapEntitiesToObjects(IEnumerable<TWMFACATTRIB> entities)
        {
            List<FacilityAttribute> objs = new List<FacilityAttribute>();

            foreach (var item in entities)
            {
                objs.Add(MapEntityToObject(item));
            }

            return objs;
        }
    }
}
