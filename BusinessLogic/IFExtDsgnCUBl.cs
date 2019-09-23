using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WM.STORMS.BusinessLayer.Models;
using WM.STORMS.DataAccessLayer;
using WM.STORMS.DataAccessLayer.Repositories;
using WM.Common;
using WM.Common.Interfaces;
using System.Data.Entity.Core.Objects;

namespace WM.STORMS.BusinessLayer.BusinessLogic
{
    public class IFExtDsgnCUBl : BaseBl
    {
     
        public IFExtDsgnCU GetByEntity(TWMIFEXTDSGN_CU entity)
        {
            return MapEntityToObject(entity);
        }

        public IFExtDsgnCU Get(long workRequest, string pointId, string span, string cuCode)
        {
            return GetByEntity(unitOfWork.IfExtDesignCuRepo.GetSingle(m => m.CD_WR == workRequest && m.NO_POINT.Trim() == pointId.Trim()  && m.NO_POINT_SPAN.Trim() == span.Trim() && m.CD_CU == cuCode));
        }

        public void Delete(IFExtDsgnCU obj)
        {
            TWMIFEXTDSGN_CU entity = MapObjectToEntity(obj);

            if (entity != null)
            {
                {
                    unitOfWork.IfExtDesignCuRepo.Delete(entity.CD_SEQ_EXTDSGN, entity.TS_EXTDSGN, entity.ID_OPER);
                }

                unitOfWork.Save();
            }
        }

     

        public void CreateExternalDesignCuIf(IFExtDsgnCU cu, ExtDesignKey key)
        {
            unitOfWork.IfExtDesignCuRepo.Insert(MapObjectToIfEntity(cu, key));
            unitOfWork.Save();
        }

        public void CreateExternalDesignCu(List<IFExtDsgnCU> cus, ExtDesignKey key)
        {
            IEnumerable<TWMIFEXTDSGN_CU> entities = MapObjectsToIfEntities(cus, key);

            if (entities != null && entities.Count() > 0)
            {
                foreach (var item in entities)
                {
                    unitOfWork.IfExtDesignCuRepo.Insert(item);
                    unitOfWork.Save();
                }
            }          
        }

        public WMException CreateExternalDesignFromIf(IFExtDsgnCU twmifextdsgn_cu_obj, DateTime timeOfCreate, string oper, string asbOrEstInd)
        {
            WMException WMexcept = null;
            ExtDesignKey key = new ExtDesignKey() { TsExtDsgn = timeOfCreate, IdOper = oper};
          //   foreach (IFExtDsgnCU cuIf in twmifextdsgn_cu_objs)
          //  {


            ///////////////////
            ////get the next Oracle WE_S_SO_EXTDSG
            //  ExtDesignKey key = new ExtDesignKey() { TsExtDsgn = DateTime.Now, IdOper = twmifextdsgn_pt_obj.OperatorId };
         //   ExtDesignKey key = new ExtDesignKey() { TsExtDsgn = twmifextdsgn_cu_objs[0].ExternalDesignTimeStamp, IdOper = twmifextdsgn_cu_objs[0].OperatorId };

                
            ///////////////////
            ////insert Points//
            //      new PointBl().CreateExternalDesign(design, design.Points, key);

            ///////////////////////
            ////insert CUs /FAC's//
            //     CreateCUsFacilityAttributes(design, key);

                key.ExtDsgnFacAttSeq = null;
                if (twmifextdsgn_cu_obj.IfExtDsgnFacAtt != null && twmifextdsgn_cu_obj.IfExtDsgnFacAtt.Count > 0)
                {
                    key.ExtDsgnFacAttSeq = (int)GetIfSequenceNo();                
                }

                CreateExternalDesignCuIf(twmifextdsgn_cu_obj, key);

                CreateFacilityAttributes(twmifextdsgn_cu_obj, key);
         //    }        
             
            ///////////////////
            ////call P Driver//            
         //    WMexcept = ExecutePDriver(twmifextdsgn_cu_obj, key);


            return WMexcept;
        }

        public WMException CallExternalDesign(IFExtDsgnCU twmifextdsgn_cu_obj, bool finished, DateTime timeofcreate, string oper, string asbOrExtInd)
        {
            WMException WMexcept = null;
            ExtDesignKey key = new ExtDesignKey() { TsExtDsgn = timeofcreate, IdOper = oper };
            //   foreach (IFExtDsgnCU cuIf in twmifextdsgn_cu_objs)
            //  {


            ///////////////////
            ////get the next Oracle WE_S_SO_EXTDSG
            //  ExtDesignKey key = new ExtDesignKey() { TsExtDsgn = DateTime.Now, IdOper = twmifextdsgn_pt_obj.OperatorId };
            //   ExtDesignKey key = new ExtDesignKey() { TsExtDsgn = twmifextdsgn_cu_objs[0].ExternalDesignTimeStamp, IdOper = twmifextdsgn_cu_objs[0].OperatorId };


            ///////////////////
            ////insert Points//
            //      new PointBl().CreateExternalDesign(design, design.Points, key);

            ///////////////////////
            ////insert CUs /FAC's//
            //     CreateCUsFacilityAttributes(design, key);

            //key.ExtDsgnFacAttSeq = null;
            //if (twmifextdsgn_cu_obj.IfExtDsgnFacAtt != null && twmifextdsgn_cu_obj.IfExtDsgnFacAtt.Count > 0)
            //{
            //    key.ExtDsgnFacAttSeq = (int)GetIfSequenceNo();
            //}

            //CreateExternalDesignCuIf(twmifextdsgn_cu_obj, key);

            //CreateFacilityAttributes(twmifextdsgn_cu_obj, key);
            //    }        

            ///////////////////
            ////call P Driver//            
            WMexcept = ExecutePDriver(twmifextdsgn_cu_obj, key, asbOrExtInd);


            return WMexcept;
        }


        private void CreateFacilityAttributes(IFExtDsgnCU cup, ExtDesignKey key)
        {
            if (cup.IfExtDsgnFacAtt != null && cup.IfExtDsgnFacAtt.Count > 0)
            {
                foreach (IFExtDsgnFacAtt fa in cup.IfExtDsgnFacAtt)
                {                    
                    unitOfWork.IfExtDesignFacRepo.Insert(MapObjectToFacIfEntity(fa, key));
                    unitOfWork.Save();
                }
            }
        }       

        private WMException ExecutePDriver(IFExtDsgnCU ifcu, ExtDesignKey key, string asbOrExtInd)
        {
            WMException wme = null;

            DesignCUIfError error = unitOfWork.IfExtDsgnCuRepo.Insert(MapObjectToIfEntity(ifcu, key), asbOrExtInd);
         
            if (error != null)
            {
                wme = CreateWMException(error.v_fg_error
                                       , error.v_cd_seq_error
                                       , error.v_cd_sqlcode
                                       , error.v_nm_interface
                                       , error.v_ts_error
                                       , error.v_txt_sqlerrtext
                                       , error.v_txt_iferror
                                       , error.v_cd_seq_error_run
                                       , error.v_cd_district
                                       , error.v_cd_workpacket
                                       , error.v_cd_crew
                                       , error.v_cd_project
                                       , error.v_id_empl
                                       , error.v_cd_fleet
                                       , error.v_nm_table
                                       , error.v_nm_column
                                       , error.v_fg_data_error
                                       , error.v_ts_error_logged);
            }




            if (wme == null)
            {
            }

            return wme;
        }

        private WMException CreateWMException(ObjectParameter v_fg_error
                                               , ObjectParameter v_cd_seq_error
                                               , ObjectParameter v_cd_sqlcode
                                               , ObjectParameter v_nm_interface
                                               , ObjectParameter v_ts_error
                                               , ObjectParameter v_txt_sqlerrtext
                                               , ObjectParameter v_txt_iferror
                                               , ObjectParameter v_cd_seq_error_run
                                               , ObjectParameter v_cd_district
                                               , ObjectParameter v_cd_workpacket
                                               , ObjectParameter v_cd_crew
                                               , ObjectParameter v_cd_project
                                               , ObjectParameter v_id_empl
                                               , ObjectParameter v_cd_fleet
                                               , ObjectParameter v_nm_table
                                               , ObjectParameter v_nm_column
                                               , ObjectParameter v_fg_data_error
                                               , ObjectParameter v_ts_error_logged)
        {
            WMException wme = new WMException();

            // TODO make sure these "null" should not be string.empty
            wme.cd_seq_error = v_cd_seq_error.Value.ToString() == "null" ? 0 : int.Parse(v_cd_seq_error.Value.ToString());
            wme.cd_sqlcode = v_cd_sqlcode.Value.ToString() == "null" ? 0 : int.Parse(v_cd_sqlcode.Value.ToString());
            wme.txt_iferror = v_nm_interface.Value.ToString();
            wme.ts_error = Convert.ToDateTime(v_ts_error.Value.ToString());
            wme.txt_sqlerrtext = v_txt_sqlerrtext.Value.ToString();
            wme.txt_iferror = v_txt_iferror.Value.ToString();
            wme.cd_seq_error_run = v_cd_seq_error_run.Value.ToString() == string.Empty ? 0 : int.Parse(v_cd_seq_error_run.Value.ToString());
            ////wme. = cmd.Parameters["v_cd_district"].Value.ToString();
            ////wme.cd_wr = int.Parse(cmd.Parameters["v_cd_workrequest"].Value.ToString());
            wme.cd_workpacket = v_cd_workpacket.Value.ToString() == string.Empty ? 0 : int.Parse(v_cd_workpacket.Value.ToString());
            wme.cd_project = v_cd_project.Value.ToString();
            wme.cd_crew = v_cd_crew.Value.ToString();
            ////wme. = cmd.Parameters["v_id_empl"].Value.ToString();
            wme.cd_fleet = v_cd_fleet.Value.ToString();
            wme.nm_table = v_nm_table.Value.ToString();
            wme.nm_column = v_nm_column.Value.ToString();
            wme.fg_data_error = v_fg_data_error.Value.ToString();
            if (v_ts_error_logged.Value.ToString() == string.Empty)
            {
                wme.ts_error_logged = null;
            }
            else
            {
                Convert.ToDateTime(v_ts_error_logged.Value.ToString());
            }

            return wme;
        }

        public TWMIFEXTDSGN_FAC MapObjectToFacIfEntity(IFExtDsgnFacAtt fa, ExtDesignKey key)
        {
            TWMIFEXTDSGN_FAC entity = new TWMIFEXTDSGN_FAC();

            entity.CD_SEQ_EXTDSGN = GetIfSequenceNo2();
            entity.TS_EXTDSGN = key.TsExtDsgn; 
            entity.ID_OPER = key.IdOper; 
            entity.NO_FACILITY = (long)key.ExtDsgnFacAttSeq;
            entity.CD_SEQ = fa.SequenceCode;  
            entity.TXT_FAC_VALUE = fa.FacilityValueText;
            entity.CD_ATTRIBUTE = fa.AttributeCode; 
            entity.FG_ERROR = "N";
            entity.CD_SEQ_ERROR_RUN = 0;

            return entity;
        }

        public TWMIFEXTDSGN_CU MapObjectToIfEntity(IFExtDsgnCU cu, ExtDesignKey key)
        {
            TWMIFEXTDSGN_CU entity = new TWMIFEXTDSGN_CU();

            entity.CD_SEQ_EXTDSGN = GetIfSequenceNo();
     //       entity.CD_SEQ_EXTDSGN = cu.ExternalDesignSequence;
            entity.TS_EXTDSGN = key.TsExtDsgn;
            entity.ID_OPER = key.IdOper;
            entity.NO_POINT = String.IsNullOrEmpty(cu.PointNumber) ? " " : cu.PointNumber;  //NotNull
            entity.NO_POINT_SPAN = String.IsNullOrEmpty(cu.PointSpanNumber) ? " " : cu.PointSpanNumber;  //NotNull
            entity.CD_CU = cu.CUCode;
          //  entity.IND_ACCT = cu.AccountIndicator;
            entity.IND_ACCT = String.IsNullOrEmpty(cu.AccountIndicator) ? GetCUAccount(cu.CUCode) : cu.AccountIndicator;  //NotNull - C, M or O
            entity.IND_ON_OFF = String.IsNullOrEmpty(cu.OnOffIndicator) ? "N" : cu.OnOffIndicator;  //NotNull
            entity.FG_CNTCR = cu.ContractorFlag;  //NotNull            
            entity.CD_USAGE = String.IsNullOrWhiteSpace(cu.UsageCode) ? GetCUUsage(cu.CUCode) : cu.UsageCode;  //NotNull - get from twmCU table
            entity.CD_SUPPLY_METHOD = String.IsNullOrEmpty(cu.SupplyMethodCode) ? " " : cu.SupplyMethodCode;  //NotNull
            entity.NO_EQUIP = String.IsNullOrEmpty(cu.EquipmentNumber) ? " " : cu.EquipmentNumber;  //NotNull
            entity.CD_WO = string.Empty;
            entity.DT_COMPLETE = DateTime.Now;
         //   entity.DT_COMPLETE = cu.CompletionDate;
            entity.IND_ACTION = String.IsNullOrEmpty(cu.ActionIndicator) ? " " : cu.ActionIndicator;  //NotNull
          //  entity.QT_ACTION = String.IsNullOrEmpty(cu.ActionQuantity) ? -1 : (decimal)int.Parse(cu.ActionQuantity);    //NotNull
            entity.QT_ACTION = cu.ActionQuantity;
            entity.FC_DIFFICULTY = null;

            entity.CD_DIST = cu.District;
          //  entity.CD_WR = String.IsNullOrEmpty(cu.WorkRequest) ? 0 : Convert.ToInt32(cu.WorkRequest);
            entity.CD_WR = cu.WorkRequest;
            entity.CD_WORKPACKET = cu.Workpacket;
            entity.CD_OWNERSHIP = string.Empty;
            entity.CD_BDGT_ITEM = string.Empty;
            entity.CD_CREW = cu.CrewCode;
          //  entity.IND_PROCESS = "A";   //NotNull - A = add, M = modify, D = delete
            entity.IND_PROCESS = cu.ProcessIndicator;   //NotNull - A = add, M = modify, D = delete
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
            if (cu.MUCode == "NONE")
            {
                cu.MUCode = " ";
            }
            entity.CD_MU = String.IsNullOrEmpty(cu.MUCode) ? " " : cu.MUCode;  //NotNull
            entity.CD_SEQ_ERROR_RUN = null;        

            return entity;
        }

        public IEnumerable<TWMIFEXTDSGN_CU> MapObjectsToIfEntities(List<IFExtDsgnCU> objs, ExtDesignKey key)
        {
            List<TWMIFEXTDSGN_CU> entities = new List<TWMIFEXTDSGN_CU>();

            foreach (var item in objs)
            {
                entities.Add(MapObjectToIfEntity(item, key));
            }

            return entities;
        }


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
       
        public string GetCUAccount(string cu)
        {
            string sql = " select a.ind_acct from twmcu a where a.cd_cu = '" + cu + "'";

            // TODO: fix this -- MAKE this call a generic RunRawSql Repository
            return unitOfWork.GenericSqlRepo.RunRawSql<string>(sql).ToList()[0];

        }
       // -------------------------

        private TWMIFEXTDSGN_CU MapObjectToCuIfEntity(IFExtDsgnCU ifcu, ExtDesignKey key)
        {
            TWMIFEXTDSGN_CU entity = new TWMIFEXTDSGN_CU();

            entity.CD_SEQ_EXTDSGN = GetIfSequenceNo();
            entity.TS_EXTDSGN = key.TsExtDsgn;
            entity.ID_OPER = key.IdOper;
            entity.NO_POINT = ifcu.PointNumber;
            entity.NO_POINT_SPAN = ifcu.PointSpanNumber;
            entity.CD_DIST = ifcu.District;
            entity.CD_WR = ifcu.WorkRequest;
            entity.CD_BDGT_ITEM = "";
            entity.CD_CREW = ifcu.CrewCode;
            entity.CD_CU = ifcu.CUCode;
            entity.CD_MU = ifcu.MUCode;
            entity.CD_USAGE = ifcu.UsageCode;
            entity.CD_WORKPACKET = ifcu.Workpacket;
            entity.DT_COMPLETE = ifcu.CompletionDate;
            entity.IND_ACCT = ifcu.AccountIndicator;
            entity.IND_ON_OFF = ifcu.OnOffIndicator;
            entity.IND_PROCESS = ifcu.ProcessIndicator;
            entity.QT_ACTION = ifcu.ActionQuantity;
            // entity.CD_SEQ_EXTDSGN = GetIfSequenceNo();
            entity.CD_SEQ_EXTDSGN = ifcu.ExternalDesignSequence;
            entity.ID_OPER = ifcu.OperatorId;
            entity.NO_POINT = ifcu.PointNumber;
            entity.NO_POINT_SPAN = ifcu.PointSpanNumber;
            entity.TS_EXTDSGN = ifcu.ExternalDesignTimeStamp;
            entity.CD_SEQ_ERROR_RUN = ifcu.ErrorRunSequence;
            entity.FG_ERROR = ifcu.ErrorFlag;
            entity.FG_CNTCR = ifcu.ContractorFlag;
            entity.CD_SUPPLY_METHOD = ifcu.SupplyMethodCode;
            entity.IND_ACTION = ifcu.ActionIndicator;
            entity.NO_EQUIP = ifcu.EquipmentNumber;
            entity.NO_FACILITY = ifcu.FacilityNumber;
            //  FC_DIFFICULTY = obj
            //   CD_BDGT_ITEM = obj.
            //  CD_SEQ_ERROR_RUN = GetIfSequenceNo()

            return entity;
        }

        public IEnumerable<TWMIFEXTDSGN_CU> MapObjectsToCuIfEntities(List<IFExtDsgnCU> objs, ExtDesignKey key)
        {
            List<TWMIFEXTDSGN_CU> entities = new List<TWMIFEXTDSGN_CU>();

            foreach (var item in objs)
            {
                entities.Add(MapObjectToCuIfEntity(item, key));
            }

            return entities;
        }


        //public TWMIFEXTDSGN_CU MapObjectToIfEntityForDelete(IFExtDsgnCU obj)
        //{
        //    if (obj != null)
        //    {
        //        return new TWMIFEXTDSGN_CU
        //        {
        //            CD_DIST = obj.District,
        //            CD_WR = obj.WorkRequest,
        //            CD_CREW = obj.CrewCode,
        //            CD_CU = obj.CUCode,
        //            CD_MU = obj.MUCode,
        //            CD_USAGE = obj.UsageCode,
        //            CD_WORKPACKET = obj.Workpacket,
        //            DT_COMPLETE = obj.CompletionDate,
        //            IND_ACCT = obj.AccountIndicator,
        //            IND_ON_OFF = obj.OnOffIndicator,
        //            IND_PROCESS = obj.ProcessIndicator,
        //            QT_ACTION = obj.ActionQuantity,
        //            // CD_SEQ_EXTDSGN = GetIfSequenceNo(),
        //            CD_SEQ_EXTDSGN = obj.ExternalDesignSequence,
        //            ID_OPER = obj.OperatorId,
        //            NO_POINT = obj.PointNumber,
        //            NO_POINT_SPAN = obj.PointSpanNumber,
        //            TS_EXTDSGN = obj.ExternalDesignTimeStamp,
        //            CD_SEQ_ERROR_RUN = obj.ErrorRunSequence,
        //            FG_ERROR = obj.ErrorFlag,
        //            FG_CNTCR = obj.ContractorFlag,
        //            CD_SUPPLY_METHOD = obj.SupplyMethodCode,
        //            IND_ACTION = obj.ActionIndicator,
        //            NO_EQUIP = obj.EquipmentNumber,
        //            NO_FACILITY = obj.FacilityNumber,
                     
        //            //  FC_DIFFICULTY = obj
        //            //   CD_BDGT_ITEM = obj.
        //            //  CD_SEQ_ERROR_RUN = GetIfSequenceNo()
        //        };

        //    }
        //    return null;
        //}

        //---------------------------------------------    
        public TWMIFEXTDSGN_CU MapObjectToEntity(IFExtDsgnCU obj)
        {
            if (obj != null)
            {
                return new TWMIFEXTDSGN_CU
                {
                    CD_DIST = obj.District,
                    CD_WR = obj.WorkRequest,
                    CD_CREW = obj.CrewCode,
                    CD_CU = obj.CUCode,
                    CD_MU = obj.MUCode,
                    CD_USAGE = obj.UsageCode,
                    CD_WORKPACKET = obj.Workpacket,
                    DT_COMPLETE = obj.CompletionDate,
                    IND_ACCT = obj.AccountIndicator,
                    IND_ON_OFF = obj.OnOffIndicator,
                    IND_PROCESS = obj.ProcessIndicator,
                    QT_ACTION = obj.ActionQuantity,
                  //  CD_SEQ_EXTDSGN = GetIfSequenceNo(),
                    CD_SEQ_EXTDSGN = obj.ExternalDesignSequence,
                    ID_OPER = obj.OperatorId,
                    NO_POINT = obj.PointNumber.Trim(),
                    NO_POINT_SPAN = obj.PointSpanNumber.Trim(),
                    TS_EXTDSGN = obj.ExternalDesignTimeStamp,
                    CD_SEQ_ERROR_RUN = obj.ErrorRunSequence,
                    FG_ERROR = obj.ErrorFlag,
                    FG_CNTCR = obj.ContractorFlag,
                    CD_SUPPLY_METHOD = obj.SupplyMethodCode,
                    IND_ACTION = obj.ActionIndicator,
                    NO_EQUIP = obj.EquipmentNumber,
                    NO_FACILITY = obj.FacilityNumber
                    //  FC_DIFFICULTY = obj
                    //   CD_BDGT_ITEM = obj.
                    //  CD_SEQ_ERROR_RUN = GetIfSequenceNo()
                };
            }
            return null;
        }

        public IFExtDsgnCU MapEntityToObject(TWMIFEXTDSGN_CU obj)
        {
            if (obj != null)
            {
                return new IFExtDsgnCU
                {
                    District = obj.CD_DIST,
                    WorkRequest = (long)obj.CD_WR,
                    CrewCode = obj.CD_CREW,
                    ErrorRunSequence = obj.CD_SEQ_ERROR_RUN,
                    AccountIndicator = obj.IND_ACCT,
                    ActionQuantity = obj.QT_ACTION,
                    CompletionDate = obj.DT_COMPLETE,
                    CUCode = obj.CD_CU,
                    MUCode = obj.CD_MU,
                    OnOffIndicator = obj.IND_ON_OFF,
                    UsageCode = obj.CD_USAGE,
                    ProcessIndicator = obj.IND_PROCESS,
                    Workpacket = obj.CD_WORKPACKET,
                    ExternalDesignSequence = obj.CD_SEQ_EXTDSGN,
                    ExternalDesignTimeStamp = obj.TS_EXTDSGN,
                    OperatorId = obj.ID_OPER,
                    PointSpanNumber = obj.NO_POINT_SPAN,
                    PointNumber = obj.NO_POINT,
                    ErrorFlag = obj.FG_ERROR,
                    ContractorFlag = obj.FG_CNTCR,
                    EquipmentNumber = obj.NO_EQUIP,
                    ActionIndicator = obj.IND_ACTION,
                    SupplyMethodCode = obj.CD_SUPPLY_METHOD,
                    FacilityNumber = obj.NO_FACILITY

                };
            }
            return null;
        }

        private long GetIfSequenceNo()
        {
            return Convert.ToInt64(unitOfWork.GenericSqlRepo.RunRawSql<Decimal>(" select WE_S_SO_EXTDSG.nextval FROM dual").ToList()[0]);
        }

        private long GetIfSequenceNo2()
        {
            return Convert.ToInt64(unitOfWork.GenericSqlRepo.RunRawSql<Decimal>(" select WE_S_SO_EXTDSG_FACATT.nextval FROM dual").ToList()[0]);
        }
    }
}


