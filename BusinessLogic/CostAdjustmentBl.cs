using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.STORMS.BusinessLayer.Models;
using WM.STORMS.DataAccessLayer;

namespace WM.STORMS.BusinessLayer.BusinessLogic
{
    public class CostAdjustmentBl : BaseBl
    {
        public CostAdjustment GetById(long WorkRequestId)
        {
            return Get(unitOfWork.CostInformationRepo.GetSingle(m => m.CD_WR == WorkRequestId));
        }
        public void Create(CostAdjustment obj)
        {
            CreateCostAdjustment(MapObjectToEntity(obj));
        }
        private void CreateCostAdjustment(TWMCOSTINFORMATION entity)
        {
            if (entity != null)
            {
                unitOfWork.CostInformationRepo.Insert(entity);
                unitOfWork.Save();
            }
        }
        public void Update(CostAdjustment obj)
        {
            var ca = unitOfWork.CostInformationRepo.GetSingle(m => m.CD_WR == obj.WorkRequestId);

            ca.CD_WR = obj.WorkRequestId;
            ca.CD_DIST = obj.District;
            ca.CD_ENTITY = obj.Entity;
            ca.AMT_ANNUAL_REV_EST = obj.AmountAnnualRevEst;
            ca.AMT_FIXED_BID = obj.AmountFixedBid;
            ca.MLT_AMT_LBR_CMPNY = obj.MultiplierAmountLaborCompany;
            ca.MLT_AMT_LBR_CNTCR = obj.MultiplierAmountLaborContractor;
            ca.MLT_AMT_MATL = obj.MultiplierAmountMaterial;
            ca.MLT_HR_LBR_CMPNY = obj.MultiplierHoursLaborCompany;
            ca.QT_AFUDC_MONTHS = Convert.ToInt16(obj.QuantityAfudcMonths);
            ca.NO_ASB_DESIGN = obj.NoAsbDesign;
            ca.CD_BID_ITEM = obj.BidItem;
            ca.QT_BID_ITEM = obj.QuantityBidItem;

            UpdateCostAdjustment(ca);
        }
        private void UpdateCostAdjustment(TWMCOSTINFORMATION entity)
        {
            if (entity != null)
            {
                unitOfWork.CostInformationRepo.Update(entity);
                unitOfWork.Save();
            }
        }
        public CostAdjustment Get(TWMCOSTINFORMATION entity)
        {
            return MapEntityToObject(entity);
        }
        public List<CostAdjustment> Get(IEnumerable<TWMCOSTINFORMATION> entities)
        {
            IEnumerable<CostAdjustment> objs = MapEntitiesToObjects(entities);

            if (objs != null)
            {
                return objs.ToList();
            }

            return null;
        }
        public CostAdjustment MapEntityToObject(TWMCOSTINFORMATION entity)
        {
            if (entity != null)
            {
                CostAdjustment obj = new CostAdjustment();

                obj.WorkRequestId = entity.CD_WR;
                obj.District = entity.CD_DIST;
                obj.Entity = entity.CD_ENTITY;
                obj.AmountAnnualRevEst = entity.AMT_ANNUAL_REV_EST;
                obj.AmountFixedBid = entity.AMT_FIXED_BID;
                obj.MultiplierAmountLaborCompany = entity.MLT_AMT_LBR_CMPNY;
                obj.MultiplierAmountLaborContractor = entity.MLT_AMT_LBR_CNTCR;
                obj.MultiplierAmountMaterial = entity.MLT_AMT_MATL;
                obj.MultiplierHoursLaborCompany = entity.MLT_HR_LBR_CMPNY;

                obj.QuantityAfudcMonths = entity.QT_AFUDC_MONTHS;
                obj.NoAsbDesign = entity.NO_ASB_DESIGN;
                obj.BidItem = entity.CD_BID_ITEM;
                obj.QuantityBidItem = entity.QT_BID_ITEM;


                return obj;
            }

            return null;
        }
        public TWMCOSTINFORMATION MapObjectToEntity(CostAdjustment obj)
        {
            if (obj != null)
            {
                return new TWMCOSTINFORMATION
                {
                     CD_WR = obj.WorkRequestId,
                     CD_DIST = obj.District,
                     CD_ENTITY = obj.Entity,

                     AMT_ANNUAL_REV_EST = obj.AmountAnnualRevEst,
                     AMT_FIXED_BID = obj.AmountFixedBid,

                     MLT_AMT_LBR_CMPNY = obj.MultiplierAmountLaborCompany,
                     MLT_AMT_LBR_CNTCR = obj.MultiplierAmountLaborContractor,
                     MLT_AMT_MATL = obj.MultiplierAmountMaterial,
                     MLT_HR_LBR_CMPNY = obj.MultiplierHoursLaborCompany,

                     //QT_AFUDC_MONTHS = obj.QuantityAfudcMonths,
                     NO_ASB_DESIGN = obj.NoAsbDesign,
                     CD_BID_ITEM = obj.BidItem,
                     QT_BID_ITEM = obj.QuantityBidItem
                };
            }
            return null;
        }
        public IEnumerable<CostAdjustment> MapEntitiesToObjects(IEnumerable<TWMCOSTINFORMATION> entities)
        {
            List<CostAdjustment> objs = new List<CostAdjustment>();

            foreach (var item in entities)
            {
                objs.Add(MapEntityToObject(item));
            }

            return objs;
        }
    }
}
