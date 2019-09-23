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
    public class MUStructureBl : BaseBl
    {
        public MUStructure Get(TWMMUSTRUCTURE entity)
        {
            if (entity != null)
            {
                MUStructure obj = MapEntityToObject(entity);

                return obj;
            }

            return null;
        }
        public List<MUStructure> GetByEntities(IEnumerable<TWMMUSTRUCTURE> entities)
        {
            IEnumerable<MUStructure> objs = MapEntitiesToObjects(entities);

            if (objs != null)
            {
                return objs.ToList();
            }

            return null;
        }
    
        public List<MUStructure> GetByMU(string muCode)
        {            
            return GetByEntities(unitOfWork.MuStructureRepo.Get(m => m.CD_MU == muCode));         
        }     

        public MUStructure MapEntityToObject(TWMMUSTRUCTURE entity)
        {
            MUStructure obj = new MUStructure();

            obj.CD_CU = entity.CD_CU;
            obj.CD_MU = entity.CD_MU;
            obj.CD_MUSTRUCTURE = entity.CD_MUSTRUCTURE;
            obj.CD_USAGE = entity.CD_USAGE;
            obj.IND_ACCT = entity.IND_ACCT;
            obj.IND_ACTION = entity.IND_ACTION;
            obj.QT_CU = (decimal)entity.QT_CU;         

            return obj;
        }

        public IEnumerable<MUStructure> MapEntitiesToObjects(IEnumerable<TWMMUSTRUCTURE> entities)
        {
            List<MUStructure> objs = new List<MUStructure>();

            foreach (var item in entities)
            {
                objs.Add(MapEntityToObject(item));
            }

            return objs;
        }
    }
}
