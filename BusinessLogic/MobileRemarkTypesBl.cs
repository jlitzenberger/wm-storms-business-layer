using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WM.Common.Interfaces;
using WM.STORMS.BusinessLayer.Models;
using WM.STORMS.DataAccessLayer;
using WM.Common;

namespace WM.STORMS.BusinessLayer.BusinessLogic
{
    public class MobileRemarkTypesBl : BaseBl
    {


        public List<MobileRemarkTypes> GetAll()
        {
            return unitOfWork.MobileRemarkTypesRepo.GetAll().Select(m => MapEntityToObject(m)).ToList();
        }

        public MobileRemarkTypes GetByType(string type)
        {
            return MapEntityToObject(unitOfWork.MobileRemarkTypesRepo.GetSingle(m => m.TP_REMARK == type));
        }

        private MobileRemarkTypes MapEntityToObject(WE_T_FAP_AD_REMARKTYPES obj)
        {
            if (obj != null)
            {
                return new MobileRemarkTypes
                {
                    OperatorId = obj.ID_OPER,
                    RemarkType = obj.TP_REMARK,
                    TimeStampLastChanged = obj.TS_LAST_CHANGED
                };
            }

            return null;
        }
    }
}
       