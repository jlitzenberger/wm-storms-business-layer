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
    public class LaborDetailBl : BaseBl
    {


        public LaborDetail Get(TWMLBRDTL entity)
        {
            if (entity != null)
            {
                LaborDetail obj = MapEntityToObject(entity);

                return obj;
            }

            return null;
        }

        public List<LaborDetail> Get(IEnumerable<TWMLBRDTL> entities)
        {
            IEnumerable<LaborDetail> objs = MapEntitiesToObjects(entities);

            if (objs != null)
            {
                return objs.ToList();
            }

            return null;
        }

        public LaborDetail MapEntityToObject(TWMLBRDTL entity)
        {
            LaborDetail obj = new LaborDetail();

            obj.LaborCode = entity.CD_LBR;
            obj.IndOnOff = entity.IND_ON_OFF;
            obj.IndAction = entity.IND_ACTION;
            obj.CrewClass = entity.CD_CREW_CLASS;
            obj.FlagActive = entity.FG_ACTIVE;
            obj.LaborHours = (decimal)entity.HR_LBR;
            obj.AdditionalLaborHours = (decimal)entity.HR_LBR_ADDL;

            return obj;
        }

        public IEnumerable<LaborDetail> MapEntitiesToObjects(IEnumerable<TWMLBRDTL> entities)
        {
            List<LaborDetail> objs = new List<LaborDetail>();

            foreach (var item in entities)
            {
                objs.Add(MapEntityToObject(item));
            }

            return objs;
        }
    }
}
