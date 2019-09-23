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
    public class IFExtDsgnFacAttBl : BaseBl
    {
        public void Create(IFExtDsgnFacAtt obj)
        {
            unitOfWork.IfExtDesignFacRepo.Insert(MapObjectToEntity(obj));
            unitOfWork.Save();
        }

        public IFExtDsgnFacAtt GetByEntity(TWMIFEXTDSGN_FAC entity)
        {
            return MapEntityToObject(entity);
        }

        public List<IFExtDsgnFacAtt> GetAll()
        {
            return unitOfWork.IfExtDesignFacRepo.GetAll().Select(m => MapEntityToObject(m)).ToList();                 
        }

        public TWMIFEXTDSGN_FAC MapObjectToEntity(IFExtDsgnFacAtt obj)
        {
            if (obj != null)
            {
                return new TWMIFEXTDSGN_FAC
                {

                    CD_ATTRIBUTE = obj.AttributeCode,
                    NO_FACILITY = obj.FacilityNumber,
                    TXT_FAC_VALUE = obj.FacilityValueText.Trim(),                  
                    CD_SEQ_EXTDSGN = obj.ExternalDesignSequence,
                    ID_OPER = obj.OperatorId,
                    TS_EXTDSGN = obj.ExternalDesignTimeStamp,
                    CD_SEQ_ERROR_RUN = obj.ErrorRunSequence,
                    FG_ERROR = obj.ErrorFlag,
                    CD_SEQ = obj.SequenceCode                  
                };
            }
            return null;
        }

        public IFExtDsgnFacAtt MapEntityToObject(TWMIFEXTDSGN_FAC obj)
        {
            if (obj != null)
            {
                return new IFExtDsgnFacAtt
                {
                    AttributeCode = obj.CD_ATTRIBUTE,
                    FacilityNumber = obj.NO_FACILITY,
                    FacilityValueText = obj.TXT_FAC_VALUE,
                    SequenceCode = obj.CD_SEQ,
                    ErrorRunSequence = obj.CD_SEQ_ERROR_RUN,
                    ExternalDesignSequence = obj.CD_SEQ_EXTDSGN,
                    ExternalDesignTimeStamp = obj.TS_EXTDSGN,
                    OperatorId = obj.ID_OPER,
                    ErrorFlag = obj.FG_ERROR
                };
            }
            return null;
        }

        private long GetIfSequenceNo()
        {
            return Convert.ToInt64(unitOfWork.GenericSqlRepo.RunRawSql<Decimal>(" select WE_S_SO_EXTDSG.nextval FROM dual").ToList()[0]);
        }
    }
}
