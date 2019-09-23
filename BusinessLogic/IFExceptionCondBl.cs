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
    public class IFExceptionCondBl : BaseBl
    {
        public IFExceptionCond GetByEntity(TWMIFEXCEPTIONCOND entity)
        {
            return MapEntityToObject(entity);
        }

        public List<IFExceptionCond> GetByEntities(IEnumerable<TWMIFEXCEPTIONCOND> entities)
        {
            if (entities != null && entities.Count() > 0)
            {
                return entities.Select(m => MapEntityToObject(m)).ToList();
            }
            return null;
        }

        public List<IFExceptionCond> GetByWorkRequestId(long workRequestId)
        {
            return GetByEntities(unitOfWork.IfExceptConditionRepo.Get(m => m.CD_WR == workRequestId));
        }


        public void Create(IFExceptionCond obj)
        {
            CreateIFExceptionCond(MapObjectToEntity(obj));
        }

        private void CreateIFExceptionCond(TWMIFEXCEPTIONCOND entity)
        {
            if (entity.CD_SEQ == 0)
            {
                entity.CD_SEQ = GetIfSequenceNo();
            } 
            unitOfWork.IfExceptConditionRepo.Insert(entity);
            unitOfWork.Save();
        } 

        public void Update(IFExceptionCond obj)
        {
            TWMIFEXCEPTIONCOND entity = MapObjectToEntity(obj);

            if (entity != null)
            {
                unitOfWork.IfExceptConditionRepo.Update(entity);
            }
        }

        private TWMIFEXCEPTIONCOND MapObjectToEntity(IFExceptionCond obj)
        {
            if (obj != null)
            {
                return new TWMIFEXCEPTIONCOND
                {
                    CD_DIST = obj.CD_DIST,
                    CD_WR = obj.CD_WR,
                    ID_OPER = obj.ID_OPER,
                    CD_SEQ = obj.CD_SEQ,
                    DS_COND = obj.DS_COND,
                    FG_ERROR = obj.FG_ERROR,
                    TS_IFEXCEPTIONCOND = obj.TS_IFEXCEPTIONCOND
                };
            }
            return null;
        }

       
        private IFExceptionCond MapEntityToObject(TWMIFEXCEPTIONCOND entity)
        {
            if (entity != null)
            {
                return new IFExceptionCond
                {
                    CD_DIST = entity.CD_DIST,
                    CD_WR = entity.CD_WR,
                    ID_OPER = entity.ID_OPER,
                    CD_SEQ = entity.CD_SEQ,
                    DS_COND = entity.DS_COND,
                    FG_ERROR = entity.FG_ERROR,
                    TS_IFEXCEPTIONCOND = entity.TS_IFEXCEPTIONCOND
                };
            }
            return null;
        }
        private long GetIfSequenceNo()
        {
            List<Decimal> a = unitOfWork.GenericSqlRepo.RunRawSql<Decimal>("select WETWMIFEXCP.nextval FROM dual").ToList();

            return Convert.ToInt64(a[0]);
        }

    }
}
