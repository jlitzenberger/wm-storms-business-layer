using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WM.STORMS.DataAccessLayer;
using WM.STORMS.BusinessLayer.Models;
using WM.Common;
using WM.Common.Interfaces;

namespace WM.STORMS.BusinessLayer.BusinessLogic
{
    public class AddressBl : BaseBl
    {

        public Address Get(TWMADDRESS entity)
        {
            return MapEntityToObject(entity);
        }

        public Address GetAddress(int addressId)
        {
            return Get(unitOfWork.AddressRepo.GetSingle(m => m.CD_ADDRESS == addressId));
        }

        private string AddressToFreeFormat(Address obj)
        {
            return (string.IsNullOrEmpty(obj.StreetNumber) ? string.Empty : obj.StreetNumber.Trim().ToUpper() + " ")
                  + (string.IsNullOrEmpty(obj.StreetPrefix) ? string.Empty : obj.StreetPrefix.Trim().ToUpper() + " ")
                  + (string.IsNullOrEmpty(obj.StreetName) ? string.Empty : obj.StreetName.Trim().ToUpper() + " ")
                  + (string.IsNullOrEmpty(obj.StreetType) ? string.Empty : obj.StreetType.Trim().ToUpper() + " ")
                  + (string.IsNullOrEmpty(obj.StreetSuffix) ? string.Empty : obj.StreetSuffix.Trim().ToUpper() + " ")
                  + (string.IsNullOrEmpty(obj.UnitType) ? string.Empty : obj.UnitType.Trim().ToUpper() + " ")
                  + (string.IsNullOrEmpty(obj.UnitID) ? string.Empty : obj.UnitID.Trim().ToUpper());
        }

        public void CreateAddress(DateTime workRequestDateTimeStamp, int workRequestSeqNum, Address address)
        {
            if (address != null)
            {
                unitOfWork.IfAddressRepo.Insert(MapObjectToEntity(workRequestDateTimeStamp, workRequestSeqNum, address));
                unitOfWork.Save();
            }
        }

        public TWMIFADDRESS MapObjectToEntity(DateTime workRequestDateTimeStamp, int workRequestSeqNum, Address obj)
        {
            if (obj != null)
            {
                TWMIFADDRESS entity = new TWMIFADDRESS();

                entity.CD_ADDRESS = workRequestSeqNum;
                entity.TS_ADDRESS = workRequestDateTimeStamp;
                entity.AD_BUILDING_NO = obj.StreetNumber;
                entity.AD_STREET = obj.StreetName;
                entity.AD_TOWN = obj.City;
                entity.AD_POSTAL = obj.Zip;
                entity.CD_STATE = obj.State;
                entity.FG_ERROR = "N";
                entity.IND_MAIN_SIDE = "N";
                entity.AD_INFO = obj.ExtraInfo;

                return entity;
            }

            return null;
        }

        public Address MapEntityToObject(TWMADDRESS entity)
        {
            if (entity != null)
            {
                Address obj = new Address();

                obj.Organization = entity.NM_ORGANIZATION;
                obj.Building = entity.NM_BUILDING;
                obj.Floor = entity.AD_FLOOR_NO;
                obj.StreetNumber = entity.AD_BUILDING_NO;
                obj.StreetPrefix = entity.AD_STREET_PFX;
                obj.StreetName = entity.AD_STREET;
                obj.StreetType = entity.TP_STREET;
                obj.StreetSuffix = entity.AD_STREET_SFX;
                obj.UnitID = entity.AD_UNIT_NO;
                obj.UnitType = entity.TP_UNIT;
                obj.City = entity.AD_TOWN;
                obj.County = entity.CD_COUNTY;
                obj.State = entity.CD_STATE;
                obj.Zip = entity.AD_POSTAL;
                obj.ExtraInfo = entity.AD_INFO;
                //obj.CityCode = entity.CD_WO_REMOV;
                obj.FreeFormat = AddressToFreeFormat(obj);

                return obj;
            }

            return null;
        }
    }
}