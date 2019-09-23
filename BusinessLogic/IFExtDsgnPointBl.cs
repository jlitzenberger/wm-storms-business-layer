using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WM.STORMS.BusinessLayer.Models;
using WM.STORMS.DataAccessLayer;
using WM.Common;
using WM.STORMS.DataAccessLayer.Repositories;
using WM.Common.Interfaces;
using System.Data.Entity.Core.Objects;

namespace WM.STORMS.BusinessLayer.BusinessLogic
{
    public class IFExtDsgnPointBl : BaseBl
    {
        public void Create(IFExtDsgnPoint obj)
        {
            unitOfWork.IfExtDesignPtRepo.Insert(MapObjectToIfEntity(obj));
            unitOfWork.Save();
        }

        public IFExtDsgnPoint GetByEntity(TWMIFEXTDSGN_PT entity)
        {
            return MapEntityToObject(entity);
        }    

        public List<IFExtDsgnPoint> GetAllByWorkRequest(long workRequest)
        {
            return unitOfWork.IfExtDsgnPtRepo.Get(m => m.CD_WR == workRequest).Select(m => MapEntityToObject(m)).ToList();

        }

        public IFExtDsgnPoint Get(long? workRequest, string pointId, string span)
        {
            return GetByEntity(unitOfWork.IfExtDesignPtRepo.GetSingle(m => m.CD_WR == workRequest && m.NO_POINT.Trim() == pointId.Trim() && m.NO_POINT_SPAN.Trim() == span.Trim()));
        }


        public void Delete(IFExtDsgnPoint obj)
        {
            if (obj != null)
            {
                
                TWMIFEXTDSGN_PT entity = MapObjectToIfEntity(obj);
                unitOfWork.IfExtDesignPtRepo.Delete(obj.ExternalDesignSequence, obj.ExternalDesignTimeStamp, obj.OperatorId);
                unitOfWork.Save();
            }
        }   

        public int CreateExternalDesignIf(IFExtDsgnPoint ifExtDsgnPt, ExtDesignKey key)
        {
          //  TWMIFEXTDSGN_PT entity = MapObjectToPtIfEntity(ifExtDsgnPt, key);
            TWMIFEXTDSGN_PT entity = MapObjectToIfEntity(ifExtDsgnPt);

            if (entity != null)
            {
                unitOfWork.IfExtDesignPtRepo.Insert(entity);        
                unitOfWork.Save();
            }      

            return 0;
        }

        public WMException CreateExternalDesignFromIf(IFExtDsgnPoint twmifextdsgn_pt_obj, bool finished, DateTime timeofcreate, string oper, string asbOrEstInd)
        {
            WMException WMexcept = null;

            ///////////////////
            ////get the next Oracle WE_S_SO_EXTDSG
            ExtDesignKey key = new ExtDesignKey() { TsExtDsgn = timeofcreate, IdOper = oper };
      //      ExtDesignKey key = new ExtDesignKey() { TsExtDsgn = twmifextdsgn_pt_obj.ExternalDesignTimeStamp, IdOper = twmifextdsgn_pt_obj.OperatorId };
                      
            ///////////////////
            ////insert Points//
      //      new PointBl().CreateExternalDesign(design, design.Points, key);

            ///////////////////////
            ////insert CUs /FAC's//
       //     CreateCUsFacilityAttributes(design, key);

            CreateExternalDesignIf(twmifextdsgn_pt_obj, key);

            ///////////////////
            ////call P Driver//            
      //      WMexcept = ExecutePDriver(twmifextdsgn_pt_obj, key);
            

            return WMexcept;
        }

        public WMException CallExternalDesign(IFExtDsgnPoint twmifextdsgn_pt_obj, bool finished, DateTime timeofcreate, string oper, string asbOrEstInd)
        {
            WMException WMexcept = null;
            ExtDesignKey key = new ExtDesignKey() { TsExtDsgn = timeofcreate, IdOper = oper };

            WMexcept = ExecutePDriver(twmifextdsgn_pt_obj, key, asbOrEstInd);


            return WMexcept;
        }



        private WMException ExecutePDriver(IFExtDsgnPoint ifpoint, ExtDesignKey key, string asbOrEstInd)
        {
            WMException wme = null;

            DesignIfError error = unitOfWork.IfExtDsgnPtRepo.Insert(MapObjectToIfEntity(ifpoint), asbOrEstInd);        

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

        private TWMIFEXTDSGN_PT MapObjectToPtIfEntity(IFExtDsgnPoint ifpoint, ExtDesignKey key)
        {
            TWMIFEXTDSGN_PT entity = new TWMIFEXTDSGN_PT();

            entity.CD_SEQ_EXTDSGN = GetIfSequenceNo();
            entity.TS_EXTDSGN = key.TsExtDsgn;
            entity.ID_OPER = key.IdOper;
            entity.NO_POINT = ifpoint.PointNumber;
            entity.NO_POINT_SPAN = ifpoint.PointSpanNumber;
            entity.CD_DIST = ifpoint.District;
            entity.CD_WR = ifpoint.WorkRequest;
            entity.AD_GR_1 = string.Empty;
            entity.AD_GR_2 = string.Empty;
            entity.TXT_DESN = string.Empty;
            entity.DT_RPTD = null;
            entity.DT_IN_SERVICE = null;
            entity.CD_TOWN_RANGE_SECT = string.Empty;
            entity.CD_TAX_DIST = string.Empty;
            entity.CD_SIDE_OF_STREET = string.Empty;
            entity.IND_WORK_STATUS = " ";
            entity.IND_MAIN_STATUS = "N";   //N = not designated, L = long, S = short
            entity.IND_PROCESS = "A";       //A = add, M = modify, D = delete
            entity.CD_ENTITY = string.Empty;
            entity.CD_ISOLATION_SECT = string.Empty;
            entity.CD_LANDMARK = string.Empty;
            entity.CD_POLITICAL_SUB = string.Empty;
            entity.CD_SCHOOL_TAX = string.Empty;
            entity.AMT_FIXED_BID = null;
            entity.NO_DRAWING = string.Empty;
            if (ifpoint.SpanLength == null)
            {
                entity.LN_SPAN = (decimal?)0.00;
            }
            else
            {
                entity.LN_SPAN = (decimal?)Convert.ToDecimal(ifpoint.SpanLength);
            }
            entity.NO_MAP = string.Empty;
            entity.AD_POINT = string.Empty;
            entity.ID_POINT = string.IsNullOrEmpty(ifpoint.PointId) ? string.Empty : ifpoint.PointId;
            entity.CD_CREW = string.Empty;
            entity.IND_CNTCR_CALC_MTH = "1";   //1, 2, 3, 4
            entity.FG_ERROR = "N";
            entity.CD_BID_ITEM = string.Empty;
            entity.FG_RWORKS = "N";   //"N" or "Y", default is "N"
            entity.CD_SEQ_ERROR_RUN = null;
            entity.NO_COMPLEXITY = null;
            entity.QT_BID_ITEM = 0;

            return entity;
        }

        public IEnumerable<TWMIFEXTDSGN_PT> MapObjectsToPtIfEntities(List<IFExtDsgnPoint> objs, ExtDesignKey key)
        {
            List<TWMIFEXTDSGN_PT> entities = new List<TWMIFEXTDSGN_PT>();

            foreach (var item in objs)
            {
                entities.Add(MapObjectToPtIfEntity(item, key));
            }

            return entities;
        }


        public TWMIFEXTDSGN_PT MapObjectToIfEntity(IFExtDsgnPoint obj)
        {
            if (obj != null)
            {
                if (obj.SpanLength == null)
                {
                    obj.SpanLength = (decimal?)0.00;
                }
                long EDS = 0;
                if (obj.ExternalDesignSequence == 0)
                {
                    EDS = GetIfSequenceNo();
                }

                return new TWMIFEXTDSGN_PT
                {
                    CD_DIST = obj.District,
                    CD_WR = obj.WorkRequest,
                    CD_CREW = obj.CrewCode,
                    CD_ENTITY = obj.EntityCode,
                    CD_SEQ_EXTDSGN = EDS,
                    ID_OPER = obj.OperatorId,
                    ID_POINT = obj.PointId,

                    LN_SPAN = obj.SpanLength,
                    NO_POINT = obj.PointNumber,
                    NO_POINT_SPAN = obj.PointSpanNumber,
                    TS_EXTDSGN = obj.ExternalDesignTimeStamp,
                    CD_SEQ_ERROR_RUN = obj.ErrorRunSequence,
                    FG_ERROR = obj.ErrorFlag,
                    CD_BID_ITEM = obj.BidItemCode,
                    IND_CNTCR_CALC_MTH = obj.ContractorCalcInd,
                    FG_RWORKS = obj.RWorksFlag,
                    IND_MAIN_STATUS = obj.MainStatusIndicator,
                    IND_PROCESS = obj.ProcessIndicator,
                    IND_WORK_STATUS = obj.WorkStatusIndicator

                };

            }
            return null;
        }

        public IFExtDsgnPoint MapEntityToObject(TWMIFEXTDSGN_PT obj)
        {
            if (obj != null)
            {
                return new IFExtDsgnPoint
                {
                    District = obj.CD_DIST,
                    WorkRequest = obj.CD_WR,
                    CrewCode = obj.CD_CREW,
                    ErrorRunSequence = obj.CD_SEQ_ERROR_RUN,
                    EntityCode = obj.CD_ENTITY,
                    ExternalDesignSequence = obj.CD_SEQ_EXTDSGN,
                    ExternalDesignTimeStamp = obj.TS_EXTDSGN,
                    OperatorId = obj.ID_OPER,
                    PointId = obj.ID_POINT,
                    PointSpanNumber = obj.NO_POINT_SPAN,
                    PointNumber = obj.NO_POINT,
                    SpanLength = obj.LN_SPAN,
                    ErrorFlag = obj.FG_ERROR,
                    BidItemCode = Convert.ToString(obj.AMT_FIXED_BID),
                    ContractorCalcInd = obj.IND_CNTCR_CALC_MTH,
                    RWorksFlag = obj.FG_RWORKS,
                    MainStatusIndicator = obj.IND_MAIN_STATUS,
                    PointAd = obj.AD_POINT,
                    ProcessIndicator = obj.IND_PROCESS,
                    WorkStatusIndicator = obj.IND_WORK_STATUS
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


