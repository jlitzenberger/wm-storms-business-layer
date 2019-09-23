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
    public class UnitDetailBl : BaseBl
    {


        public UnitDetail GetByEntity(TWMCU entity)
        {
            return MapEntityToObject(entity);
        }

        public List<UnitDetail> GetByEntities(IEnumerable<TWMCU> entities)
        {
            if (entities != null && entities.Count() > 0)
            {
                return entities.Select(m => MapEntityToObject(m)).ToList();
            }

            return null;
        }

        public List<UnitDetail> GetActive(bool active = true)
        {
            List<UnitDetail> objs = null;
            if (active)
            {
                objs = new List<UnitDetail>();
                objs = GetByEntities(unitOfWork.CuRepo.GetAll().Where(m => m.DT_DISCONTINUED > DateTime.Now));
            }
            else
            {
                objs = new List<UnitDetail>();
                objs = GetByEntities(unitOfWork.CuRepo.GetAll().Where(m => m.DT_DISCONTINUED > DateTime.Now));
            }

            return objs;
        }

        public UnitDetail GetUnitDetail(string UnitCode)
        {
            UnitDetail obj = GetByEntity(unitOfWork.CuRepo.GetSingle(m => m.CD_CU == UnitCode));

            return obj;
        }

        public List<UnitDetail> GetUnitDetailsByMU(string mu)
        {
            List<UnitDetail> objs = GetByEntities(unitOfWork.CuRepo.Get(m => m.TWMMUSTRUCTUREs.Any(x => x.CD_MU == mu)));

            return objs;
        }

        public List<UnitDetail> GetByDate(string cumuInd, DateTime lastRunDate)
        {
            return unitOfWork.CuRepo.Get(m => m.IND_CU_MU == cumuInd && m.TS_LAST_CHANGED >= lastRunDate).Select(m => MapEntityToObject(m)).ToList();
        }

        public List<UnitDetail> GetCUByDate(DateTime lastRunDate)
        {
            return unitOfWork.CuRepo.Get(m => m.IND_CU_MU == "C" && m.TS_LAST_CHANGED >= lastRunDate).Select(m => MapEntityToObject(m)).ToList();
        }

        public List<UnitDetail> GetMUByDate(DateTime lastRunDate)
        {
            return unitOfWork.CuRepo.Get(m => m.IND_CU_MU == "M" && m.TS_LAST_CHANGED >= lastRunDate).Select(m => MapEntityToObject(m)).ToList();
        }

        public UnitDetail MapEntityToObject(TWMCU entity)
        {
            if (entity != null)
            {
                UnitDetail obj = new UnitDetail();

                obj.Name = entity.CD_CU;
                obj.CD_CU_KIND = entity.CD_CU_KIND;
                obj.CD_CU_MAKE = entity.CD_CU_MAKE;
                obj.CD_LBR = entity.CD_LBR;
                obj.CD_UOM = entity.CD_UOM;
                obj.CD_USAGE = entity.CD_USAGE;
                obj.Description = entity.DS_CU;
                obj.DiscontinuedDate = entity.DT_DISCONTINUED;
                obj.EffectiveDate = entity.DT_EFFECTIVE;
                obj.FC_LBR_HRS = entity.FC_LBR_HRS;
                obj.FG_CAPITAL = entity.FG_CAPITAL;
                obj.FG_MAINTENANCE = entity.FG_MAINTENANCE;
                obj.FG_OPERATIONS = entity.FG_OPERATIONS;
                obj.FG_RESTORATION = entity.FG_RESTORATION;
                obj.FG_TEMPORARY = entity.FG_TEMPORARY;
                obj.CUMUIndicator = entity.IND_CU_MU;
                obj.IND_UTIL = entity.IND_UTIL;
                obj.TP_ASSET = entity.TP_ASSET;
                obj.TP_CU = entity.TP_CU;
                obj.CD_SPEC = entity.CD_SPEC;
                obj.CD_FACILITY = entity.CD_FACILITY;
                obj.FG_HR_CHANGED = entity.FG_HR_CHANGED;
                obj.TS_LAST_CHANGED = entity.TS_LAST_CHANGED;
                obj.AMT_MATL_ITEM_TOT = entity.AMT_MATL_ITEM_TOT;
                obj.AMT_SALVAGE_TOT = entity.AMT_SALVAGE_TOT;
                obj.AMT_SCRAP_TOT = entity.AMT_SCRAP_TOT;
                obj.CD_CATEGORY = entity.CD_CATEGORY;
                obj.IND_ACCT = entity.IND_ACCT;
                obj.IND_ACTION = entity.IND_ACTION;
                obj.FG_HIDE_CU = entity.FG_HIDE_CU;
                obj.CD_BID_ITEM = entity.CD_BID_ITEM;
                obj.FG_MOBILE = entity.FG_MOBILE;
                //obj.CD_CREW_CLASS = entity.

                obj.CUStructures = null;
                if (entity.TWMCUSTRUCTUREs != null && entity.TWMCUSTRUCTUREs.Count > 0)
                {
                    obj.CUStructures = new CUStructureBl().Get(entity.TWMCUSTRUCTUREs);
                }
                obj.Facility = null;
                if (entity.TWMFACILITY != null)
                {
                    obj.Facility = new FacilityBl().Get(entity.TWMFACILITY);
                }

                return obj;
            }

            return null;
        }
    }
}
