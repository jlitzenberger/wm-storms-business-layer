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
    public class AttribBl : BaseBl
    {

        public Attrib Get(TWMATTRIBUTE entity)
        {
            if (entity != null)
            {
                return MapEntityToObject(entity);
            }
            return null;
        }

        public List<Attrib> GetAll()
        {
            return unitOfWork.AttributeRepo.Get(m => m.FG_ACTIVE == "Y").Select(m => MapEntityToObject(m)).OrderByDescending(d => d.LastChangedTimeStamp).ToList();               
        }

        public List<Attrib> GetAllByDate(DateTime changeDate)
        {
            return GetByEntities(unitOfWork.AttributeRepo.Get(m => m.TS_LAST_CHANGED >= changeDate).ToList());
        }

        public List<Attrib> GetByEntities(IEnumerable<TWMATTRIBUTE> entities)
        {
            if (entities != null && entities.Count() > 0)
            {
                return entities.Select(m => MapEntityToObject(m)).ToList();
            }

            return null;
        }

        public List<Attrib> GetfacDescription(string FacId)
        {
            return unitOfWork.AttributeRepo.Get(m => m.FG_ACTIVE == "Y" && m.CD_ATTRIBUTE == FacId).Select(m => MapEntityToObject(m)).ToList();
            //Todo           
        }

        public IEnumerable<Attrib> MapEntitiesToObjects(IEnumerable<TWMATTRIBUTE> entities)
        {
            List<Attrib> objs = new List<Attrib>();

            foreach (var item in entities)
            {
                objs.Add(MapEntityToObject(item));
            }
            return objs;
        }


        public Attrib MapEntityToObject(TWMATTRIBUTE obj)
        {
            if (obj != null)
            {
                return new Attrib
                {
                    ActiveFlag = obj.FG_ACTIVE,
                    AttributeCode = obj.CD_ATTRIBUTE,
                    RequiredFlag = obj.FG_REQUIRED,
                    AttributeIndicator = obj.IND_ATTRIBUTE,
                    CollectionIndicator = obj.IND_COLLECTION,
                    DataTypeIndicator = obj.IND_DATATYPE,
                    ValidationIndicator = obj.IND_VALIDATION,
                    DatafieldLength = obj.LN_DATAFIELD,
                    LastChangedTimeStamp = obj.TS_LAST_CHANGED,
                    FieldNameText = obj.TXT_FIELDNAME,
                    TargetColumnText = obj.TXT_TARGET_COLUMN,
                    TargetTableText = obj.TXT_TARGET_TABLE
                };

            }
            return null;
        }



    }
}
