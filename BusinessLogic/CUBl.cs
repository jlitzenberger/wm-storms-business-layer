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
    public class CUBl : BaseBl
    {

        public CU Get(TWMCUPOINT_EST entity)
        {
            if (entity != null)
            {
                CU obj = MapEntityToObject(entity);

                return obj;
            }

            return null;
        }
        public List<CU> Get(IEnumerable<TWMCUPOINT_EST> entities)
        {
            IEnumerable<CU> objs = MapEntitiesToObjects(entities);

            if (objs != null)
            {
                return objs.ToList();
            }

            return null;
        }
        public List<CU> Get()
        {
            throw new NotImplementedException();
        }

        public CU GetPointCU(long workRequestId, int specId, string pointId, string pointSpanId, string unitCode,
                             string usageId, string indAccount, string indOnOff, string noEquip, string indAction, string supplyMethod, string muId)
        {
            usageId = (usageId == null) ? " " : usageId;
            indAccount = (indAccount == null) ? " " : indAccount;
            indOnOff = (indOnOff == null) ? " " : indOnOff;
            noEquip = (noEquip == null) ? " " : noEquip;
            indAction = (indAction == null) ? " " : indAction;
            supplyMethod = (supplyMethod == null) ? " " : supplyMethod;
            muId = (muId == null) ? " " : muId;

            CU obj = Get(unitOfWork.CuPointEstRepo.GetSingle(m => m.CD_WR == workRequestId
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
        public List<CU> GetPointCUs(Point point)
        {
            return Get(unitOfWork.CuPointEstRepo.Get(m => m.CD_WR == long.Parse(point.WorkRequest)
                                                             && m.NO_DESIGN == short.Parse(point.DesignNumber)
                                                             && m.NO_POINT == point.PointNumber
                                                             && m.NO_POINT_SPAN == point.PointSpanNumber
                                                             ));
        }
        public List<CU> GetPointCUs(long workRequestId, int specId, string pointId = null, string pointSpanId = null, string unitCode = null,
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

        public void CreateExternalDesign(string district, List<CU> CUPoints, ExtDesignKey key)
        {
            foreach (CU cup in CUPoints)
            {
                key.ExtDsgnFacAttSeq = null;
                if (cup.CUFacilityAttributes != null && cup.CUFacilityAttributes.Count > 0)
                {
                    key.ExtDsgnFacAttSeq = (int)GetIfSequenceNo();
                }

                //insert CUs
                CreateExternalDesign(cup, district, key);

                //insert FAs
                CreateFacilityAttributes(cup, key);
            }
        }
        public void CreateExternalDesign(CU cu, string district, ExtDesignKey key)
        {
            unitOfWork.IfExtDesignCuRepo.Insert(MapObjectToIfEntity(cu, district, key));
            unitOfWork.Save();
        }
        private void CreateFacilityAttributes(CU cup, ExtDesignKey key)
        {
            if (cup.CUFacilityAttributes != null && cup.CUFacilityAttributes.Count > 0)
            {
                foreach (CUFacilityAttribute fa in cup.CUFacilityAttributes)
                {
                    unitOfWork.IfExtDesignFacRepo.Insert(MapObjectToIfEntity(fa, key));
                    unitOfWork.Save();
                }
            }
        }

        public CU TransformToCU(string workRequestId, ExtDesignKey key, string PointNumber, string PointSpanNumber, decimal? quanity, UnitDetail obj)
        {
            CU cu = new CU();

            cu.WorkRequest = workRequestId;
            cu.UnitAction = obj.IND_ACTION;
            cu.Account = obj.IND_ACCT;
            cu.UnitCode = obj.Name;
            //cu.MUID = obj.
            //cu.DesignNumber = 
            cu.PointNumber = PointNumber;
            cu.PointSpanNumber = PointSpanNumber;
            cu.Usage = obj.CD_USAGE;
            cu.OnOff = "N";
            cu.EquipNumber = "";
            cu.SupplyMethod = "";
            cu.Quantity = quanity.ToString();         
            //cu.CrewClass =

            cu.UnitDetail = null;
            cu.CUFacilityAttributes = null;
            cu.LaborDetails = null;

            return cu;
        }

        public CU MapEntityToObject(TWMCUPOINT_EST entity)
        {
            CU obj = new CU();

            obj.WorkRequest = entity.CD_WR.ToString();
            obj.UnitAction = entity.IND_ACTION;
            obj.Account = entity.IND_ACCT;
            obj.UnitCode = entity.CD_CU;
            obj.MUID = entity.CD_MU;
            obj.DesignNumber = entity.NO_DESIGN.ToString();
            obj.PointNumber = entity.NO_POINT;
            obj.PointSpanNumber = entity.NO_POINT_SPAN;
            obj.Usage = entity.CD_USAGE;
            obj.OnOff = entity.IND_ON_OFF;
            obj.EquipNumber = entity.NO_EQUIP;
            obj.SupplyMethod = entity.CD_SUPPLY_METHOD;
            obj.Quantity = entity.QT_ACTION.ToString();
            obj.CrewClass = entity.CD_CREW_CLASS;
            obj.District = entity.CD_DIST;
            obj.RestorationFlag = entity.FG_RESTORATION;
            obj.MaterialSubExistsFlag = entity.FG_MATL_SUB_EXISTS;
            obj.WorkpacketId = entity.CD_WORKPACKET;

            obj.UnitDetail = null;
            obj.CUFacilityAttributes = null;
            obj.LaborDetails = null;

            if (entity.TWMCU != null)
            {
                obj.UnitDetail = new UnitDetailBl().GetByEntity(entity.TWMCU);
            }
            // TODO: Problem with primary key on twmwrfacility... NO_ASB_DESIGN is alway null
            if (entity.TWMWRFACILITies != null && entity.TWMWRFACILITies.Count > 0)
            {
                obj.CUFacilityAttributes = new CUFacilityAttributeBl().GetByEntities(entity.TWMWRFACILITies).ToList();
            }
            if (entity.TWMCU.TWMLBRDTLs != null)
            {
                obj.LaborDetails = new LaborDetailBl().Get(entity.TWMCU.TWMLBRDTLs.Where(m => m.IND_ON_OFF == obj.OnOff && m.CD_CREW_CLASS == obj.CrewClass && m.IND_ACTION == (obj.UnitAction == "S" ? "R" : obj.UnitAction)));
            }

            return obj;
        }
        public IEnumerable<CU> MapEntitiesToObjects(IEnumerable<TWMCUPOINT_EST> entities)
        {
            List<CU> objs = new List<CU>();

            foreach (var item in entities)
            {
                objs.Add(MapEntityToObject(item));
            }

            return objs;
        }
        public TWMCUPOINT_EST MapObjectToEntity(CU obj)
        {
            TWMCUPOINT_EST entity = new TWMCUPOINT_EST();

            entity = MapRootObjectToEntity(obj, entity);

            return entity;
        }
        public IEnumerable<TWMCUPOINT_EST> MapObjectsToEntities(IEnumerable<CU> objs)
        {
            List<TWMCUPOINT_EST> entities = new List<TWMCUPOINT_EST>();

            foreach (var item in objs)
            {
                entities.Add(MapObjectToEntity(item));
            }

            return entities;
        }
        public TWMCUPOINT_EST MapRootObjectToEntity(CU obj, TWMCUPOINT_EST entity)
        {
            entity.CD_WR = Convert.ToInt64(obj.WorkRequest);
            entity.IND_ACTION = obj.UnitAction;
            entity.IND_ACCT = obj.Account;
            entity.CD_CU = obj.UnitCode;
            entity.CD_MU = obj.MUID;
            entity.NO_DESIGN = Convert.ToInt16(obj.DesignNumber);
            entity.NO_POINT = obj.PointNumber;
            entity.NO_POINT_SPAN = obj.PointSpanNumber;
            entity.CD_USAGE = obj.Usage;
            entity.IND_ON_OFF = obj.OnOff;
            entity.NO_EQUIP = obj.EquipNumber;
            entity.CD_SUPPLY_METHOD = obj.SupplyMethod;
            entity.QT_ACTION = Convert.ToDecimal(obj.Quantity);
            entity.CD_CREW_CLASS = obj.CrewClass;
            entity.CD_DIST = obj.District;
            entity.FG_RESTORATION = obj.RestorationFlag;
            entity.FG_MATL_SUB_EXISTS = obj.MaterialSubExistsFlag;
            entity.CD_WORKPACKET = obj.WorkpacketId;


            //entity.TWMCU = new UnitDetailBl(dbManagerBase.iOrigin, dbManagerBase.iEnvironment).MapObjectsToEntities(obj.UnitDetail).ToList();

            //// TODO: Problem with primary key on twmwrfacility... NO_ASB_DESIGN is alway null
            //obj.CUFacilityAttributes = new CUFacilityAttributeBl(dbManagerBase.iOriginType, dbManagerBase.iEnvironmentType).GetCUFacilityAttributes((int)entity.CD_WR, entity.NO_POINT, entity.NO_POINT_SPAN, entity.CD_CU, entity.CD_USAGE, entity.IND_ACCT, entity.IND_ON_OFF, entity.IND_ACTION, entity.NO_EQUIP, entity.CD_SUPPLY_METHOD, entity.CD_MU);
            //obj.LaborDetails = new LaborDetailBl(dbManagerBase.iOriginType, dbManagerBase.iEnvironmentType).GetLaborDetails(entity.TWMCU.TWMLBRDTLs.Where(m => m.IND_ON_OFF == obj.OnOff && m.CD_CREW_CLASS == obj.CrewClass && m.IND_ACTION == (obj.UnitAction == "S" ? "R" : obj.UnitAction)));

            return entity;
        }
        public TWMIFEXTDSGN_CU MapObjectToIfEntity(CU cu, string district, ExtDesignKey key)
        {
            TWMIFEXTDSGN_CU entity = new TWMIFEXTDSGN_CU();

            entity.CD_SEQ_EXTDSGN = GetIfSequenceNo();
            entity.TS_EXTDSGN = key.TsExtDsgn;
            entity.ID_OPER = key.IdOper;
            entity.NO_POINT = String.IsNullOrEmpty(cu.PointNumber) ? " " : cu.PointNumber;  //NotNull
            entity.NO_POINT_SPAN = String.IsNullOrEmpty(cu.PointSpanNumber) ? " " : cu.PointSpanNumber;  //NotNull
            entity.CD_CU = cu.UnitCode;
            entity.IND_ACCT = String.IsNullOrEmpty(cu.Account) ? GetCUAccount(cu.UnitCode) : cu.Account;  //NotNull - C, M or O
            entity.IND_ON_OFF = String.IsNullOrEmpty(cu.OnOff) ? "N" : cu.OnOff;  //NotNull
            entity.FG_CNTCR = "N";  //NotNull
            entity.CD_USAGE = GetCUUsage(cu.UnitCode);  //NotNull - get from twmCU table
            entity.CD_SUPPLY_METHOD = String.IsNullOrEmpty(cu.SupplyMethod) ? " " : cu.SupplyMethod;  //NotNull
            entity.NO_EQUIP = String.IsNullOrEmpty(cu.EquipNumber) ? " " : cu.EquipNumber;  //NotNull
            entity.CD_WO = string.Empty;
            entity.DT_COMPLETE = null;
            entity.IND_ACTION = String.IsNullOrEmpty(cu.UnitAction) ? " " : cu.UnitAction;  //NotNull
            entity.QT_ACTION = String.IsNullOrEmpty(cu.Quantity) ? -1 : (decimal)int.Parse(cu.Quantity);    //NotNull
            entity.FC_DIFFICULTY = null;

            entity.CD_DIST = district;
            entity.CD_WR = String.IsNullOrEmpty(cu.WorkRequest) ? 0 : Convert.ToInt32(cu.WorkRequest);
            entity.CD_OWNERSHIP = string.Empty;
            entity.CD_BDGT_ITEM = string.Empty;
            entity.CD_CREW = string.Empty;
            entity.IND_PROCESS = "A";   //NotNull - A = add, M = modify, D = delete
            entity.FG_ERROR = "N";  //NotNull
            if (key.ExtDsgnFacAttSeq == null)
            {
                entity.NO_FACILITY = null;
            }
            else
            {
                entity.NO_FACILITY = key.ExtDsgnFacAttSeq;
            }
            entity.NO_APPLICATION = null;
            entity.CD_MU = String.IsNullOrEmpty(cu.MUID) ? " " : cu.MUID;  //NotNull
            entity.CD_SEQ_ERROR_RUN = null;
            entity.CD_WORKPACKET = null;

            return entity;
        }
        public TWMIFEXTDSGN_FAC MapObjectToIfEntity(CUFacilityAttribute fa, ExtDesignKey key)
        {
            TWMIFEXTDSGN_FAC entity = new TWMIFEXTDSGN_FAC();

            entity.CD_SEQ_EXTDSGN = GetIfSequenceNo2();
            entity.TS_EXTDSGN = key.TsExtDsgn; //PK
            entity.ID_OPER = key.IdOper; //PK
            entity.NO_FACILITY = (long)key.ExtDsgnFacAttSeq;  //PK -- seq generated
            entity.CD_SEQ = fa.IdSeq;  //PK
            entity.TXT_FAC_VALUE = fa.Value;
            entity.CD_ATTRIBUTE = fa.AttributeName; //PK
            entity.FG_ERROR = "N";
            entity.CD_SEQ_ERROR_RUN = null;

            return entity;
        }

        // TODO: see if this can be done through the ORM
        public string GetCUUsage(string cu)
        {
            if (cu.Substring(cu.Length - 2) == "FU")
            {
                cu = cu.Remove(cu.Length - 3);
            }

            string sql = " select a.CD_USAGE from TWMCUUSAGE a where a.cd_cu = '" + cu + "' and a.fg_default = 'Y'";

            // TODO: fix this -- MAKE this call a generic RunRawSql Repository
            return unitOfWork.GenericSqlRepo.RunRawSql<string>(sql).ToList()[0];


        }
        // TODO: see if this can be done through the ORM
        public string GetCUAccount(string cu)
        {
            string sql = " select a.ind_acct from twmcu a where a.cd_cu = " + cu;

            // TODO: fix this -- MAKE this call a generic RunRawSql Repository
            return unitOfWork.GenericSqlRepo.RunRawSql<string>(sql).ToList()[0];

        }
        // TODO: see if this can be done through the ORM
        private long GetIfSequenceNo()
        {
            return Convert.ToInt64(unitOfWork.GenericSqlRepo.RunRawSql<Decimal>(" select WE_S_SO_EXTDSG.nextval FROM dual").ToList()[0]);
        }
        // TODO: see if this can be done through the ORM
        private long GetIfSequenceNo2()
        {
            return Convert.ToInt64(unitOfWork.GenericSqlRepo.RunRawSql<Decimal>(" select WE_S_SO_EXTDSG_FACATT.nextval FROM dual").ToList()[0]);
        }
    }
}
