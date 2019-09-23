using System;
using System.Collections.Generic;
//using System.Data.EntityClient;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WM.STORMS.BusinessLayer.BusinessLogic;
using WM.Common.Interfaces;
using WM.STORMS.BusinessLayer.Models;
using WM.STORMS.DataAccessLayer;

namespace WM.STORMS.BusinessLayer.BusinessLogic
{
    public class PremiseBl : BaseBl
    {
        public Premise GetByEntity(TWMPREMISE entity)
        {
            return MapEntityToObject(entity);
        }

        public List<Premise> GetByEntities(IEnumerable<TWMPREMISE> entities)
        {
            if (entities != null && entities.Count() > 0)
            {
                return entities.Select(m => MapEntityToObject(m)).ToList();
            }

            return null;
        }

        public Premise GetByWorkRequestIdPremiseIdServiceId(long workRequestId, string premiseId, string serviceId)
        {
            return GetByEntity(unitOfWork.PremiseRepo.GetSingle(m => m.CD_WR == workRequestId && m.ID_PREMISE == premiseId && m.ID_SERVICE == serviceId));
        }

        public List<Premise> GetByWorkRequestId(long WorkRequestId)
        {
            return GetByEntities(unitOfWork.PremiseRepo.Get(m => m.CD_WR == WorkRequestId));
        }

        public void Create(Premise obj)
        {
            if (obj != null)
            {
                Delete(obj);
                Insert(obj);
            }
        }

        public void Create(List<Premise> objs)
        {
            foreach (var item in objs)
            {
                Create(item);
            }
        }

        public void Update(Premise obj)
        {
            UpdateRoot(obj);

            if (obj.PremiseMore != null)
            {
                //Use Create here because of "pivot" table
                new PremiseMoreBl().Create(obj.PremiseMore);
            }
        }

        public void Update(List<Premise> objs)
        {
            foreach (var item in objs)
            {
                Update(item);
            }
        }

        private void UpdateRoot(Premise obj)
        {
            var entity = unitOfWork.PremiseRepo.GetSingle(m => m.CD_WR == obj.CD_WR && m.CD_DIST == obj.CD_DIST && m.ID_PREMISE == obj.ID_PREMISE && m.ID_SERVICE == obj.ID_SERVICE);

            //entity.CD_DIST = obj.CD_DIST;
            //entity.CD_WR = obj.CD_WR;
            //entity.ID_PREMISE = obj.ID_PREMISE;
            entity.CD_SIC = obj.CD_SIC;
            entity.CD_RATE_CLASS = obj.CD_RATE_CLASS;
            entity.CD_REVENUE_CLASS = obj.CD_REVENUE_CLASS;
            entity.CD_ADDRESS = obj.CD_ADDRESS;
            entity.FG_HAZARD = obj.FG_HAZARD;
            entity.FG_KEY_AVAIL = obj.FG_KEY_AVAIL;
            entity.QT_BASELOAD = obj.QT_BASELOAD;
            entity.QT_HEATING_FACTOR = obj.QT_HEATING_FACTOR;
            //entity.ID_SERVICE = obj.ID_SERVICE;

            unitOfWork.PremiseRepo.Update(entity);
            unitOfWork.Save();
        }

        public void Delete(string district, long workRequestId, string premiseId, string serviceId)
        {
            unitOfWork.PremiseRepo.Delete(district, workRequestId, premiseId, serviceId);
            unitOfWork.Save();
        }

        public void Delete(Premise obj)
        {
            Delete(obj.CD_DIST, obj.CD_WR, obj.ID_PREMISE, obj.ID_SERVICE);

            if (obj.PremiseMore != null)
            {
                new PremiseMoreBl().Delete(obj.PremiseMore);
            }
        }

        public void Delete(List<Premise> objs)
        {
            foreach (var item in objs)
            {
                Delete(item);
            }
        }

        public void Insert(Premise obj)
        {
            unitOfWork.PremiseRepo.Insert(MapObjectToEntity(obj));
            unitOfWork.Save();
        }

        public void Insert(List<Premise> objs)
        {
            foreach (var item in objs)
            {
                Insert(item);
            }
        }

        public Premise MapEntityToObject(TWMPREMISE entity)
        {
            if (entity != null)
            {
                Premise obj = new Premise();

                obj.CD_DIST = entity.CD_DIST;
                obj.CD_WR = entity.CD_WR;
                obj.ID_PREMISE = entity.ID_PREMISE;
                obj.CD_SIC = entity.CD_SIC;
                obj.CD_RATE_CLASS = entity.CD_RATE_CLASS;
                obj.CD_REVENUE_CLASS = entity.CD_REVENUE_CLASS;
                obj.CD_ADDRESS = entity.CD_ADDRESS;
                obj.FG_HAZARD = entity.FG_HAZARD;
                obj.FG_KEY_AVAIL = entity.FG_KEY_AVAIL;
                obj.QT_BASELOAD = entity.QT_BASELOAD;
                obj.QT_HEATING_FACTOR = entity.QT_HEATING_FACTOR;
                obj.ID_SERVICE = entity.ID_SERVICE;

                if (entity.TWMPREMISEMOREs != null && entity.TWMPREMISEMOREs.Count > 0)
                {
                    obj.PremiseMore = new PremiseMoreBl().Get(entity.TWMPREMISEMOREs);
                }

                return obj;
            }

            return null;
        }

        public TWMPREMISE MapObjectToEntity(Premise obj)
        {
            if (obj != null)
            {
                TWMPREMISE entity = new TWMPREMISE();
                entity.CD_DIST = obj.CD_DIST;
                entity.CD_WR = obj.CD_WR;
                entity.ID_PREMISE = obj.ID_PREMISE;
                entity.CD_SIC = obj.CD_SIC;
                entity.CD_RATE_CLASS = obj.CD_RATE_CLASS;
                entity.CD_REVENUE_CLASS = obj.CD_REVENUE_CLASS;
                entity.CD_ADDRESS = obj.CD_ADDRESS;
                entity.FG_HAZARD = obj.FG_HAZARD;
                entity.FG_KEY_AVAIL = obj.FG_KEY_AVAIL;
                entity.QT_BASELOAD = obj.QT_BASELOAD;
                entity.QT_HEATING_FACTOR = obj.QT_HEATING_FACTOR;
                entity.ID_SERVICE = obj.ID_SERVICE;

                return entity;
            }

            return null;
        }
    }
}
