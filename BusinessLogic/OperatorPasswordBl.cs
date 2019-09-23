using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.STORMS.BusinessLayer.Models;
using WM.STORMS.DataAccessLayer;

namespace WM.STORMS.BusinessLayer.BusinessLogic
{
    class OperatorPasswordBl : BaseBl
    {
        public OperatorPassword GetByEntity(TWMOPERPASSWORD entity)
        {
            return MapEntityToObject(entity);
        }
        public OperatorPassword GetById(string operId)
        {
            return GetByEntity(unitOfWork.OperRepo.GetSingle(m => m.ID_OPER == operId)?.TWMOPERPASSWORD);
        }
        public OperatorPassword MapEntityToObject(TWMOPERPASSWORD entity)
        {
            if (entity != null)
            {
                OperatorPassword obj = new OperatorPassword();

                obj.IdOper = entity.ID_OPER;
                obj.Password = entity.CD_PASSWORD;
                obj.ChangedAtLogin = entity.FG_CHANGE_PWD_AT_LOGIN;
                obj.DateChanged = entity.DT_PWD_CHANGED;

                return obj;
            }
            return null;
        }
    }
}
