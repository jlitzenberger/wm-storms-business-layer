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
    public class WRTrackingBl : BaseBl
    {


        public List<WRTracking> GetByWorkRequestId(long WorkRequestId)
        {

            IEnumerable<TWMWRAUDIT> wrtrackings = unitOfWork.TwmwrAuditRepo.Get(m => m.CD_WR == WorkRequestId);

            if (wrtrackings != null && wrtrackings.Count() > 0)
            {
                return wrtrackings.Select(m => MapEntityToObject(m)).ToList();
            }

            return null;
        }

        public WRTracking GetById(long id)
        {
            return MapEntityToObject(unitOfWork.TwmwrAuditRepo.GetSingle(m => m.ID_WR_AUDIT == id));
        }

        public void Create(WRTracking obj)
        {
            if (obj.id == null || obj.id == 0)
            {
                obj.id = GetTWMWRAuditSequenceNo();
            }
            unitOfWork.TwmwrAuditRepo.Insert(MapObjectToEntity(obj));
            unitOfWork.Save();
        }

        private long GetTWMWRAuditSequenceNo()
        {
            return unitOfWork.GenericSqlRepo.RunRawSql<long>("SELECT QWM25WRAUDIT.nextval FROM dual").ToList()[0];
        }

        private TWMWRAUDIT MapObjectToEntity(WRTracking obj)
        {
            if (obj != null)
            {
                return new TWMWRAUDIT
                {
                    CD_DIST = obj.District,
                    CD_STATUS = obj.Status,
                    CD_WR = obj.WorkRequestId,
                    ID_OPER = obj.OperatorId,
                    NM_FUNCTION = obj.Function,
                    TP_CHANGE = obj.TypeOfChange,
                    TS_CHANGE = obj.ChangeDate,
                    ID_WR_AUDIT = Convert.ToInt64(obj.id)
                };
            }
            return null;
        }

        private WRTracking MapEntityToObject(TWMWRAUDIT obj)
        {
            if (obj != null)
            {
                return new WRTracking
                {
                    District = obj.CD_DIST,
                    Status = obj.CD_STATUS,
                    WorkRequestId = obj.CD_WR,
                    OperatorId = obj.ID_OPER,
                    Function = obj.NM_FUNCTION,
                    TypeOfChange = obj.TP_CHANGE,
                    ChangeDate = obj.TS_CHANGE,
                    id = obj.ID_WR_AUDIT
                };
            }
            return null;
        }
    }
}
