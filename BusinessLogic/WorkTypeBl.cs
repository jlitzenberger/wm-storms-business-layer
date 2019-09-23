using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Common;
using WM.STORMS.BusinessLayer.Models;
using WM.STORMS.DataAccessLayer;

namespace WM.STORMS.BusinessLayer.BusinessLogic
{
    public class WorkTypeBl : BaseBl
    {


        public List<WorkType> GetAll()
        {
            return unitOfWork.WorkTypeRepo.GetAll().Select(m => MapEntityToObject(m)).ToList();
        }

        public WorkType GetById(string id)
        {
            return MapEntityToObject(unitOfWork.WorkTypeRepo.GetSingle(m => m.TP_WORK == id));
        }

        private WorkType MapEntityToObject(TWMWORKTYPE obj)
        {
            if (obj != null)
            {
                return new WorkType
                {
                    Id = obj.TP_WORK,
                    Description = obj.DS_WORK
                };
            }

            return null;
        }
    }
}
