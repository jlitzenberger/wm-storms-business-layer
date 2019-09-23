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
    public class GeoBl : BaseBl
    {

        public Geo Get(TWMWR entity)
        {
            if (entity != null)
            {
                Geo obj = MapEntityToObject(entity);

                return obj;
            }

            return null;
        }

        public Geo MapEntityToObject(TWMWR entity)
        {
            Geo obj = new Geo();

            obj.WorkRequest = (int)entity.CD_WR;
            obj.Dist = entity.CD_DIST;
            obj.Area = entity.CD_AREA;
            obj.Zone = entity.CD_ZONE;
            obj.XCoordinate = entity.AD_GR_1;
            obj.YCoordinate = entity.AD_GR_2;

            return obj;
        }   
    }
}
