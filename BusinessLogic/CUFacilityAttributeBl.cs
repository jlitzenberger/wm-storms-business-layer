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
    public class CUFacilityAttributeBl : BaseBl
    {
        public CUFacilityAttribute GetByEntity(TWMWRFACILITY entity)
        {
            return MapEntityToObject(entity);
        }
        public CUFacilityAttribute Get(TWMWRFACILITY entity)
        {
            if (entity != null)
            {
                CUFacilityAttribute obj = MapEntityToObject(entity);

                return obj;
            }

            return null;
        }
        public List<CUFacilityAttribute> GetByEntities(IEnumerable<TWMWRFACILITY> entities)
        {
            if (entities != null && entities.Count() > 0)
            {
                return entities.Select(m => MapEntityToObject(m)).ToList();
            }
            return null;
        }
        //IEnumerable<CUFacilityAttribute> objs = MapEntitiesToObjects(entities);

        //if (objs != null)
        //{
        //    return objs.ToList();
        //}

        //return null;
        //}
        public List<CUFacilityAttribute> Get()
        {
            throw new NotImplementedException();
        }

        public CUFacilityAttribute GetCUFacilityAttribute(int workRequestId, int seqId)
        {
            return Get(unitOfWork.WrfacilityRepo.GetSingle(m => m.CD_WR == workRequestId && m.CD_SEQ == seqId));
        }

        public List<CUFacilityAttribute> GetCUFacAttsByWorkRequest(long workRequestId, string noPoint, string noPointSpan)
        {
            return GetByEntities(unitOfWork.WrfacilityRepo.Get(m => m.CD_WR == workRequestId && m.NO_POINT == noPoint && m.NO_POINT_SPAN == noPointSpan));
        }

        //public List<CUFacilityAttribute> GetCUFacilityAttributes(int workRequestId, string noPoint, string noPointSpan, string cu, string cdUsage, string indAcct, string indOnOff, string indAction, string noEquip, string cdSupplyMethod, string cdMu, string cdAttribute, int seqId)
        //{
        //    return Get(unitOfWork.WrfacilityRepo.Get(m => m.CD_WR == (long)workRequestId
        //                                                        && m.NO_POINT == noPoint
        //                                                        && m.NO_POINT_SPAN == noPointSpan
        //                                                        && m.CD_CU == cu
        //                                                        && m.CD_USAGE == cdUsage
        //                                                        && m.IND_ACCT == indAcct
        //                                                        && m.IND_ON_OFF == indOnOff
        //                                                        && m.IND_ACTION == indAction
        //                                                        && m.NO_EQUIP == noEquip
        //                                                        && m.CD_SUPPLY_METHOD == cdSupplyMethod
        //                                                        && m.CD_MU == cdMu
        //                                                        && m.CD_ATTRIBUTE == cdAttribute
        //                                                        && m.CD_SEQ == seqId
        //                                                        ));
        //}

        public CUFacilityAttribute MapEntityToObject(TWMWRFACILITY entity)
        {
            CUFacilityAttribute obj = new CUFacilityAttribute();

            obj.WorkRequestId = entity.CD_WR;
            obj.CuName = entity.CD_CU;
            obj.AttributeName = entity.CD_ATTRIBUTE;
            obj.PointNumber = entity.NO_POINT;
            obj.PointSpan = entity.NO_POINT_SPAN;
            obj.Usage = entity.CD_USAGE;
            obj.SupplyMethod = entity.CD_SUPPLY_METHOD;
            obj.Account = entity.IND_ACCT;
            obj.Action = entity.IND_ACTION;
            obj.OnOff = entity.IND_ON_OFF;
            obj.Equip = entity.NO_EQUIP;
            obj.MuId = entity.CD_MU;
            obj.IdSeq = entity.CD_SEQ;
            obj.Value = entity.TXT_FAC_VALUE;
            obj.Facility = entity.CD_FACILITY;
            //     obj.AsbuiltDesignNumber = entity.NO_ASB_DESIGN;
            obj.IdCU = entity.ID_CU;
            obj.District = entity.CD_DIST;
            obj.RequiredFlag = entity.FG_REQUIRED;

            //    obj.CD_WR = entity.CD_WR;
            //    obj.CD_CU = entity.CD_CU;
            //    obj.CD_ATTRIBUTE = entity.CD_ATTRIBUTE;
            //    obj.CD_DIST = entity.CD_DIST;
            //    obj.CD_FACILITY = entity.CD_FACILITY;
            //    obj.CD_MU = entity.CD_MU;
            //    obj.CD_SEQ = entity.CD_SEQ;
            //    obj.CD_SUPPLY_METHOD = entity.CD_SUPPLY_METHOD;
            //    obj.CD_USAGE = entity.CD_USAGE;
            //    obj.FG_REQUIRED = entity.FG_REQUIRED;
            //    obj.ID_CU = entity.ID_CU;
            //    obj.IND_ACCT = entity.IND_ACCT;
            //    obj.IND_ACTION = entity.IND_ACTION;
            //    obj.IND_ON_OFF = entity.IND_ON_OFF;
            ////    obj.NO_ASB_DESIGN = entity.NO_ASB_DESIGN;
            //    obj.NO_EQUIP = entity.NO_EQUIP;
            //    obj.NO_POINT = entity.NO_POINT;
            //    obj.NO_POINT_SPAN = entity.NO_POINT_SPAN;
            //    obj.TXT_FAC_VALUE = entity.TXT_FAC_VALUE;

            return obj;
        }
        public IEnumerable<CUFacilityAttribute> MapEntitiesToObjects(IEnumerable<TWMWRFACILITY> entities)
        {
            List<CUFacilityAttribute> objs = new List<CUFacilityAttribute>();

            foreach (var item in entities)
            {
                objs.Add(MapEntityToObject(item));
            }

            return objs;
        }

    }
}
