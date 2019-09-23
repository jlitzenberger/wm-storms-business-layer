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
    public class CUStructureBl : BaseBl
    {
        public CUStructure Get(TWMCUSTRUCTURE entity)
        {
            if (entity != null)
            {
                CUStructure obj = MapEntityToObject(entity);

                return obj;
            }

            return null;
        }
        public List<CUStructure> Get(IEnumerable<TWMCUSTRUCTURE> entities)
        {
            IEnumerable<CUStructure> objs = MapEntitiesToObjects(entities);

            if (objs != null)
            {
                return objs.ToList();
            }

            return null;
        }

        public List<CUStructure> GetByEntities(IEnumerable<TWMCUSTRUCTURE> entities)
        {
            IEnumerable<CUStructure> objs = MapEntitiesToObjects(entities);

            if (objs != null)
            {
                return objs.ToList();
            }

            return null;
        }

        public List<CUStructure> GetByCU(string cuCode)
        {
            return GetByEntities(unitOfWork.CuStructureRepo.Get(m => m.CD_CU == cuCode));
        }     


        public CUStructure MapEntityToObject(TWMCUSTRUCTURE entity)
        {
            CUStructure obj = new CUStructure();

            obj.CD_CU = entity.CD_CU;
            obj.NO_MATL_ITEM = entity.NO_MATL_ITEM;
            obj.PC_ALLOWANCE = (decimal)entity.PC_ALLOWANCE;
            obj.QT_ITEM = (decimal)entity.QT_ITEM;
            obj.CD_DRAWING_SEQ = entity.CD_DRAWING_SEQ;
            obj.TS_LAST_CHANGED = entity.TS_LAST_CHANGED;

            obj.MaterialItem = null;
            if (entity.TWMMATLITEM != null)
            {
                obj.MaterialItem = new MaterialItemBl().Get(entity.TWMMATLITEM);
            }

            return obj;
        }
        public IEnumerable<CUStructure> MapEntitiesToObjects(IEnumerable<TWMCUSTRUCTURE> entities)
        {
            List<CUStructure> objs = new List<CUStructure>();

            foreach (var item in entities)
            {
                objs.Add(MapEntityToObject(item));
            }

            return objs;
        }
    }
}
