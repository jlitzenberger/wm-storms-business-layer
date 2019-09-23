using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Common;
using WM.STORMS.BusinessLayer.Models;
using WM.STORMS.DataAccessLayer;

namespace WM.STORMS.BusinessLayer.BusinessLogic
{
    public class ZoneBl : BaseBl
    {

        public List<Zone> Get(List<TWMZONE> zones)
        {
            if (zones != null && zones.Count > 0)
            {
                zones = zones.Where(m => m.FG_ACTIVE == "Y").ToList();
                return zones.Select(m => MapEntityToObject(m)).ToList();
            }

            return null;
        }

        public List<Zone> GetAll()
        {
            return unitOfWork.ZoneRepo.Get(m => m.FG_ACTIVE == "Y").Select(m => MapEntityToObject(m)).ToList();
        }

        public Zone GetById(string zoneId)
        {
            return MapEntityToObject(unitOfWork.ZoneRepo.GetSingle(m => m.CD_ZONE == zoneId && m.FG_ACTIVE == "Y"));
        }

        private Zone MapEntityToObject(TWMZONE obj)
        {
            if (obj != null)
            {
                return new Zone
                {
                    Description = obj.DS_ZONE,
                    Name = obj.CD_ZONE,
                    Area = obj.CD_AREA,
                    District = obj.CD_DIST
                };
            }
            return null;
        }
    }
}
