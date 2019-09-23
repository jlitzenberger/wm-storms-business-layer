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
    public class AreaBl : BaseBl
    {
        private ZoneBl zoneBl;
        public AreaBl()            
        {
            zoneBl = new ZoneBl();
        }

        public List<Area> Get(List<TWMAREA> areas)
        {
            if (areas != null && areas.Count > 0)
            {
                areas = areas.Where(m => m.FG_ACTIVE == "Y").ToList();
                return areas.Select(m => MapEntityToObject(m)).ToList();
            }

            return null;
        }

        public List<Area> GetAll()
        {
            return unitOfWork.AreaRepo.Get(m => m.FG_ACTIVE == "Y").Select(m => MapEntityToObject(m)).ToList();
        }

        public Area GetById(string areaId)
        {
            return MapEntityToObject(unitOfWork.AreaRepo.GetSingle(m => m.CD_AREA == areaId && m.FG_ACTIVE == "Y"));
        }

        private Area MapEntityToObject(TWMAREA obj)
        {
            if (obj != null)
            {
                return new Area
                {
                    Description = obj.DS_AREA,
                    Name = obj.CD_AREA,
                    Zones = zoneBl.Get(obj.TWMZONEs.ToList())
                };
            }
            return null;
        }
    }
}
