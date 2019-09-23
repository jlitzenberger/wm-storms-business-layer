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
    public class RemarkTypeBl : BaseBl
    {

        public List<RemarkType> GetAll()
        {
            return unitOfWork.RemarkTypeRepo.GetAll().Select(m => MapEntityToObject(m)).ToList();
        }

        public RemarkType GetByType(string type)
        {
            return MapEntityToObject(unitOfWork.RemarkTypeRepo.GetSingle(m => m.TP_REMARK == type));
        }     

        private RemarkType MapEntityToObject(TWMREMARKTYPE obj)
        {
            if (obj != null)
            {
                return new RemarkType
                {
                    ActiveFlag = obj.FG_ACTIVE,
                    InterfaceFlag = obj.FG_INTERFACE,
                    Type = obj.TP_REMARK,
                    RemarkTypeDescription = obj.DS_REMARK_TYPE                    
                };
            }

            return null;
        }
    }
}
