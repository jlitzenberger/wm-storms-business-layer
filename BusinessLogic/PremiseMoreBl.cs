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

namespace WM.STORMS.BusinessLayer.BusinessLogic 
{
    public class PremiseMoreBl : BaseBl
    {


        public PremiseMore Get(long WorkRequestNumber, string district, string premiseId) 
        {
            return MapEntitiesToObject(unitOfWork.PremiseMoreRepo.Get(m => m.CD_WR == WorkRequestNumber));
        }
        public PremiseMore Get(IEnumerable<TWMPREMISEMORE> entities)
        {
            PremiseMore obj = MapEntitiesToObject(entities);

            if (obj != null)
            {
                return obj;
            }

            return null;
        }
        
        public void Create(PremiseMore obj)
        {
            IEnumerable<TWMPREMISEMORE> entities = MapObjectToEntitites(obj);

            foreach (var item in entities)
            {
                unitOfWork.PremiseMoreRepo.Delete(item.CD_DIST, item.CD_WR, item.ID_PREMISE, item.ID_SERVICE, item.NM_FIELD);
                unitOfWork.PremiseMoreRepo.Insert(item);
            }

            unitOfWork.Save();
        }
        public void Update(PremiseMore obj)
        {
            IEnumerable<TWMPREMISEMORE> entities = MapObjectToEntitites(obj);

            foreach (var item in entities)
            {
                if (item.TXT_VALUE != null) 
                {
                    unitOfWork.PremiseMoreRepo.Update(item);
                }
            }

            unitOfWork.Save();
        }
        public void Delete(PremiseMore obj)
        {
            IEnumerable<TWMPREMISEMORE> entities = MapObjectToEntitites(obj);

            foreach (var item in entities)
            {
                unitOfWork.PremiseMoreRepo.Delete(item.CD_DIST, item.CD_WR, item.ID_PREMISE, item.ID_SERVICE, item.NM_FIELD);
            }

            unitOfWork.Save();
        }

        public PremiseMore MapEntitiesToObject(IEnumerable<TWMPREMISEMORE> entities)
        {
            PremiseMore obj = null;

            if (entities != null || entities.Count() > 0)
            {
                obj = new PremiseMore();

                foreach (var item in entities)
                {
                    try
                    {
                        obj.WorkRequestId = item.CD_WR;
                        obj.District = item.CD_DIST;
                        obj.PremiseId = item.ID_PREMISE;
                        obj.ServiceId = item.ID_SERVICE;

                        obj.GetType().GetProperty(item.NM_FIELD.ToString()).SetValue(obj, item.TXT_VALUE, null);
                    }
                    catch (NullReferenceException)
                    {
                        //Property Does Not Exist Under ExtraDetails
                    }
                }
            }

            return obj;
        }
        public List<TWMPREMISEMORE> MapObjectToEntitites(PremiseMore obj)
        {
            List<TWMPREMISEMORE> entity = new List<TWMPREMISEMORE>();

            foreach (System.Reflection.PropertyInfo item in obj.GetType().GetProperties())
            {
                var ent = new TWMPREMISEMORE();

                ent.CD_WR = obj.WorkRequestId;
                ent.CD_DIST = obj.District;
                ent.ID_PREMISE = obj.PremiseId;
                ent.ID_SERVICE = obj.ServiceId;

                if (item.Name != "WorkRequestId" && item.Name != "District" && item.Name != "PremiseId" && item.Name != "ServiceId") 
                {
                    ent.TXT_VALUE = (string)item.GetValue(obj, null);
                    ent.NM_FIELD = item.Name;

                    entity.Add(ent);
                }
            }

            return entity;
        }
    }
}
