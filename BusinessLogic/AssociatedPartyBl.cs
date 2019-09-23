using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WM.Common;
using WM.Common.Interfaces;
using WM.STORMS.BusinessLayer.Models;
using WM.STORMS.DataAccessLayer;

namespace WM.STORMS.BusinessLayer.BusinessLogic
{
    public class AssociatedPartyBl : BaseBl
    {
        private AddressBl addressBl;
        public AssociatedPartyBl()            
        {
            addressBl = new AddressBl();
        }

        public AssociatedParty GetByEntity(TWMASSOCPARTY entity)
        {
            return MapEntityToObject(entity);
        }

        public List<AssociatedParty> GetByEntities(IEnumerable<TWMASSOCPARTY> entity)
        {
            if (entity != null && entity.Count() > 0)
            {
                return entity.Select(m => MapEntityToObject(m)).ToList();
            }

            return null;
        }

        public List<AssociatedParty> GetByWorkRequestId(long workRequestId)
        {
            var assParties = unitOfWork.AssocPartyRepo.Get(m => m.CD_WR == workRequestId);

            if (assParties != null && assParties.Count() > 0)
            {
                return assParties.Select(m => MapEntityToObject(m)).ToList();
            }

            return null;
        }

        public AssociatedParty GetByWorkRequestIdSeq(long workRequestId, long seq)
        {
            return GetByEntity(unitOfWork.AssocPartyRepo.GetSingle(m => m.CD_WR == workRequestId && m.CD_SEQ == seq));
        }

        public AssociatedParty MapEntityToObject(TWMASSOCPARTY entity)
        {
            if (entity != null)
            {
                AssociatedParty obj = new AssociatedParty();

                obj.WorkRequestId = entity.CD_WR;
                obj.Seq = entity.CD_SEQ;
                obj.Address = addressBl.MapEntityToObject(entity.TWMADDRESS);
                obj.Phones = new Phones();
                obj.EntityType = entity.TP_ENTITY;

                return obj;
            }

            return null;
        }
    }
}
