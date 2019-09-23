using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.STORMS.BusinessLayer.Models;
using WM.STORMS.DataAccessLayer;
using WM.Common;
using WM.Common.Interfaces;

namespace WM.STORMS.BusinessLayer.BusinessLogic
{
    public class ExceptionConditionBl : BaseBl        
    {

        //public ExceptionCondition Get(TWMEXCEPTIONCOND entity)
        //{
        //    return MapEntityToObject(entity);
        //}    

        public List<ExceptionCondition> GetByWorkRequestId(long WorkRequestId)
        {
            IEnumerable<TWMEXCEPTIONCOND> exceptionconds = unitOfWork.ExceptConditionRepo.Get(m => m.CD_WR == WorkRequestId);

            if (exceptionconds != null && exceptionconds.Count() > 0)
            {
                return exceptionconds.Select(m => MapEntityToObject(m)).ToList();
            }

            return null;
        }

        //public void Create(IFExceptionCond obj)
        //{
        //    if (obj.CD_SEQ == 0)
        //    {
        //        obj.CD_SEQ = GetIfSequenceNo();
        //    }
        //    unitOfWork.IfExceptConditionRepo.Insert(MapObjectToEntity(obj));
        //    unitOfWork.Save();
        //}
       
        //private TWMIFEXCEPTIONCOND MapObjectToEntity(IFExceptionCond obj)
        //{
        //    if (obj != null)
        //    {
        //        return new TWMIFEXCEPTIONCOND
        //        {
        //            CD_DIST = obj.CD_DIST,
        //            CD_WR = obj.CD_WR,
        //            ID_OPER = obj.ID_OPER,                       
        //            CD_SEQ = obj.CD_SEQ,                  
        //            DS_COND = obj.DS_COND,
        //            FG_ERROR = obj.FG_ERROR,
        //            TS_IFEXCEPTIONCOND = obj.TS_IFEXCEPTIONCOND                 
        //        };
        //    }
        //    return null;
        //}

        private ExceptionCondition MapEntityToObject(TWMEXCEPTIONCOND entity)
        {
            if (entity != null)
            {
                return new ExceptionCondition
                {                    
                    District = entity.CD_DIST,
                    WorkRequestId = entity.CD_WR,
                    OperatorId = entity.ID_OPER,
                    SequenceNumber = Convert.ToInt64(entity.CD_WQ_SEQ),
                    ConditionDescription = entity.DS_COND,
                    ConditionCode = entity.CD_COND,
                    ExceptionConditionTimeStamp = entity.TS_LAST_CHANGED                    
                };
            }
            return null;
        }

        //private long GetIfSequenceNo()
        //{
        //    List<Decimal> a = unitOfWork.GenericSqlRepo.RunRawSql<Decimal>("select we_s_fap_execp_cond.nextval FROM dual").ToList();

        //    return Convert.ToInt64(a[0]);
        //}
    
    }
}
