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
    public class WorkRequestNonDesignedBl : BaseBl
    {


        public WorkRequestNonDesigned Get(TWMWRNONDESIGNED entity)
        {
            if (entity != null)
            {
                WorkRequestNonDesigned obj = MapEntityToObject(entity);

                return obj;
            }

            return null;
        }
        public List<WorkRequestNonDesigned> Get(IEnumerable<TWMWRNONDESIGNED> entities)
        {
            IEnumerable<WorkRequestNonDesigned> objs = MapEntitiesToObjects(entities);

            if (objs != null)
            {
                return objs.ToList();
            }

            return null;
        }

        public WorkRequestNonDesigned GetNonDesigned(long WorkRequestNumber, string district) 
        {
            return Get(unitOfWork.WrNonDesignedRepo.GetSingle(m => m.CD_WR == WorkRequestNumber));
        }
        public List<WorkRequestNonDesigned> GetNonDesigneds(long WorkRequestNumber, string district)
        {
            return Get(unitOfWork.WrNonDesignedRepo.Get(m => m.CD_WR == WorkRequestNumber));
        }
        
        public void Create(WorkRequestNonDesigned obj) 
        {
            if (obj != null)
            {
                Delete(obj);
                Insert(obj);
            }
        }
        public void Create(List<WorkRequestNonDesigned> objs)
        {
            foreach (var item in objs)
            {
                Create(item);
            }
        }
        public void Update(WorkRequestNonDesigned obj)
        {
            unitOfWork.WrNonDesignedRepo.Update(MapObjectToEntity(obj));
            unitOfWork.Save();
        }
        public void Update(List<WorkRequestNonDesigned> objs)
        {
            foreach (var item in objs)
            {
                Update(item);
            }
        }
        public void Delete(string district, long workRequestId)
        {
            unitOfWork.WrNonDesignedRepo.Delete(district, workRequestId);
            unitOfWork.Save();
        }
        public void Delete(WorkRequestNonDesigned obj) 
        {
            Delete(obj.CD_DIST, obj.CD_WR);
        }
        public void Delete(List<WorkRequestNonDesigned> objs)
        {
            foreach (var item in objs)
            {
                Delete(item);
            }
        }
        public void Insert(WorkRequestNonDesigned obj)
        {
            unitOfWork.WrNonDesignedRepo.Insert(MapObjectToEntity(obj));
            unitOfWork.Save();
        }
        public void Insert(List<WorkRequestNonDesigned> objs)
        {
            foreach (var item in objs)
            {
                Insert(item);
            }
        }

        public TWMWRNONDESIGNED MapObjectToEntity(WorkRequestNonDesigned obj)
        {
            TWMWRNONDESIGNED entity = new TWMWRNONDESIGNED();

            entity = MapRootObjectToEntity(obj, entity);
            
            return entity;
        }
        public WorkRequestNonDesigned MapEntityToObject(TWMWRNONDESIGNED entity) 
        {
            WorkRequestNonDesigned obj = new WorkRequestNonDesigned();

            obj.CD_DIST = entity.CD_DIST;
            obj.CD_WR = entity.CD_WR;
            obj.CD_JOB = entity.CD_JOB;
            obj.DS_JOB = entity.DS_JOB;
            obj.FG_REINITIATED = entity.FG_REINITIATED;

            return obj;
        }
        public IEnumerable<WorkRequestNonDesigned> MapEntitiesToObjects(IEnumerable<TWMWRNONDESIGNED> entities)
        {
            List<WorkRequestNonDesigned> objs = new List<WorkRequestNonDesigned>();

            foreach (var item in entities) 
            {
                objs.Add(MapEntityToObject(item));
            }

            return objs;
        }
        public IEnumerable<TWMWRNONDESIGNED> MapObjectsToEntities(IEnumerable<WorkRequestNonDesigned> objs)
        {
            List<TWMWRNONDESIGNED> entities = new List<TWMWRNONDESIGNED>();

            foreach (var item in objs) 
            {
                entities.Add(MapObjectToEntity(item));
            }

            return entities;
        }
        public TWMWRNONDESIGNED MapRootObjectToEntity(WorkRequestNonDesigned obj, TWMWRNONDESIGNED entity)
        {
            entity.CD_DIST = obj.CD_DIST;
            entity.CD_WR = obj.CD_WR;
            entity.CD_JOB = obj.CD_JOB;
            entity.DS_JOB = obj.DS_JOB;
            entity.FG_REINITIATED = obj.FG_REINITIATED;

            return entity;
        }
    }
}
