using System;
using System.Collections.Generic;
//using System.Data.EntityClient;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WM.STORMS.BusinessLayer.Models;
using WM.STORMS.DataAccessLayer;
using WM.Common;
using WM.Common.Interfaces;

namespace WM.STORMS.BusinessLayer.BusinessLogic
{
    public class EquipmentBl : BaseBl
    {


        public Equipment GetByEntity(TWMEQUIPTYPE entity)
        {
            return MapEntityToObject(entity);
        }

        public List<Equipment> GetByEntities(IEnumerable<TWMEQUIPTYPE> entities)
        {
            if (entities != null && entities.Count() > 0)
            {
                return entities.Select(m => MapEntityToObject(m)).ToList();
            }

            return null;
        }

        public List<Equipment> Get()
        {
            return GetByEntities(unitOfWork.EquipmentRepo.GetAll());
        }

        public Equipment GetEquipment(string Equipment, string CustomerType)
        {
            return GetByEntity(unitOfWork.EquipmentRepo.GetSingle(m => m.TP_EQUIP == Equipment && m.TP_CUSTOMER == CustomerType));
        }

        public List<Equipment> GetEquipmentsByCustomerType(string CustomerType)
        {
            return GetByEntities(unitOfWork.EquipmentRepo.Get(m => m.TP_CUSTOMER == CustomerType && m.FG_ACTIVE == "Y"));
        }

        public Equipment MapEntityToObject(TWMEQUIPTYPE entity)
        {
            if (entity != null)
            {
                Equipment obj = new Equipment();

                obj.EquipmentId = entity.TP_EQUIP;
                obj.EquipmentDescription = entity.DS_EQUIP_TYPE;
                obj.CustomerType = entity.TP_CUSTOMER;
                obj.Active = entity.FG_ACTIVE == "Y" ? true : false;

                return obj;
            }

            return null;
        }
    }
}