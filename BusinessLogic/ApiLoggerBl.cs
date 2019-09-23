using WM.STORMS.BusinessLayer.Models;
using WM.STORMS.DataAccessLayer;
using WM.Common;

namespace WM.STORMS.BusinessLayer.BusinessLogic
{
    public class ApiLoggerBl : BaseBl
    {
        public ApiLogger MapEntityToModel(WE_T_API_LOG entity)
        {
            if (entity != null)
            {
                return new ApiLogger
                {
                    Id = entity.ID,
                    Application = entity.APPLICATION,
                    User = entity.REQUEST_USER,
                    Machine = entity.MACHINE,
                    RequestIpAddress = entity.REQUEST_IP_ADDRESS,
                    RequestContentType = entity.REQUEST_CONTENT_TYPE,
                    RequestContentBody = entity.REQUEST_CONTENT_BODY,
                    RequestUri = entity.REQUEST_URI,
                    RequestMethod = entity.REQUEST_METHOD,
                    RequestRouteTemplate = entity.REQUEST_ROUTE_TEMPLATE,
                    RequestRouteData = entity.REQUEST_ROUTE_DATA,
                    RequestHeaders = entity.REQUEST_HEADERS,
                    RequestTimestamp = entity.REQUEST_DATETIME,
                    ResponseContentType = entity.RESPONSE_CONTENT_TYPE,
                    ResponseContentBody = entity.RESPONSE_CONTENT_BODY,
                    ResponseStatusCode = entity.RESPONSE_STATUS_CODE,
                    ResponseHeaders = entity.RESPONSE_HEADERS,
                    ResponseTimestamp = entity.RESPONSE_DATETIME,
                    ExceptionId = entity.API_EXCEPTION_ID,
                    CorrelationId = entity.CORRELATION_ID,
                    FieldJobId = entity.FIELDJOB_ID,
                    WorkPacketId = entity.WORKPACKET_ID,
                    WorkRequestId = entity.WORKREQUEST_ID
                };
            }

            return null;
        }

        public WE_T_API_LOG MapModelToEntity(ApiLogger obj)
        {
            if (obj != null)
            {
                return new WE_T_API_LOG
                {
                    ID = obj.Id,
                    APPLICATION = obj.Application,
                    REQUEST_USER = obj.User,
                    MACHINE = obj.Machine,
                    REQUEST_IP_ADDRESS = obj.RequestIpAddress,
                    REQUEST_CONTENT_TYPE = obj.RequestContentType,
                    REQUEST_CONTENT_BODY = obj.RequestContentBody,
                    REQUEST_URI = obj.RequestUri,
                    REQUEST_METHOD = obj.RequestMethod,
                    REQUEST_ROUTE_TEMPLATE = obj.RequestRouteTemplate,
                    REQUEST_ROUTE_DATA = obj.RequestRouteData,
                    REQUEST_HEADERS = obj.RequestHeaders,
                    REQUEST_DATETIME = obj.RequestTimestamp,
                    RESPONSE_CONTENT_TYPE = obj.ResponseContentType,
                    RESPONSE_CONTENT_BODY = obj.ResponseContentBody,
                    RESPONSE_STATUS_CODE = obj.ResponseStatusCode,
                    RESPONSE_HEADERS = obj.ResponseHeaders,
                    RESPONSE_DATETIME = obj.ResponseTimestamp,
                    API_EXCEPTION_ID = obj.ExceptionId,
                    CORRELATION_ID = obj.CorrelationId,
                    FIELDJOB_ID = obj.FieldJobId,
                    WORKPACKET_ID = obj.WorkPacketId,
                    WORKREQUEST_ID = obj.WorkRequestId
                };
            }

            return null;
        }

        public long InsertLog(ApiLogger app)
        {
            //try {
            var newexception = MapModelToEntity(app);
            unitOfWork.ApiLogRepo.Insert(newexception);
            unitOfWork.Save();

            return newexception.ID;
            //} catch (System.Data.Entity.Validation.DbEntityValidationException e) {
            //    foreach (var eve in e.EntityValidationErrors) {
            //        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
            //        foreach (var ve in eve.ValidationErrors) {
            //            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
            //                ve.PropertyName, ve.ErrorMessage);
            //        }
            //    }
            //    throw;
            //}
        }

        public ApiLogger GetById(long id)
        {
            var apiLog = unitOfWork.ApiLogRepo.GetSingle(m => m.ID == id);

            return MapEntityToModel(apiLog);
        }

        public void UpdateLog(ApiLogger app)
        {
            var apiLog = unitOfWork.ApiLogRepo.GetSingle(m => m.ID == app.Id);
            if (apiLog != null)
            {
                apiLog.APPLICATION = app.Application.Truncate(50);
                apiLog.REQUEST_USER = app.User.Truncate(50);
                apiLog.MACHINE = app.Machine.Truncate(50);
                apiLog.REQUEST_IP_ADDRESS = app.RequestIpAddress.Truncate(50);
                apiLog.REQUEST_CONTENT_TYPE = app.RequestContentType.Truncate(250);
                apiLog.REQUEST_CONTENT_BODY = app.RequestContentBody;
                apiLog.REQUEST_URI = app.RequestUri.Truncate(500);
                apiLog.REQUEST_METHOD = app.RequestMethod.Truncate(20);
                apiLog.REQUEST_ROUTE_TEMPLATE = app.RequestRouteTemplate.Truncate(500);
                apiLog.REQUEST_ROUTE_DATA = app.RequestRouteData.Truncate(1000);
                apiLog.REQUEST_HEADERS = app.RequestHeaders.Truncate(1000);
                apiLog.REQUEST_DATETIME = app.RequestTimestamp;
                apiLog.RESPONSE_CONTENT_TYPE = app.ResponseContentType.Truncate(250);
                apiLog.RESPONSE_CONTENT_BODY = app.ResponseContentBody;
                apiLog.RESPONSE_STATUS_CODE = app.ResponseStatusCode;
                apiLog.RESPONSE_HEADERS = app.ResponseHeaders.Truncate(1000);
                apiLog.RESPONSE_DATETIME = app.ResponseTimestamp;
                apiLog.API_EXCEPTION_ID = app.ExceptionId;
                apiLog.CORRELATION_ID = app.CorrelationId;
                apiLog.FIELDJOB_ID = app.FieldJobId;
                apiLog.WORKPACKET_ID = app.WorkPacketId;
                apiLog.WORKREQUEST_ID = app.WorkRequestId;

                unitOfWork.ApiLogRepo.Update(apiLog);
                unitOfWork.Save();
            }
        }
    }
}