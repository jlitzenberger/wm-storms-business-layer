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
    public class MaterialItemBl : BaseBl
    {


        public MaterialItem Get(TWMMATLITEM entity)
        {
            if (entity != null)
            {
                MaterialItem obj = MapEntityToObject(entity);

                return obj;
            }

            return null;
        }

        public List<MaterialItem> Get(IEnumerable<TWMMATLITEM> entities)
        {
            IEnumerable<MaterialItem> objs = MapEntitiesToObjects(entities);

            if (objs != null)
            {
                return objs.ToList();
            }

            return null;
        }

        public List<MaterialItem> GetAllByDate(DateTime lastRunDate)
        {
            return unitOfWork.MatlItemRepo.Get(m => m.FG_ACTIVE == "Y").Select(m => MapEntityToObject(m)).Where(d => d.TS_LAST_CHANGED >= lastRunDate).ToList();

        }      

        public MaterialItem MapEntityToObject(TWMMATLITEM entity)
        {
            if (entity != null)
            {
                MaterialItem obj = new MaterialItem();

                obj.NO_MATL_ITEM = entity.NO_MATL_ITEM;
                obj.AMT_MATL_ITEM = (decimal)entity.AMT_MATL_ITEM;
                obj.AMT_SALVAGE = (decimal)entity.AMT_SALVAGE;
                obj.AMT_SCRAP = (decimal)entity.AMT_SCRAP;
                obj.CD_MATL_CLASS = entity.CD_MATL_CLASS;
                obj.CD_UOM = entity.CD_UOM;
                obj.DS_MATL_ITEM = entity.DS_MATL_ITEM;
                obj.DY_LEAD_TIME = entity.DY_LEAD_TIME;
                obj.FG_ACTIVE = entity.FG_ACTIVE;
                obj.FG_PREV_CAPITALIZE = entity.FG_PREV_CAPITALIZE;
                obj.FG_STOCK_ITEM = entity.FG_STOCK_ITEM;
                obj.PC_ALLOWANCE = (decimal)entity.PC_ALLOWANCE;
                obj.QT_MIN = (decimal)entity.QT_MIN;
                obj.FG_TRUCK_STOCK = entity.FG_TRUCK_STOCK;
                obj.FG_MAJOR = entity.FG_MAJOR;
                obj.FG_ASSET = entity.FG_ASSET;
                obj.CD_STOCKING = entity.CD_STOCKING;
                obj.TS_LAST_CHANGED = entity.TS_LAST_CHANGED;

                return obj;
            }

            return null;
        }
        public IEnumerable<MaterialItem> MapEntitiesToObjects(IEnumerable<TWMMATLITEM> entities)
        {
            List<MaterialItem> objs = new List<MaterialItem>();

            foreach (var item in entities)
            {
                objs.Add(MapEntityToObject(item));
            }

            return objs;
        }
    }
}