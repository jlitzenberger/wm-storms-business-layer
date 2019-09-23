using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using WM.STORMS.BusinessLayer.Models;
using WM.STORMS.DataAccessLayer;

namespace WM.STORMS.BusinessLayer.BusinessLogic
{
    public class LeakWorkRequest
    {
        public Int64 CD_WR { get; set; }
        public Int64 cd_seq { get; set; }
    }

    public class WorkRequestBl : BaseBl
    {
        private AddressBl addressBl;
        public WorkRequestBl()            
        {
            addressBl = new AddressBl();
        }

        public WorkRequest GetByEntity(TWMWR entity)
        {
            return MapEntityToObject(entity);
        }

        public List<WorkRequest> GetByExternalJobNumber(string externalJobNumber)
        {
            var wrs = unitOfWork.WrRepo.Get(m => m.NO_EXT_JOB == externalJobNumber).ToList();
            if (wrs.Any())
            {
                return wrs.Select(MapEntityToObjectSlim).ToList();
            }

            return null;
        }

        public WorkRequest GetWorkRequest(long workRequestId)
        {
            return GetByEntity(unitOfWork.WrRepo.GetSingle(m => m.CD_WR == workRequestId));
        }

        public WorkRequest GetWorkRequestSlim(long workRequestId)
        {
            return MapEntityToObjectSlim(unitOfWork.WrRepo.GetSingle(m => m.CD_WR == workRequestId));
        }

        public WorkRequest CreateWorkRequest(WorkRequest obj)
        {
            WorkRequestKeyGenerator wrKey = new WorkRequestKeyGenerator();
            DateTime workRequestDateTimeStamp = DateTime.Now;
            int workRequestSeqNum = wrKey.NextValSeq;

            //or map model to entity  then pass workRequest entity to twmIfWorkRepository    to be mapped to TWMIFWORK
            unitOfWork.IfWorkRepo.Insert(MapObjectToIfEntity(workRequestDateTimeStamp, workRequestSeqNum, obj));
            unitOfWork.Save();
            addressBl.CreateAddress(workRequestDateTimeStamp, workRequestSeqNum, obj.Address);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ////Had to go outside of Entity Framework because the stored proc is compliled and would not import correctly ...........
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            using (OleDbConnection oConnection = new OleDbConnection(ConfigurationManager.AppSettings["StormsOleDb"]))
            {
                oConnection.Open();
                OleDbCommand oCommand = oConnection.CreateCommand();
                oCommand = new OleDbCommand("PKGSTORMSAUTOGENERATEWR.PCREATEWR", oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;

                var oraDateTime = workRequestDateTimeStamp.ToString("dd-MMM-yyyy HH:mm:ss");

                //Input Parameters
                oCommand.Parameters.Add("p_dt_timestamp", OleDbType.Date).Value = oraDateTime;
                oCommand.Parameters.Add("p_cd_seq", OleDbType.Decimal, 11).Value = workRequestSeqNum;

                //Output Parameters
                oCommand.Parameters.Add(new OleDbParameter("p_error", OleDbType.Char, 1)).Direction = ParameterDirection.InputOutput;
                oCommand.Parameters["p_error"].Value = "N";
                oCommand.Parameters.Add(new OleDbParameter("p_cd_wr", OleDbType.Decimal, 11)).Direction = ParameterDirection.InputOutput;
                oCommand.Parameters["p_cd_wr"].Value = 0;
                
                oCommand.ExecuteNonQuery();

                string errMessage = oCommand.Parameters["p_error"].Value.ToString();

                if (errMessage == "Y")
                {
                    //return 0;
                    //Throw Exception
                    throw new Exception("Error occurred creating work request");
                }
                else
                {
                    //return Convert.ToInt64(oCommand.Parameters["p_cd_wr"].Value.ToString());
                    return GetWorkRequest(Convert.ToInt64(oCommand.Parameters["p_cd_wr"].Value.ToString()));
                }
            }
        }

        public void Update(WorkRequest obj)
        {
            UpdateRoot(obj);

            if (obj.WorkType == "NOND")
            {
                CreateOrUpdateNonDesigned(obj);
            }
            //if (obj.ExtraDetails != null)
            //{
            //    new ExtraDetailsBl().Create(obj.ExtraDetails);
            //}
            //if (obj.Premises != null && obj.Premises.Count > 0)
            //{
            //    new PremiseBl().Update(obj.Premises);
            //}
            //if (obj.GasLoadDetails != null && obj.GasLoadDetails.Count > 0)
            //{
            //    //Use Create here because of "pivot" table
            //    new GasLoadDetailBl().Create(obj.GasLoadDetails);
            //}
            //if (obj.MilestoneRequirements != null && obj.MilestoneRequirements.Count > 0)
            //{
            //    new MilestoneRequirementBl().Update(obj.MilestoneRequirements);
            //}
            //if (obj.Specifications != null && obj.Specifications.Count > 0)
            //{
            //    foreach (var item in obj.Specifications)
            //    {
            //        new DesignBl().CreateExternalDesign(item);
            //    }
            //}
        }

        public void Cancel(long id)
        {
            var wr = unitOfWork.WrRepo.GetSingle(m => m.CD_WR == id);
            TWMIFWRCANCEL entity = new TWMIFWRCANCEL
            {
                CD_CANCELLATION = "CE",
                CD_DIST = wr.CD_DIST,
                CD_WR = id,
                FG_ERROR = "N",
                TS_WRCANCEL = DateTime.Now,
                ID_OPER = "XXSTORMS",
                CD_SEQ = id
            };
            unitOfWork.TwmIfWrCancel.Insert(entity);
            unitOfWork.Save();
        }

        private void UpdateRoot(WorkRequest obj)
        {
            var entity = unitOfWork.WrRepo.GetSingle(m => m.CD_WR == obj.WorkRequestId && m.CD_DIST == obj.District);
            entity = MapRootObjectToEntity(obj, entity);

            unitOfWork.WrRepo.Update(entity);
            unitOfWork.Save();
        }

        private void CreateOrUpdateNonDesigned(WorkRequest workRequest)
        {
            WorkRequestNonDesigned wrnd = new WorkRequestNonDesignedBl().GetNonDesigned(workRequest.WorkRequestId, workRequest.District);

            if (wrnd == null)
            {
                JobCode jc = new JobCodeBl().GetById(workRequest.JobCode);

                wrnd = new WorkRequestNonDesigned();

                wrnd.CD_WR = workRequest.WorkRequestId;
                wrnd.CD_DIST = workRequest.District;
                wrnd.DS_JOB = jc.DS_JOB;
                wrnd.CD_JOB = jc.CD_JOB;
                wrnd.FG_REINITIATED = "N";

                new WM.STORMS.BusinessLayer.BusinessLogic.WorkRequestNonDesignedBl().Create(wrnd);
            }
            else
            {
                wrnd.DS_JOB = workRequest.JobDescription;
                wrnd.CD_JOB = workRequest.JobCode;
                wrnd.FG_REINITIATED = "N";

                new WM.STORMS.BusinessLayer.BusinessLogic.WorkRequestNonDesignedBl().Update(wrnd);
            }
        }

        public List<WorkRequest> GetLeakWorkRequestsByRange(DateTime startDate, DateTime endDate)
        {
            List<WorkRequest> lstWorkRequests = new List<WorkRequest>();
            string sql = string.Empty;

            sql = " SELECT lg.CD_WR, lg.cd_seq " +
            " FROM WE_T_SF_LEAK_GENERAL lg" +
            " JOIN TWMMILESTONERQMT rq ON rq.CD_WR = lg.CD_WR" +
            " AND lg.IND_LEAK_FOUND = 'Yes'" +
            " AND (rq.TS_OPER_LAST_UPDTD IS NULL " +
            "  OR (rq.TS_OPER_LAST_UPDTD BETWEEN TO_DATE('" + startDate + "','MM/DD/YYYY HH:MI:SS AM') AND TO_DATE('" + endDate + "','MM/DD/YYYY HH:MI:SS AM') " +
            " AND  rq.CD_RQMT = 699))";

            List<LeakWorkRequest> lstLeakWorkRequests = unitOfWork.GenericSqlRepo.RunRawSql<LeakWorkRequest>(sql).ToList();

            foreach (var item in lstLeakWorkRequests)
            {
                lstWorkRequests.Add(GetWorkRequest(item.CD_WR));
            }

            return lstWorkRequests;

        }

        public WorkRequest GetWorkRequestByWorkPacket(long workpacketId)
        {
            WorkRequest obj = MapEntityToObject(unitOfWork.WrRepo.Get(m => m.TWMWORKPACKETs.Any(x => x.CD_WORKPACKET == workpacketId)).FirstOrDefault());

            return obj;
        }

        public WorkRequest MapEntityToObjectSlim(TWMWR entity)
        {
            if (entity != null)
            {
                WorkRequest obj = new WorkRequest();

                obj.WorkRequestId = entity.CD_WR;
                obj.District = entity.CD_DIST;

                obj.JobDescription = entity.DS_WR;
                obj.RequiredDate = entity.DT_REQUIRED;
                obj.CrewHeadquarter = entity.CD_CREWHQ;
                obj.AssignedTo = entity.ID_OPER_ASSIGNED;
                obj.WorkType = entity.TP_WR;
                obj.JobCode = entity.TWMWRNONDESIGNED != null ? entity.TWMWRNONDESIGNED.CD_JOB : string.Empty;
                obj.JobType = entity.TP_JOB;

                //obj.Latitude = entity.AD_GR_1; //Not needed here..in the Geo model XCoordinate
                //obj.Longitute = entity.AD_GR_2; //Not needed here..in the Geo model YCoordinate
                //obj.ReviewDate = entity.TWMMILESTONERQMTs.Where(m => m.CD_RQMT == 712 && m.ST_RQMT == "C").FirstOrDefault() != null ? entity.TWMMILESTONERQMTs.Where(m => m.CD_RQMT == 712 && m.ST_RQMT == "C").FirstOrDefault().TS_OPER_LAST_UPDTD.ToString() : string.Empty;
                //obj.SuprevisorID = entity.TWMMILESTONERQMTs.Where(m => m.CD_RQMT == 712).Count() > 0 ? entity.TWMMILESTONERQMTs.Where(m => m.CD_RQMT == 712).FirstOrDefault().ID_OPER_LAST_UPDTD != null ? entity.TWMMILESTONERQMTs.Where(m => m.CD_RQMT == 712).FirstOrDefault().ID_OPER_LAST_UPDTD.ToString(): string.Empty : string.Empty;
                //obj.WorkCompletedDate = entity.TWMMILESTONERQMTs.Where(m => m.CD_RQMT == 699 && m.ST_RQMT == "C").FirstOrDefault() != null ? entity.TWMMILESTONERQMTs.Where(m => m.CD_RQMT == 699 && m.ST_RQMT == "C").FirstOrDefault().TS_OPER_LAST_UPDTD.ToString() : string.Empty;
                //obj.WorkRequestClosedDate = entity.TWMMILESTONERQMTs.Where(m => m.CD_RQMT == 899 && m.ST_RQMT == "C").FirstOrDefault() != null ? entity.TWMMILESTONERQMTs.Where(m => m.CD_RQMT == 899 && m.ST_RQMT == "C").FirstOrDefault().TS_OPER_LAST_UPDTD.ToString() : string.Empty;

                //obj.WorkRequestStatus = entity.CD_STATUS;
                obj.Project = entity.CD_PROJECT;
                obj.ProjectName = entity.NM_PROJECT;
                obj.IOInstallation = entity.CD_WO_INSTL;
                obj.IORemoval = entity.CD_WO_REMOV;
                obj.IOTransfer = entity.CD_WO_TRNSF;
                obj.IORevenue = entity.CD_WO_REVENUE;
                obj.Priority = entity.CD_PRIORITY;
                obj.MeterNo = entity.NO_METER;
                //obj.WorkRequestRelatedTo = entity.CD_WR_RELATED;

                obj.ExternalJobNumber = entity.NO_EXT_JOB;
                obj.ExternalSystemId = entity.NO_EXT_SYS_ID;
                obj.HrEstTime = entity.HR_EST_TIME;

                obj.DateCanceled = entity.DT_CANCELLED;
                obj.Status = entity.CD_STATUS;
                obj.Canceled = entity.FG_CANCELED == "Y" ? true : false;
                obj.Resolution = entity.CD_RESOLUTION;

                obj.ContactName = entity.NM_CONTACT; // not the same as customer
                obj.ContactPhone = entity.TL_CONTACT;
                obj.MobilizedWr = entity.TXT_REFERENCE_5;

                //obj.Specifications = entity.TWMDESIGNs != null && entity.TWMDESIGNs.Count > 0 ? new DesignBl().GetByEntities(entity.TWMDESIGNs) : null;
                //AsBuilt = new DesignBl();
                //obj.ExtraDetails = entity.TWMBILLINGMOREs != null && entity.TWMBILLINGMOREs.Count > 0 ? new ExtraDetailsBl().GetByEntities(entity.TWMBILLINGMOREs) : null;
                obj.Address = entity.TWMADDRESS != null ? new AddressBl().Get(entity.TWMADDRESS) : null;
                obj.Geo = new GeoBl().Get(entity);
                //obj.Customers = entity.TWMCUSTOMERs != null && entity.TWMCUSTOMERs.Count > 0 ? new CustomerBl().GetByEntities(entity.TWMCUSTOMERs) : null;
                //obj.Contact = new Customer();
                //obj.AssociatedParties = entity.TWMASSOCPARTies != null && entity.TWMASSOCPARTies.Count > 0 ? new AssociatedPartyBl().GetByEntities(entity.TWMASSOCPARTies) : null;
                //obj.Remarks = entity.TWMREMARKs != null && entity.TWMREMARKs.Count > 0 ? new RemarkBl().GetByEntities(entity.TWMREMARKs) : null;
                //obj.FieldReports = new FieldReports();
                //obj.WorkPackets = entity.TWMWORKPACKETs != null && entity.TWMWORKPACKETs.Count > 0 ? new WorkPacketBl().Get(entity.TWMWORKPACKETs) : null;
                // TODO: problem with primary keys - fix causes slowness
                //    obj.Documents = entity.twmw new DocumentBl().GetDocumentsByWorkRequestId((long)entity.CD_WR);
                //obj.MilestoneRequirements = entity.TWMMILESTONERQMTs != null && entity.TWMMILESTONERQMTs.Count > 0 ? new MilestoneRequirementBl().GetByEntities(entity.TWMMILESTONERQMTs) : null;
                //obj.Premises = entity.TWMPREMISEs != null && entity.TWMPREMISEs.Count > 0 ? new PremiseBl().GetByEntities(entity.TWMPREMISEs) : null;
                //obj.GasLoadDetails = entity.TWMGASLOADDETs != null && entity.TWMGASLOADDETs.Count > 0 ? new GasLoadDetailBl().GetByEntities(entity.TWMGASLOADDETs) : null;
                //obj.PointAsbs = entity.TWMPOINT_ASBs != null && entity.TWMPOINT_ASBs.Count > 0 ? new PointASBBl().Get(entity.TWMPOINT_ASBs) : null;
                //obj.ExceptionConditions = entity.TWMIFEXCEPTIONCONDs != null && entity.TWMIFEXCEPTIONCONDs.Count > 0 ? new ExceptionConditionBl().GetByWorkRequestId((long)entity.CD_WR) : null;
                return obj;
            }

            return null;
        }

        public WorkRequest MapEntityToObject(TWMWR entity)
        {
            if (entity != null)
            {
                WorkRequest obj = new WorkRequest();

                obj.WorkRequestId = entity.CD_WR;
                obj.District = entity.CD_DIST;

                obj.JobDescription = entity.DS_WR;
                obj.RequiredDate = entity.DT_REQUIRED;
                obj.CrewHeadquarter = entity.CD_CREWHQ;
                obj.AssignedTo = entity.ID_OPER_ASSIGNED;
                obj.WorkType = entity.TP_WR;
                obj.JobCode = entity.TWMWRNONDESIGNED != null ? entity.TWMWRNONDESIGNED.CD_JOB : string.Empty;
                obj.JobType = entity.TP_JOB;

                //obj.Latitude = entity.AD_GR_1; //Not needed here..in the Geo model XCoordinate
                //obj.Longitute = entity.AD_GR_2; //Not needed here..in the Geo model YCoordinate
                //obj.ReviewDate = entity.TWMMILESTONERQMTs.Where(m => m.CD_RQMT == 712 && m.ST_RQMT == "C").FirstOrDefault() != null ? entity.TWMMILESTONERQMTs.Where(m => m.CD_RQMT == 712 && m.ST_RQMT == "C").FirstOrDefault().TS_OPER_LAST_UPDTD.ToString() : string.Empty;
                //obj.SuprevisorID = entity.TWMMILESTONERQMTs.Where(m => m.CD_RQMT == 712).Count() > 0 ? entity.TWMMILESTONERQMTs.Where(m => m.CD_RQMT == 712).FirstOrDefault().ID_OPER_LAST_UPDTD != null ? entity.TWMMILESTONERQMTs.Where(m => m.CD_RQMT == 712).FirstOrDefault().ID_OPER_LAST_UPDTD.ToString(): string.Empty : string.Empty;
                //obj.WorkCompletedDate = entity.TWMMILESTONERQMTs.Where(m => m.CD_RQMT == 699 && m.ST_RQMT == "C").FirstOrDefault() != null ? entity.TWMMILESTONERQMTs.Where(m => m.CD_RQMT == 699 && m.ST_RQMT == "C").FirstOrDefault().TS_OPER_LAST_UPDTD.ToString() : string.Empty;
                //obj.WorkRequestClosedDate = entity.TWMMILESTONERQMTs.Where(m => m.CD_RQMT == 899 && m.ST_RQMT == "C").FirstOrDefault() != null ? entity.TWMMILESTONERQMTs.Where(m => m.CD_RQMT == 899 && m.ST_RQMT == "C").FirstOrDefault().TS_OPER_LAST_UPDTD.ToString() : string.Empty;

                //obj.WorkRequestStatus = entity.CD_STATUS;
                obj.Project = entity.CD_PROJECT;
                obj.ProjectName = entity.NM_PROJECT;
                obj.IOInstallation = entity.CD_WO_INSTL;
                obj.IORemoval = entity.CD_WO_REMOV;
                obj.IOTransfer = entity.CD_WO_TRNSF;
                obj.IORevenue = entity.CD_WO_REVENUE;
                obj.Priority = entity.CD_PRIORITY;
                obj.MeterNo = entity.NO_METER;
                //obj.WorkRequestRelatedTo = entity.CD_WR_RELATED;

                obj.ExternalJobNumber = entity.NO_EXT_JOB;
                obj.ExternalSystemId = entity.NO_EXT_SYS_ID;
                obj.HrEstTime = entity.HR_EST_TIME;

                obj.DateCanceled = entity.DT_CANCELLED;
                obj.Status = entity.CD_STATUS;
                obj.Canceled = entity.FG_CANCELED == "Y" ? true : false;
                obj.Resolution = entity.CD_RESOLUTION;

                obj.ContactName = entity.NM_CONTACT; // not the same as customer
                obj.ContactPhone = entity.TL_CONTACT;

                obj.MobilizedWr = entity.TXT_REFERENCE_5;

                obj.Specifications = entity.TWMDESIGNs != null && entity.TWMDESIGNs.Count > 0 ? new DesignBl().GetByEntities(entity.TWMDESIGNs) : null;
                //AsBuilt = new DesignBl();
                obj.ExtraDetails = entity.TWMBILLINGMOREs != null && entity.TWMBILLINGMOREs.Count > 0 ? new ExtraDetailsBl().GetByEntities(entity.TWMBILLINGMOREs) : null;
                obj.Address = entity.TWMADDRESS != null ? new AddressBl().Get(entity.TWMADDRESS) : null;
                obj.Geo = new GeoBl().Get(entity);
                obj.Customers = entity.TWMCUSTOMERs != null && entity.TWMCUSTOMERs.Count > 0 ? new CustomerBl().GetByEntities(entity.TWMCUSTOMERs) : null;
                
                obj.AssociatedParties = entity.TWMASSOCPARTies != null && entity.TWMASSOCPARTies.Count > 0 ? new AssociatedPartyBl().GetByEntities(entity.TWMASSOCPARTies) : null;
                obj.Remarks = entity.TWMREMARKs != null && entity.TWMREMARKs.Count > 0 ? new RemarkBl().GetByEntities(entity.TWMREMARKs) : null;
                obj.FieldReports = new FieldReports();
                obj.WorkPackets = entity.TWMWORKPACKETs != null && entity.TWMWORKPACKETs.Count > 0 ? new WorkPacketBl().Get(entity.TWMWORKPACKETs) : null;
                // TODO: problem with primary keys - fix causes slowness
            //    obj.Documents = entity.twmw new DocumentBl().GetDocumentsByWorkRequestId((long)entity.CD_WR);
                obj.MilestoneRequirements = entity.TWMMILESTONERQMTs != null && entity.TWMMILESTONERQMTs.Count > 0 ? new MilestoneRequirementBl().GetByEntities(entity.TWMMILESTONERQMTs) : null;
                obj.Premises = entity.TWMPREMISEs != null && entity.TWMPREMISEs.Count > 0 ? new PremiseBl().GetByEntities(entity.TWMPREMISEs) : null;
                obj.GasLoadDetails = entity.TWMGASLOADDETs != null && entity.TWMGASLOADDETs.Count > 0 ? new GasLoadDetailBl().GetByEntities(entity.TWMGASLOADDETs) : null;
                obj.PointAsbs = entity.TWMPOINT_ASBs != null && entity.TWMPOINT_ASBs.Count > 0 ? new PointASBBl().Get(entity.TWMPOINT_ASBs) : null;
                obj.ExceptionConditions = entity.TWMIFEXCEPTIONCONDs != null && entity.TWMIFEXCEPTIONCONDs.Count > 0 ? new ExceptionConditionBl().GetByWorkRequestId((long)entity.CD_WR) : null;               
                return obj;
            }

            return null;
        }

        public TWMWR MapObjectToEntity(WorkRequest obj)
        {
            TWMWR entity = new TWMWR();

            entity = MapRootObjectToEntity(obj, entity);

            return entity;
        }

        public TWMWR MapRootObjectToEntity(WorkRequest obj, TWMWR entity)
        {
            entity.CD_WR = obj.WorkRequestId;
            entity.CD_DIST = obj.Geo.Dist;

            entity.DS_WR = obj.JobDescription;
            entity.DT_REQUIRED = obj.RequiredDate;
            entity.CD_CREWHQ = obj.CrewHeadquarter;
            //obj.AssignedTo = string.Empty;
            entity.TP_WR = obj.WorkType;
            //entity.TWMWRNONDESIGNED.CD_JOB = obj.JobCode;
            entity.TP_JOB = obj.JobType;
            entity.TXT_REFERENCE_5 = obj.MobilizedWr;

            ////////////entity.FG_CHARGEABLE = "Y";
            ////////////entity.IND_UTIL = "Y";
            ////////////entity.FG_JOB_CARD_PRINTED = "Y";
            ////////////entity.FG_ADD_ITEM = "Y";
            ////////////entity.FG_WO_COST = "Y";
            ////////////entity.FG_CANCELED = "Y";
            ////////////entity.FG_APPOINTMENT = "Y";
            ////////////entity.FG_AUTOC_FAILED = "Y";
            ////////////entity.CD_FORECAST = "Y";
            ////////////entity.FG_EXT_OWNER = "Y";
            ////////////entity.FG_READY_TO_ASSIGN = "Y";
            ////////////entity.FG_DT_REQ_COMMIT = "Y";

            //obj.WorkRequestStatus = entity.CD_STATUS;
            //obj.Project = entity.CD_PROJECT;
            //obj.ProjectName = entity.NM_PROJECT;
            //obj.IOInstallation = entity.CD_WO_INSTL;
            //obj.IORemoval = entity.CD_WO_REMOV;
            //obj.IOTransfer = entity.CD_WO_TRNSF;
            //obj.IORevenue = entity.CD_WO_REVENUE;
            //obj.Priority = string.Empty;
            //obj.MeterNo = entity.NO_METER;
            //obj.WorkRequestRelatedTo = 0;
            //obj.ExternalJobNumber = string.Empty;


            //obj.DateCanceled = entity.DT_CANCELLED;
            //obj.Status = entity.CD_STATUS;
            //obj.Canceled = entity.FG_CANCELED == "Y" ? true : false;
            //obj.District = null;



            //entity.TWMDESIGNs = new DesignBl(dbManagerBase.iOrigin, dbManagerBase.iEnvironment).MapObjectsToEntities(obj.Specifications).ToList();

            //AsBuilt = new DesignBl();
            //entity.TWMBILLINGMOREs = new ExtraDetailsBl(dbManagerBase.iOrigin, dbManagerBase.iEnvironment).MapObjectToEntity(obj.ExtraDetails).ToList();

            //obj.Address = new AddressBl(dbManagerBase.iOrigin, dbManagerBase.iEnvironment).GetAddress(entity.TWMADDRESS);
            //obj.Geo = new GeoBl(dbManagerBase.iOrigin, dbManagerBase.iEnvironment).GetGeo(entity);
            //obj.Customers = new CustomerBl(dbManagerBase.iOrigin, dbManagerBase.iEnvironment).GetCustomers(entity.TWMCUSTOMERs);
            //obj.Contact = new Customer();
            //obj.AssociatedParties = new AssociatedPartyBl(dbManagerBase.iOrigin, dbManagerBase.iEnvironment).GetAssociatedParties(entity.TWMASSOCPARTies);
            //obj.Remarks = new RemarkBl(dbManagerBase.iOrigin, dbManagerBase.iEnvironment).GetRemarks(entity.TWMREMARKs);
            //obj.FieldReports = new FieldReports();
            //obj.WorkPackets = new WorkPacketBl(dbManagerBase.iOrigin, dbManagerBase.iEnvironment).GetWorkPackets(entity.TWMWORKPACKETs);
            //// TODO: problem with primary keys - fix causes slowness
            //obj.Attachments = new AttachmentBl(dbManagerBase.iOrigin, dbManagerBase.iEnvironment).GetAttachmentsByWorkRequestId((int)entity.CD_WR);
            //obj.MilestoneRequirements = new MilestoneRequirementBl(dbManagerBase.iOrigin, dbManagerBase.iEnvironment).GetMilestoneRequirements(entity.TWMMILESTONERQMTs);

            //entity.TWMMILESTONERQMTs = new MilestoneRequirementBl(dbManagerBase.iOrigin, dbManagerBase.iEnvironment).MapObjectsToEntities(obj.MilestoneRequirements).ToList();

            //obj.Premises = new PremiseBl(dbManagerBase.iOrigin, dbManagerBase.iEnvironment).GetPremises(entity.TWMPREMISEs);
            //obj.GasLoadDetails = new GasLoadDetailBl(dbManagerBase.iOrigin, dbManagerBase.iEnvironment).GetGasLoadDets(entity.TWMGASLOADDETs);


            return entity;
        }

        private TWMIFWORK MapObjectToIfEntity(DateTime workRequestDateTimeStamp, int workRequestSeqNum, WorkRequest obj)
        {
            string FlagError = "N";
            string Process = "A";
            string Appointment = "N";
            string ExternalOwner = "N";
            string DsgnReplace = "N";
            string Direct = "Y";
            string ExtSystem = "WebSrvs";
            string WorkRequestNbr = string.Empty;

            TWMIFWORK entity = new TWMIFWORK();

            entity.TS_INTF_WORKFILE = workRequestDateTimeStamp;
            entity.CD_SEQ = workRequestSeqNum;
            entity.CD_AREA = obj.Geo.Area;
            entity.CD_CREWHQ = obj.CrewHeadquarter;
            entity.CD_DIST = obj.Geo.Dist;
            entity.CD_JOB = obj.WorkType == "NONC" || obj.WorkType == "DESG" ? "" : obj.JobCode; //Required
            entity.CD_STATE = obj.Address.State;
            entity.CD_ZONE = obj.Geo.Zone;
            entity.DT_REQUIRED = (DateTime)obj.RequiredDate;
            entity.FG_ERROR = FlagError;
            entity.ID_OPER_ASSIGNED = obj.AssignedTo;
            entity.IND_PROCESS = Process;
            entity.ID_EXT_SYSTEM = ExtSystem;
            entity.ID_CUSTOMER = obj.Customers != null && obj.Customers.Count > 0 ? "1" : "";
            entity.TP_WR = obj.WorkType;
            entity.TP_JOB = obj.JobType;
            entity.CD_ADDRESS_JOB = workRequestSeqNum;
            entity.FG_APPOINTMENT = Appointment;
            entity.FG_EXT_OWNER = ExternalOwner;
            entity.FG_DSGN_REPLACE = DsgnReplace;
            entity.FG_DIRECT = Direct;
            entity.TS_RECORDED_ON = null; // workRequestDateTimeStamp; //DateTime.Now;
            entity.CD_PRIORITY = obj.Priority;
            entity.NO_EXT_SYS_ID = "Entered By " + obj.ExternalSystemId;
            entity.NO_EXT_JOB = obj.ExternalJobNumber;
            entity.NO_METER = obj.MeterNo;

            entity.HR_EST_TIME = obj.HrEstTime;
            entity.CD_WO_REVENUE = obj.IORevenue;
            entity.CD_WO_TRNSF = obj.IOTransfer;
            entity.CD_WO_REMOV = obj.IORemoval;
            entity.CD_WO_INSTL = obj.IOInstallation;
            entity.CD_PROJECT = obj.Project;
            entity.NM_CONTACT = obj.ContactName;
            entity.TL_CONTACT = obj.ContactPhone;
            entity.AD_GR1 = obj.Geo.XCoordinate;
            entity.AD_GR2 = obj.Geo.YCoordinate;

            entity.TXT_REFERENCE_5 = obj.MobilizedWr;

            if (obj.WorkRequestRelatedTo == 0)
            {
                entity.CD_WR_RELATED = null;
                entity.CD_DIST_RELATED = null;
            }
            else
            {
                entity.CD_WR_RELATED = (long?)obj.WorkRequestRelatedTo;
                entity.CD_DIST_RELATED = GetWorkRequest(obj.WorkRequestRelatedTo).Geo.Dist; ;
            }

            return entity;
        }

        private long GetIfSequenceNo()
        {
            List<Decimal> a = unitOfWork.GenericSqlRepo.RunRawSql<Decimal>("select we_s_so_extdsg.nextval FROM dual").ToList();

            return Convert.ToInt64(a[0]);
        }
    }
}
