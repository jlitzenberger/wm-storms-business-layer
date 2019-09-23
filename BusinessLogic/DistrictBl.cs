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

    public class DistrictBl : BaseBl
    {
        private AreaBl areaBl;
        public DistrictBl()            
        {
            areaBl = new AreaBl();
        }

        public List<District> GetAll()
        {
            return unitOfWork.DistrictRepo.Get(m => m.FG_ACTIVE == "Y").Select(m => MapEntityToObject(m)).ToList();
        }

        public District GetById(string districtId)
        {
            return MapEntityToObject(unitOfWork.DistrictRepo.GetSingle(m => m.CD_DIST == districtId && m.FG_ACTIVE == "Y"));
        }

        private District MapEntityToObject(TWMDIST obj)
        {
            if (obj != null)
            {
                return new District
                {
                    Description = obj.DS_DIST,
                    Name = obj.CD_DIST,
                    Areas = areaBl.Get(obj.TWMAREAs.ToList())
                };
            }
            return null;
        }
    }
}
