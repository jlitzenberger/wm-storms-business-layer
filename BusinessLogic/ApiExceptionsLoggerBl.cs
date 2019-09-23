using WM.STORMS.BusinessLayer.Models;
using WM.STORMS.DataAccessLayer;

namespace WM.STORMS.BusinessLayer.BusinessLogic
{
    public class ApiExceptionsLoggerBl : BaseBl
    {
        public ApiException MapEntityToModel(WE_T_API_EXCEPTIONS_LOG entity)
        {
            if (entity != null)
            {
                return new ApiException
                {
                    Id = entity.ID,
                    ExceptionType = entity.EXCEPTIONTYPE,
                    Code = entity.CODE,
                    Description = entity.DESCRIPTION,
                    StackTrace = entity.STACKTRACE,
                    ExceptionDateTime = entity.EXCEPTIONDATETIME
                };
            }

            return null;
        }

        public WE_T_API_EXCEPTIONS_LOG MapModelToEntity(ApiException obj)
        {
            if (obj != null)
            {
                return new WE_T_API_EXCEPTIONS_LOG
                {
                    ID = obj.Id,
                    EXCEPTIONTYPE = obj.ExceptionType,
                    CODE = obj.Code,
                    DESCRIPTION = obj.Description,
                    STACKTRACE = obj.StackTrace,
                    EXCEPTIONDATETIME = obj.ExceptionDateTime
                };
            }

            return null;
        }

        public long InsertException(ApiException app)
        {
            var newexception = MapModelToEntity(app);
            unitOfWork.ApiExceptionsRepo.Insert(newexception);
            unitOfWork.Save();

            return newexception.ID;
        }
    }
}
