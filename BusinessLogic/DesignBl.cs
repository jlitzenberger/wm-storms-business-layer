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
    public class DesignBl : BaseBl
    {
        public Design GetByEntity(TWMDESIGN entity)
        {
            return MapEntityToObject(entity);
        }

        public List<Design> GetByEntities(IEnumerable<TWMDESIGN> entities)
        {
            if (entities != null && entities.Count() > 0)
            {
                return entities.Select(m => MapEntityToObject(m)).ToList();
            }

            return null;
        }

        public Design GetByWorkRequestIdDesignId(long workRequestId, int designId)
        {
            Design obj = GetByEntity(unitOfWork.DesignRepo.GetSingle(m => m.CD_WR == workRequestId && m.NO_DESIGN == designId));

            return obj;
        }

        public List<Design> GetByWorkRequestId(long workRequestId)
        {
            return GetByEntities(unitOfWork.DesignRepo.Get(m => m.CD_WR == workRequestId));
        }

        public WMException CreateExternalDesign(Design design)
        {
            WMException WMexcept = null;

            ///////////////////
            ////get the next Oracle WE_S_SO_EXTDSG
            ExtDesignKey key = new ExtDesignKey() { TsExtDsgn = DateTime.Now, IdOper = design.IDOper };

            // TODO: Add an insert to the design table
            //   twmDesignRepository.Insert(null);

            ///////////////////
            ////insert Points//
            new PointBl().CreateExternalDesign(design, design.Points, key);

            ///////////////////////
            ////insert CUs /FAC's//
            CreateCUsFacilityAttributes(design, key);

            ///////////////////
            ////call P Driver//
            WMexcept = ExecutePDriver(design, key);

            return WMexcept;
        }

        private void CreateCUsFacilityAttributes(Design design, ExtDesignKey key)
        {
            foreach (Point p in design.Points)
            {
                new CUBl().CreateExternalDesign(design.District, p.CUs, key);
            }
        }

        private WMException ExecutePDriver(Design design, ExtDesignKey key)
        {
            WMException wme = null;

            DesignError error = unitOfWork.DesignRepo.Insert(MapObjectToEntity(design), key.TsExtDsgn, key.IdOper);

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



            //TODO: create the multispeak library and put all of this there
            if (wme == null)
            {
                //////if success set the 211 to Complete
                ////new MilestoneRequirementBl(dbManagerBase.iOrigin, dbManagerBase.iEnvironment).SetRequirementStatus("211", design.WorkRequestID, "C");
                //////set the 491 Required
                ////new MilestoneRequirementBl(dbManagerBase.iOrigin, dbManagerBase.iEnvironment).SetRequirementStatus("491", design.WorkRequestID, "R");

                //TODO: create the multispeak library
                //get Approval Queue
                //WorkRequest wr = new WorkRequestBl(dbManagerBase.iOrigin, dbManagerBase.iEnvironment).GetWorkRequest(Int32.Parse(design.WorkRequestID));
                //MultiSpeakType mst = new MultiSpeakTypeDal().GetMultiSpeakTypeByWorkRequest(wr, Active.Yes);

                //TODO: create the multispeak library
                //set 491 Responsible to the correct Approval Queue
                //new MilestoneRequirementBl(dbManagerBase.iOrigin, dbManagerBase.iEnvironmentType).SetRequirementOperator("491", design.WorkRequestID.ToString(), mst.StormsApprovalQueue, "XXGEO_DO");
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

        public Design MapEntityToObject(TWMDESIGN entity)
        {
            if (entity != null)
            {
                Design obj = new Design();

                obj.District = entity.CD_DIST;
                obj.WorkRequestID = entity.CD_WR;
                obj.DesignNumber = entity.NO_DESIGN.ToString();
                obj.DesignDescription = entity.DS_DESIGN;
                obj.FlagPreferred = entity.FG_PREFERRED == "Y" ? true : false;
                obj.FlagComplete = entity.FG_COMPLETE == "Y" ? true : false;
                obj.FlagExtDesign = entity.FG_EXTDSGN == "Y" ? true : false;

                obj.Points = null;
                if (entity.TWMPOINT_ESTs != null && entity.TWMPOINT_ESTs.Count() > 0)
                {
                    obj.Points = new PointBl().Get(entity.TWMPOINT_ESTs);
                }

                return obj;
            }

            return null;
        }

        public TWMDESIGN MapObjectToEntity(Design obj)
        {
            if (obj != null)
            {
                TWMDESIGN entity = new TWMDESIGN();

                entity.CD_DIST = obj.District;
                entity.CD_WR = obj.WorkRequestID;
                //entity.NO_DESIGN = (short)int.Parse(obj.DesignNumber);
                entity.NO_DESIGN = 1;
                entity.DS_DESIGN = obj.DesignDescription;
                entity.FG_PREFERRED = obj.FlagPreferred == true ? "Y" : "N";
                entity.FG_COMPLETE = obj.FlagComplete == true ? "Y" : "N";
                entity.FG_EXTDSGN = obj.FlagExtDesign == true ? "Y" : "N";

                entity.TWMPOINT_ESTs = new PointBl().MapObjectsToEntities(obj.Points).ToList();

                return entity;
            }

            return null;
        }
    }
}
