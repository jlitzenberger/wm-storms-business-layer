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
    public class CUAsbBl : BaseBl
    {


        public CUAsb GetByEntity(TWMCUPOINT_ASB entity)
        {
            return MapEntityToObject(entity);
        }

        public List<CUAsb> GetByEntities(IEnumerable<TWMCUPOINT_ASB> entities)
        {
            if (entities != null && entities.Count() > 0)
            {
                return entities.Select(m => MapEntityToObject(m)).ToList();
            }
            return null;
        }

        public List<CUAsb> GetByWorkRequestId(long workRequestId)
        {
            return GetByEntities(unitOfWork.CuPointAsbRepo.Get(m => m.CD_WR == workRequestId));
        }


        public List<CUAsb> GetByWorkPacket(long workRequestId, long workpacketId)
        {
            return GetByEntities(unitOfWork.CuPointAsbRepo.Get(m => m.CD_WR == workRequestId && m.CD_WORKPACKET == workpacketId));
        }


        public List<CUAsb> GetByWorkRequestIdandPoint(long workRequestId, string point, string pointSpan)
        {
            return GetByEntities(unitOfWork.CuPointAsbRepo.Get(m => m.CD_WR == workRequestId && m.NO_POINT == point && m.NO_POINT_SPAN == pointSpan));
        }

        public CUAsb MapEntityToObject(TWMCUPOINT_ASB entity)
        {
            CUAsb obj = new CUAsb();

            obj.WorkRequestId = entity.CD_WR;
            obj.UnitAction = entity.IND_ACTION;
            obj.Account = entity.IND_ACCT;
            obj.UnitCode = entity.CD_CU;
            obj.MuId = entity.CD_MU;
            obj.PointNumber = entity.NO_POINT;
            obj.PointSpanNumber = entity.NO_POINT_SPAN;
            obj.Usage = entity.CD_USAGE;
            obj.OnOff = entity.IND_ON_OFF;
            obj.EquipNumber = entity.NO_EQUIP;
            obj.SupplyMethod = entity.CD_SUPPLY_METHOD;
            obj.Quantity = entity.QT_ACTION.ToString();
            obj.CrewClass = entity.CD_CREW_CLASS;
            obj.UnitDetail = null;
            obj.AsbuiltDesignNumber = entity.NO_ASB_DESIGN;
            obj.BudgetItemCode = entity.CD_BDGT_ITEM;
            obj.ConstructionLaborAmount = entity.AMT_LBR_CONTRUCT;
            obj.ConstructionLaborHours = entity.HR_LBR_CONSTRUCT;
            obj.CUId = entity.ID_CU;
            obj.District = entity.CD_DIST;
            obj.FacilityCode = entity.CD_FACILITY;
            obj.FacilityLaborHours = entity.FC_LBR_HRS;
            obj.NonConstructionLaborAmount = entity.AMT_LBR_NON_CONSTR;
            obj.NonConstructionLaborHours = entity.HR_LBR_NON_CONSTR;
            obj.OrderNumber = entity.NO_ORDER;
            obj.RestorationFlag = entity.FG_RESTORATION;
            obj.TimeStampLastChanged = entity.TS_LAST_CHANGED;
            obj.WorkOrderCode = entity.CD_WO;
            obj.WorkpacketId = entity.CD_WORKPACKET;

            obj.CUFacilityAttributes = null;

            if (entity.TWMCU != null)
            {
                obj.UnitDetail = new UnitDetailBl().GetByEntity(entity.TWMCU);
            }
            // TODO: Problem with primary key on twmwrfacility... NO_ASB_DESIGN is alway null
            if (entity.TWMWRFACILITies != null && entity.TWMWRFACILITies.Count > 0)
            {
                obj.CUFacilityAttributes = new CUFacilityAttributeBl().GetByEntities(entity.TWMWRFACILITies).ToList();
            }
            return obj;
        }


        //    List<CUAsb> obj = new List<CUAsb>().ToList().MapObjectsToEntities(unitOfWork.CuPointAsbRepo.Get(m => m.CD_WR == workRequestId));           
        //    return obj;
        //}

        //public CUAsb MapEntityToObject(TWMCUPOINT_ASB entity)
        //{
        //}

        //public IEnumerable<CUAsb> MapEntitiesToObjects(IEnumerable<TWMCUPOINT_ASB> entities)
        //{
        //    List<CUAsb> objs = new List<CUAsb>();

        //    foreach (var item in entities)
        //    {
        //        objs.Add(MapEntityToObject(item));
        //    }

        //    return objs;
        //}

        //public CUAsb TransformToCUAsb(long workRequestId, string PointNumber, string PointSpanNumber, decimal? quantity, UnitDetail obj)
        //{
        //    CUAsb cuAsb = new CUAsb();

        //    cuAsb.WorkRequestId = workRequestId;
        //    cuAsb.UnitAction = obj.IND_ACTION;
        //    cuAsb.Account = obj.IND_ACCT;
        //    cuAsb.UnitCode = obj.Name;
        //    cuAsb.MUID = obj.c
        //    //cu.DesignNumber = 
        //    cuAsb.PointNumber = PointNumber;
        //    cuAsb.PointSpanNumber = PointSpanNumber;
        //    cuAsb.Usage = obj.CD_USAGE;
        //    cuAsb.OnOff = "N";
        //    cuAsb.EquipNumber = "";
        //    cuAsb.SupplyMethod = "";
        //    cuAsb.Quantity = quantity.ToString();
        //    //cuAsb.CrewClass =

         
        //    cuAsb.CUFacilityAttributes = null;
          
        //    return cuAsb;
        //}



        public TWMCUPOINT_ASB MapObjectToEntity(CUAsb obj)
        {
            TWMCUPOINT_ASB entity = new TWMCUPOINT_ASB();

            entity = MapRootObjectToEntity(obj, entity);

            return entity;
        }

        public IEnumerable<TWMCUPOINT_ASB> MapObjectsToEntities(IEnumerable<CUAsb> objs)
        {
            List<TWMCUPOINT_ASB> entities = new List<TWMCUPOINT_ASB>();

            foreach (var item in objs)
            {
                entities.Add(MapObjectToEntity(item));
            }

            return entities;
        }

        public TWMCUPOINT_ASB MapRootObjectToEntity(CUAsb obj, TWMCUPOINT_ASB entity)
        {
            entity.CD_WR = obj.WorkRequestId;
            entity.IND_ACTION = obj.UnitAction;
            entity.IND_ACCT = obj.Account;
            entity.CD_CU = obj.UnitCode;
            entity.CD_MU = obj.MuId;
            entity.NO_POINT = obj.PointNumber;
            entity.NO_POINT_SPAN = obj.PointSpanNumber;
            entity.CD_USAGE = obj.Usage;
            entity.IND_ON_OFF = obj.OnOff;
            entity.NO_EQUIP = obj.EquipNumber;
            entity.CD_SUPPLY_METHOD = obj.SupplyMethod;
            entity.QT_ACTION = Convert.ToDecimal(obj.Quantity);
            entity.CD_CREW_CLASS = obj.CrewClass;
            entity.AMT_LBR_CONTRUCT = obj.ConstructionLaborAmount;
            entity.AMT_LBR_NON_CONSTR = obj.NonConstructionLaborAmount;
            entity.CD_BDGT_ITEM = obj.BudgetItemCode;
            entity.CD_DIST = obj.District;
            entity.CD_FACILITY = obj.FacilityCode;
            entity.CD_WO = obj.WorkOrderCode;
            entity.CD_WORKPACKET = obj.WorkpacketId;
            entity.FC_LBR_HRS = obj.FacilityLaborHours;
            entity.FG_RESTORATION = obj.RestorationFlag;
            entity.HR_LBR_CONSTRUCT = obj.ConstructionLaborHours;
            entity.HR_LBR_NON_CONSTR = obj.NonConstructionLaborHours;
            entity.ID_CU = obj.CUId;
            entity.NO_ASB_DESIGN = obj.AsbuiltDesignNumber;
            entity.NO_ORDER = obj.OrderNumber;
            entity.TS_LAST_CHANGED = obj.TimeStampLastChanged;
            

            //// TODO: Problem with primary key on twmwrfacility... NO_ASB_DESIGN is alway null
            //obj.CUFacilityAttributes = new CUFacilityAttributeBl(dbManagerBase.iOriginType, dbManagerBase.iEnvironmentType).GetCUFacilityAttributes((int)entity.CD_WR, entity.NO_POINT, entity.NO_POINT_SPAN, entity.CD_CU, entity.CD_USAGE, entity.IND_ACCT, entity.IND_ON_OFF, entity.IND_ACTION, entity.NO_EQUIP, entity.CD_SUPPLY_METHOD, entity.CD_MU);
            //obj.LaborDetails = new LaborDetailBl(dbManagerBase.iOriginType, dbManagerBase.iEnvironmentType).GetLaborDetails(entity.TWMCU.TWMLBRDTLs.Where(m => m.IND_ON_OFF == obj.OnOff && m.CD_CREW_CLASS == obj.CrewClass && m.IND_ACTION == (obj.UnitAction == "S" ? "R" : obj.UnitAction)));

            return entity;
        }
    }
}
