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
    public class CUEstBl : BaseBl
    {

        public CUEst Get(TWMCUPOINT_EST entity)
        {
            if (entity != null)
            {
                CUEst obj = MapEntityToObject(entity);

                return obj;
            }

            return null;
        }
        public List<CUEst> Get(IEnumerable<TWMCUPOINT_EST> entities)
        {
            IEnumerable<CUEst> objs = MapEntitiesToObjects(entities);

            if (objs != null)
            {
                return objs.ToList();
            }

            return null;
        }
        public List<CUEst> Get()
        {
            throw new NotImplementedException();
        }

        public CUEst GetPointCUEst(long workRequestId, int specId, string pointId, string pointSpanId, string unitCode,
                             string usageId, string indAccount, string indOnOff, string noEquip, string indAction, string supplyMethod, string muId)
        {
            usageId = (usageId == null) ? " " : usageId;
            indAccount = (indAccount == null) ? " " : indAccount;
            indOnOff = (indOnOff == null) ? " " : indOnOff;
            noEquip = (noEquip == null) ? " " : noEquip;
            indAction = (indAction == null) ? " " : indAction;
            supplyMethod = (supplyMethod == null) ? " " : supplyMethod;
            muId = (muId == null) ? " " : muId;

            CUEst obj = Get(unitOfWork.CuPointEstRepo.GetSingle(m => m.CD_WR == workRequestId
                                                             && m.NO_DESIGN == specId
                                                             && m.NO_POINT == pointId
                                                             && m.NO_POINT_SPAN == pointSpanId
                                                             && m.CD_CU == unitCode

                                                             && m.CD_USAGE == usageId
                                                             && m.IND_ACCT == indAccount
                                                             && m.IND_ON_OFF == indOnOff
                                                             && m.NO_EQUIP == noEquip
                                                             && m.IND_ACTION == indAction
                                                             && m.CD_SUPPLY_METHOD == supplyMethod
                                                             && m.CD_MU == muId

                                                             ));

            return obj;
        }
        public List<CUEst> GetPointCUEsts(Point point)
        {
            return Get(unitOfWork.CuPointEstRepo.Get(m => m.CD_WR == long.Parse(point.WorkRequest)
                                                             && m.NO_DESIGN == short.Parse(point.DesignNumber)
                                                             && m.NO_POINT == point.PointNumber
                                                             && m.NO_POINT_SPAN == point.PointSpanNumber
                                                             ));
        }
        public List<CUEst> GetPointCUEsts(long workRequestId, int specId, string pointId = null, string pointSpanId = null, string unitCode = null,
                                string usageId = null, string indAccount = null, string indOnOff = null, string noEquip = null, string indAction = null, string supplyMethod = null, string muId = null)
        {
            return Get(unitOfWork.CuPointEstRepo.Get(m => m.CD_WR == workRequestId
                                                             && m.NO_DESIGN == specId
                                                             && (m.NO_POINT == pointId || pointId == null)
                                                             && (m.NO_POINT_SPAN == pointSpanId || pointSpanId == null)
                                                             && (m.CD_CU == unitCode || unitCode == null)
                                                             && (m.CD_USAGE == usageId || usageId == null)
                                                             && (m.IND_ACCT == indAccount || indAccount == null)
                                                             && (m.IND_ON_OFF == indOnOff || indOnOff == null)
                                                             && (m.NO_EQUIP == noEquip || noEquip == null)
                                                             && (m.IND_ACTION == indAction || indAction == null)
                                                             && (m.CD_SUPPLY_METHOD == supplyMethod || supplyMethod == null)
                                                             && (m.CD_MU == muId || muId == null)
                                                             ));
        }

        public CUEst MapEntityToObject(TWMCUPOINT_EST entity)
        {
            CUEst obj = new CUEst();            
            obj.CD_WR = entity.CD_WR;

            obj.IND_ACTION = entity.IND_ACTION;
            obj.IND_ACCT = entity.IND_ACCT;
            obj.CD_CU = entity.CD_CU;
            obj.CD_MU = entity.CD_MU;
            obj.NO_DESIGN = entity.NO_DESIGN;
            obj.NO_POINT = entity.NO_POINT;
            obj.NO_POINT_SPAN = entity.NO_POINT_SPAN;
            obj.CD_USAGE = entity.CD_USAGE;
            obj.IND_ON_OFF = entity.IND_ON_OFF;
            obj.NO_EQUIP = entity.NO_EQUIP;
            obj.CD_SUPPLY_METHOD = entity.CD_SUPPLY_METHOD;
            obj.QT_ACTION = entity.QT_ACTION;
            obj.CD_CREW_CLASS = entity.CD_CREW_CLASS;
            obj.CD_DIST = entity.CD_DIST;
            obj.FG_RESTORATION = entity.FG_RESTORATION;
            obj.FG_MATL_SUB_EXISTS = entity.FG_MATL_SUB_EXISTS;
            obj.CD_WORKPACKET = entity.CD_WORKPACKET;

            obj.CUFacilityAttributes = null;
           

            // TODO: Problem with primary key on twmwrfacility... NO_ASB_DESIGN is alway null
            if (entity.TWMWRFACILITies != null && entity.TWMWRFACILITies.Count > 0)
            {
                obj.CUFacilityAttributes = new CUFacilityAttributeBl().GetByEntities(entity.TWMWRFACILITies).ToList();
            }
         

            return obj;
        }
        public IEnumerable<CUEst> MapEntitiesToObjects(IEnumerable<TWMCUPOINT_EST> entities)
        {
            List<CUEst> objs = new List<CUEst>();

            foreach (var item in entities)
            {
                objs.Add(MapEntityToObject(item));
            }

            return objs;
        }
        public TWMCUPOINT_EST MapObjectToEntity(CUEst obj)
        {
            TWMCUPOINT_EST entity = new TWMCUPOINT_EST();

            entity = MapRootObjectToEntity(obj, entity);

            return entity;
        }
        public IEnumerable<TWMCUPOINT_EST> MapObjectsToEntities(IEnumerable<CUEst> objs)
        {
            List<TWMCUPOINT_EST> entities = new List<TWMCUPOINT_EST>();

            foreach (var item in objs)
            {
                entities.Add(MapObjectToEntity(item));
            }

            return entities;
        }
        public TWMCUPOINT_EST MapRootObjectToEntity(CUEst obj, TWMCUPOINT_EST entity)
        {
            entity.CD_WR = obj.CD_WR;
            entity.IND_ACTION = obj.IND_ACTION;
            entity.IND_ACCT = obj.IND_ACCT;
            entity.CD_CU = obj.CD_CU;
            entity.CD_MU = obj.CD_MU;
            entity.NO_DESIGN = Convert.ToInt16(obj.NO_DESIGN);
            entity.NO_POINT = obj.NO_POINT;
            entity.NO_POINT_SPAN = obj.NO_POINT_SPAN;
            entity.CD_USAGE = obj.CD_USAGE;
            entity.IND_ON_OFF = obj.IND_ON_OFF;
            entity.NO_EQUIP = obj.NO_EQUIP;
            entity.CD_SUPPLY_METHOD = obj.CD_SUPPLY_METHOD;
            entity.QT_ACTION = Convert.ToDecimal(obj.QT_ACTION);
            entity.CD_CREW_CLASS = obj.CD_CREW_CLASS;
            entity.CD_DIST = obj.CD_DIST;
            entity.FG_RESTORATION = obj.FG_RESTORATION;
            entity.FG_MATL_SUB_EXISTS = obj.FG_MATL_SUB_EXISTS;
            entity.CD_WORKPACKET = obj.CD_WORKPACKET;


            //entity.TWMCU = new UnitDetailBl(dbManagerBase.iOrigin, dbManagerBase.iEnvironment).MapObjectsToEntities(obj.UnitDetail).ToList();

            //// TODO: Problem with primary key on twmwrfacility... NO_ASB_DESIGN is alway null
            //obj.CUFacilityAttributes = new CUFacilityAttributeBl(dbManagerBase.iOriginType, dbManagerBase.iEnvironmentType).GetCUFacilityAttributes((int)entity.CD_WR, entity.NO_POINT, entity.NO_POINT_SPAN, entity.CD_CU, entity.CD_USAGE, entity.IND_ACCT, entity.IND_ON_OFF, entity.IND_ACTION, entity.NO_EQUIP, entity.CD_SUPPLY_METHOD, entity.CD_MU);
            //obj.LaborDetails = new LaborDetailBl(dbManagerBase.iOriginType, dbManagerBase.iEnvironmentType).GetLaborDetails(entity.TWMCU.TWMLBRDTLs.Where(m => m.IND_ON_OFF == obj.OnOff && m.CD_CREW_CLASS == obj.CrewClass && m.IND_ACTION == (obj.UnitAction == "S" ? "R" : obj.UnitAction)));

            return entity;
        }
      
    }
}
