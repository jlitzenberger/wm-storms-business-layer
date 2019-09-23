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
    public class FacilityBl : BaseBl
    {


        public Facility Get(TWMFACILITY entity)
        {
            if (entity != null)
            {
                Facility obj = MapEntityToObject(entity);

                return obj;
            }

            return null;
        }

        //public List<Facility> Get(IEnumerable<TWMFACILITY> entities)
        //{
        //    IEnumerable<Facility> objs = MapEntitiesToObjects(entities);

        //    if (objs != null)
        //    {
        //        return objs.ToList();
        //    }

        //    return null;
        //}

        public Facility GetFacility(string FacilityId)
        {
            return Get(unitOfWork.FacilityRepo.Get(m => m.CD_FACILITY == FacilityId).FirstOrDefault());
        }

        public List<Facility> GetAllByDate(DateTime changeDate)
        {
            return GetByEntities(unitOfWork.FacilityRepo.Get(m => m.TS_LAST_CHANGED >= changeDate).ToList());
        }

        public List<Facility> GetByEntities(IEnumerable<TWMFACILITY> entities)
        {
            if (entities != null && entities.Count() > 0)
            {
                return entities.Select(m => MapEntityToObject(m)).ToList();
            }

            return null;
        }

        public IEnumerable<Facility> MapEntitiesToObjects(IEnumerable<TWMFACILITY> entities)
        {
            List<Facility> objs = new List<Facility>();

            foreach (var item in entities)
            {
                objs.Add(MapEntityToObject(item));
            }
            return objs;
        }


        public Facility MapEntityToObject(TWMFACILITY entity)
        {
            Facility obj = new Facility();

            obj.FacilityName = entity.CD_FACILITY;
            obj.FacilityDescription = entity.DS_FACILITY;
            obj.FlagActive = entity.FG_ACTIVE;
            obj.FlagCollectedByQty = entity.FG_COLLECT_BY_QTY;
            obj.LastChanged = entity.TS_LAST_CHANGED;
            obj.TPFacility = entity.TP_FACILITY;

        //    obj.FacilityAttributes = entity.TWMFACATTRIBs != null && entity.TWMFACATTRIBs.Count > 0 ? new FacilityAttributeBl().Get(entity.TWMFACATTRIBs).ToList() : null;

            return obj;
        }  
    }
}