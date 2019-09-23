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
    public class CustomerBl : BaseBl
    {


        public Customer GetByEntity(TWMCUSTOMER entity)
        {
            return MapEntityToObject(entity);
        }

        public List<Customer> GetByEntities(IEnumerable<TWMCUSTOMER> entities)
        {
            if (entities != null && entities.Count() > 0)
            {
                return entities.Select(m => MapEntityToObject(m)).ToList();
            }

            return null;
        }

        public List<Customer> GetByWorkRequestId(long workRequestId)
        {
            return GetByEntities(unitOfWork.CustomerRepo.Get(m => m.CD_WR == workRequestId));
        }

        public Customer GetByWorkRequestIdCustomerId(long workRequestId, string customerId)
        {
            return GetByEntity(unitOfWork.CustomerRepo.GetSingle(m => m.CD_WR == workRequestId && m.ID_CUSTOMER == customerId));
        }

        public Customer MapEntityToObject(TWMCUSTOMER entity)
        {
            if (entity != null)
            {
                Customer obj = new Customer();

                obj.WorkRequestId = entity.CD_WR;
                obj.Address = new AddressBl().MapEntityToObject(entity.TWMADDRESS);
                obj.Phones = new Phones();
                obj.Name = entity.NM_CONTACT;
                obj.CustomerType = null;
                obj.SSN = entity.CD_SSN;
                obj.EffectiveMoveInDate = null;
                obj.DogCode = null;
                obj.CustomerID = entity.ID_CUSTOMER;
                obj.PremiseID = entity.ID_PREMISE;
                obj.PhoneNumber = entity.TL_CUSTOMER;

                return obj;
            }

            return null;
        }
    }
}
