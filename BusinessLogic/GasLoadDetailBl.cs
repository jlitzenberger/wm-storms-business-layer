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
    public class GasLoadDetailBl : BaseBl
    {
        public GasLoadDetail GetByEntity(TWMGASLOADDET entity)
        {
            return MapEntityToObject(entity);
        }

        public List<GasLoadDetail> GetByEntities(IEnumerable<TWMGASLOADDET> entities)
        {
            if (entities != null && entities.Count() > 0)
            {
                return entities.Select(m => MapEntityToObject(m)).ToList();
            }

            return null;
        }

        public List<GasLoadDetail> GetGasLoadDetails(long workRequestId, string tpCustomer = null, string proposed = null, string tpEquip = null)
        {
            return GetByEntities(unitOfWork.GasLoadDetRepo.Get(m => m.CD_WR == workRequestId && (m.TP_CUSTOMER == tpCustomer || tpCustomer == null) && (m.CD_DEL_PRES_PROPOSED == proposed || proposed == null) && (m.TP_EQUIP == tpEquip || tpEquip == null)));
        }

        public void Create(GasLoadDetail obj)
        {
            if (obj != null)
            {
                Delete(obj);
                Insert(obj);
            }
        }

        public void Create(List<GasLoadDetail> objs)
        {
            foreach (var item in objs)
            {
                Create(item);
            }
        }

        public void Update(GasLoadDetail obj)
        {
            var entity = unitOfWork.GasLoadDetRepo.GetSingle(m => m.CD_WR == obj.CD_WR && m.CD_DIST == obj.CD_DIST && m.TP_CUSTOMER == obj.TP_CUSTOMER && m.CD_DEL_PRES_PROPOSED == obj.CD_DEL_PRES_PROPOSED && m.TP_EQUIP == obj.TP_EQUIP);

            //entity.CD_DIST = obj.CD_DIST;
            //entity.CD_WR = obj.CD_WR;
            entity.TP_CUSTOMER = obj.TP_CUSTOMER;
            entity.CD_DEL_PRES_PROPOSED = obj.CD_DEL_PRES_PROPOSED;
            entity.TP_EQUIP = obj.TP_EQUIP;
            entity.CD_DEL_PRES_EXISTING = obj.CD_DEL_PRES_EXISTING;
            entity.QT_CONNECT_LOAD = obj.QT_CONNECT_LOAD;
            entity.QT_CONN_LOAD_INC = obj.QT_CONN_LOAD_INC;
            entity.QT_TOT_EST_DEMAND = obj.QT_TOT_EST_DEMAND;
            entity.DS_COMMENTS = obj.DS_COMMENTS;

            unitOfWork.GasLoadDetRepo.Update(entity);
            unitOfWork.Save();
        }

        public void Update(List<GasLoadDetail> objs)
        {
            foreach (var item in objs)
            {
                Update(item);
            }
        }

        public void Delete(long workRequestId, string tpCustomer, string proposed, string tpEquip)
        {
            GasLoadDetail objs = GetByEntity(unitOfWork.GasLoadDetRepo.GetSingle(m => m.CD_WR == workRequestId && m.TP_CUSTOMER == tpCustomer && m.CD_DEL_PRES_PROPOSED == proposed && m.TP_EQUIP == tpEquip));
            if (objs != null)
            {
                unitOfWork.GasLoadDetRepo.Delete(objs.CD_DIST, workRequestId, tpCustomer, proposed, tpEquip);
                unitOfWork.Save();
            }
        }

        public void Delete(GasLoadDetail obj)
        {
            Delete(obj.CD_WR, obj.TP_CUSTOMER, obj.CD_DEL_PRES_PROPOSED, obj.TP_EQUIP);
        }

        public void Delete(List<GasLoadDetail> objs)
        {
            foreach (var item in objs)
            {
                Delete(item);
            }
        }

        public void DeleteGasLoadDetails(long workRequestId, string district)
        {
            List<GasLoadDetail> objs = GetByEntities(unitOfWork.GasLoadDetRepo.Get(m => m.CD_WR == workRequestId && m.CD_DIST == district));

            foreach (var item in objs)
            {
                Delete(item.CD_WR, item.TP_CUSTOMER, item.CD_DEL_PRES_PROPOSED, item.TP_EQUIP);
            }
        }

        public void Insert(GasLoadDetail obj)
        {
            unitOfWork.GasLoadDetRepo.Insert(MapObjectToEntity(obj));
            unitOfWork.Save();
        }

        public void Insert(List<GasLoadDetail> objs)
        {
            foreach (var item in objs)
            {
                Insert(item);
            }
        }

        public GasLoadDetail MapEntityToObject(TWMGASLOADDET entity)
        {
            if (entity != null)
            {
                GasLoadDetail obj = new GasLoadDetail();

                obj.CD_DIST = entity.CD_DIST;
                obj.CD_WR = entity.CD_WR;
                obj.TP_CUSTOMER = entity.TP_CUSTOMER;
                obj.CD_DEL_PRES_PROPOSED = entity.CD_DEL_PRES_PROPOSED;
                obj.TP_EQUIP = entity.TP_EQUIP;
                obj.CD_DEL_PRES_EXISTING = entity.CD_DEL_PRES_EXISTING;
                obj.QT_CONNECT_LOAD = entity.QT_CONNECT_LOAD;
                obj.QT_CONN_LOAD_INC = entity.QT_CONN_LOAD_INC;
                obj.QT_TOT_EST_DEMAND = entity.QT_TOT_EST_DEMAND;
                obj.DS_COMMENTS = entity.DS_COMMENTS;

                return obj;
            }

            return null;
        }

        public TWMGASLOADDET MapObjectToEntity(GasLoadDetail obj)
        {
            if (obj != null)
            {
                TWMGASLOADDET entity = new TWMGASLOADDET();
                entity.CD_DIST = obj.CD_DIST;
                entity.CD_WR = obj.CD_WR;
                entity.TP_CUSTOMER = obj.TP_CUSTOMER;
                entity.CD_DEL_PRES_PROPOSED = obj.CD_DEL_PRES_PROPOSED;
                entity.TP_EQUIP = obj.TP_EQUIP;
                entity.CD_DEL_PRES_EXISTING = obj.CD_DEL_PRES_EXISTING;
                entity.QT_CONNECT_LOAD = obj.QT_CONNECT_LOAD;
                entity.QT_CONN_LOAD_INC = obj.QT_CONN_LOAD_INC;
                entity.QT_TOT_EST_DEMAND = obj.QT_TOT_EST_DEMAND;
                entity.DS_COMMENTS = obj.DS_COMMENTS;

                return entity;
            }

            return null;
        }
    }
}
