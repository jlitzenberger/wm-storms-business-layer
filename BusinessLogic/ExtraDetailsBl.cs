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
    public class ExtraDetailsBl : BaseBl
    {
        public ExtraDetails GetByEntities(IEnumerable<TWMBILLINGMORE> entities)
        {
            return MapEntitiesToObjects(entities);
        }

        public ExtraDetails GetByWorkRequestId(long workRequestId)
        {
            return GetByEntities(unitOfWork.BillingMoreRepo.Get(m => m.CD_WR == workRequestId));
        }

        public void Create(ExtraDetails obj)
        {
            if (obj != null)
            {
                IEnumerable<TWMBILLINGMORE> entities = MapObjectToEntity(obj);

                Delete(obj);

                foreach (var item in entities)
                {
                    unitOfWork.BillingMoreRepo.Insert(item);
                }

                unitOfWork.Save();
            }
        }

        public void Delete(ExtraDetails obj)
        {
            IEnumerable<TWMBILLINGMORE> entities = MapObjectToEntity(obj);

            if (entities.Count() > 0)
            {
                foreach (var item in entities)
                {
                    unitOfWork.BillingMoreRepo.Delete(item.CD_DIST, item.CD_WR, item.NM_FIELD);
                }

                unitOfWork.Save();
            }
        }

        public void Update(ExtraDetails obj)
        {
            List<TWMBILLINGMORE> entities = MapObjectToEntity(obj).ToList();

            if (entities != null && entities.Count > 0)
            {
                foreach (var item in entities)
                {
                    unitOfWork.BillingMoreRepo.Update(item);
                }
            }
        }

        private string GetTxtValue(TWMBILLINGMORE entity)
        {
            string actualValue = entity.TWMAMPACITY == null ? string.Empty : entity.TWMAMPACITY.CD_AMPACITY
                               + entity.TWMSERVICETYPE == null ? string.Empty : entity.TWMSERVICETYPE.CD_SERVICE_TYPE
                               + entity.TWMSERVICEVOLT == null ? string.Empty : entity.TWMSERVICEVOLT.CD_SERVICE_VOLT
                               + entity.TWMCUSTTYPE == null ? string.Empty : entity.TWMCUSTTYPE.TP_CUSTOMER;

            if (actualValue == string.Empty)
            {
                return entity.TXT_VALUE;
            }
            else
            {
                return actualValue;
            }
        }

        public ExtraDetails MapEntitiesToObjects(IEnumerable<TWMBILLINGMORE> entities)
        {
            ExtraDetails obj = null;

            if (entities != null && entities.Count() > 0)
            {
                obj = new ExtraDetails();

                foreach (var item in entities)
                {
                    try
                    {
                        obj.workRequestId = item.CD_WR.ToString();
                        obj.district = item.CD_DIST;
                        obj.GetType().GetProperty(item.NM_FIELD.ToString()).SetValue(obj, GetTxtValue(item), null);
                    }
                    catch (NullReferenceException)
                    {
                        //Property Does Not Exist Under ExtraDetails
                    }
                }
            }

            return obj;
        }

        public IEnumerable<TWMBILLINGMORE> MapObjectToEntity(ExtraDetails obj)
        {
            List<TWMBILLINGMORE> entity = new List<TWMBILLINGMORE>();

            foreach (System.Reflection.PropertyInfo item in new List<System.Reflection.PropertyInfo>(obj.GetType().GetProperties()))
            {
                var ent = new TWMBILLINGMORE();

                ent.CD_DIST = obj.district;
                ent.CD_WR = Convert.ToInt64(obj.workRequestId);
                ent.TXT_VALUE = (string)item.GetValue(obj, null);
                ent.NM_FIELD = item.Name;

                if (item.Name != "workRequestId" && item.Name != "district")
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
