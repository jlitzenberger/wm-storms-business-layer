using System;
using System.Collections.Generic;
//using System.Data.EntityClient;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WM.STORMS.BusinessLayer.BusinessLogic;
using WM.Common.Interfaces;
using WM.STORMS.BusinessLayer.Models;
using WM.STORMS.DataAccessLayer;
using System.Globalization;

namespace WM.STORMS.BusinessLayer.BusinessLogic
{
    public class IFErrorBl : BaseBl
    {
        public List<IFError> GetByEntities(IEnumerable<TWMIFERROR> entities)
        {
            if (entities != null && entities.Count() > 0)
            {
                return entities.Select(m => MapEntityToObject(m)).ToList();
            }

            return null;
        }
     
        public List<IFError> GetByWorkRequestId(long WorkRequestId)
        {
            return GetByEntities(unitOfWork.IfErrorRepo.Get(m => m.CD_WR == WorkRequestId));
        }     

        public void Create(IFError ifError)
        {
            CreateIfError(MapObjectToEntity(ifError));
        }

        private void CreateIfError(TWMIFERROR entity)
        {
            unitOfWork.IfErrorRepo.Insert(entity);
            unitOfWork.Save();
        }

        public IFError GetByEntity(TWMIFERROR entity)
        {
            return MapEntityToObject(entity);
        }

        public List<IFError> GetAllByWorkRequest(long workRequest)
        {
            return unitOfWork.IfErrorRepo.Get(m => m.CD_WR == workRequest).Select(m => MapEntityToObject(m)).ToList();

        }

        public IFError Get(long sequenceError, string name, long sequenceErrorRun)
        {
            return GetByEntity(unitOfWork.IfErrorRepo.GetSingle(m => m.CD_SEQ_ERROR == sequenceError && m.NM_INTERFACE == name && m.CD_SEQ_ERROR_RUN == sequenceErrorRun));
        }


        public void Delete(IFError obj)
        {
            if (obj != null)
            {
                TWMIFERROR entity = MapObjectToEntity(obj);
                unitOfWork.IfErrorRepo.Delete(obj.CD_SEQ_ERROR, obj.NM_INTERFACE, obj.CD_SEQ_ERROR_RUN, obj.TS_ERROR);
                unitOfWork.Save();
            }
        }  

        public void Insert(IFError obj)
        {
            unitOfWork.IfErrorRepo.Insert(MapObjectToEntity(obj));
            unitOfWork.Save();
        }

        public void Insert(List<IFError> objs)
        {
            foreach (var item in objs)
            {
                Insert(item);
            }
        }

        public IFError MapEntityToObject(TWMIFERROR entity)
        {
            if (entity != null)
            {
                IFError obj = new IFError();
            
                obj.CD_WR = entity.CD_WR;
                obj.CD_PROJECT = entity.CD_PROJECT;
                obj.CD_SEQ_ERROR = entity.CD_SEQ_ERROR;
                obj.CD_SEQ_ERROR_RUN = entity.CD_SEQ_ERROR_RUN;
                obj.CD_WORKPACKET = entity.CD_WORKPACKET;
                obj.CD_WR = entity.CD_WR;
                obj.FG_DATA_ERROR = entity.FG_DATA_ERROR;
                obj.ID_EMPL = entity.ID_EMPL;
                obj.NM_COLUMN = entity.NM_COLUMN;
                obj.NM_INTERFACE = entity.NM_INTERFACE;
                obj.NM_TABLE = entity.NM_TABLE;
                obj.TS_ERROR = entity.TS_ERROR;
                
             //   obj.TS_ERROR = entity.TS_ERROR.ToString("dd-MMM-yy HH:mm:ss");

                obj.TS_ERROR_LOGGED = entity.TS_ERROR_LOGGED;
                obj.TXT_IFERROR = entity.TXT_IFERROR;
                obj.CD_CREW = entity.CD_CREW;
                obj.CD_DIST = entity.CD_DIST;
                obj.CD_FLEET = entity.CD_FLEET;
                obj.CD_SQLCODE = entity.CD_SQLCODE;
                obj.TXT_SQLERRTEXT = entity.TXT_SQLERRTEXT;

                return obj;
            }
            return null;
        }

        public TWMIFERROR MapObjectToEntity(IFError obj)
        {
            if (obj != null)
            {
                TWMIFERROR entity = new TWMIFERROR();
          
                entity.CD_WR = obj.CD_WR;
                entity.CD_PROJECT = obj.CD_PROJECT;
                entity.CD_SEQ_ERROR = obj.CD_SEQ_ERROR;
                entity.CD_SEQ_ERROR_RUN = obj.CD_SEQ_ERROR_RUN;
                entity.CD_WORKPACKET = obj.CD_WORKPACKET;
                entity.CD_WR = obj.CD_WR;
                entity.FG_DATA_ERROR = obj.FG_DATA_ERROR;
                entity.ID_EMPL = obj.ID_EMPL;
                entity.NM_COLUMN = obj.NM_COLUMN;
                entity.NM_INTERFACE = obj.NM_INTERFACE;
                entity.NM_TABLE = obj.NM_TABLE;
                entity.TS_ERROR = obj.TS_ERROR;
              //  entity.TS_ERROR = DateTime.ParseExact(obj.TS_ERROR, "dd-MMM-yy HH:mm:ss", CultureInfo.InvariantCulture);
                entity.TS_ERROR_LOGGED = obj.TS_ERROR_LOGGED;
                entity.TXT_IFERROR = obj.TXT_IFERROR;
                entity.CD_CREW = obj.CD_CREW;
                entity.CD_DIST = obj.CD_DIST;
                entity.CD_FLEET = obj.CD_FLEET;
                entity.CD_SQLCODE = obj.CD_SQLCODE;
                entity.TXT_SQLERRTEXT = obj.TXT_SQLERRTEXT;

                return entity;
            }
            return null;
        }
    }
}

