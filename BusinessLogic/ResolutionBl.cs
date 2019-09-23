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
    public class ResolutionBl : BaseBl
    {
 

        public Resolution Get(TWMRESOLUTION entity)
        {
            if (entity != null)
            {
                Resolution obj = MapEntityToObject(entity);

                return obj;
            }

            return null;
        }

        public List<Resolution> Get(IEnumerable<TWMRESOLUTION> entities)
        {
            IEnumerable<Resolution> objs = MapEntitiesToObjects(entities);

            if (objs != null)
            {
                return objs.ToList();
            }

            return null;
        }

        public Resolution GetByEntity(TWMRESOLUTION entity)
        {
            return MapEntityToObject(entity);
        }
        
        public List<Resolution> GetActiveResolutions(string active)
        {
            List<Resolution> obj = Get(unitOfWork.ResolutionRepo.Get( (m => m.FG_ACTIVE == active)));

            return obj;
        }

        public TWMRESOLUTION MapObjectToEntity(Resolution obj)
        {
            if (obj != null)
            {
                return new TWMRESOLUTION
                {
                    CD_RESOLUTION = obj.ResolutionCode,
                    DS_RESOLUTION = obj.ResolutionDescription,
                    FG_ACTIVE = obj.ActiveFlag
                };
            }
            return null;
        }

        public Resolution MapEntityToObject(TWMRESOLUTION obj)
        {
            if (obj != null)
            {
                return new Resolution                
                {
                    ActiveFlag = obj.FG_ACTIVE,
                    ResolutionCode = obj.CD_RESOLUTION,
                    ResolutionDescription = obj.DS_RESOLUTION
                };
            }
            return null;
        }

        public IEnumerable<Resolution> MapEntitiesToObjects(IEnumerable<TWMRESOLUTION> entities)
        {
            List<Resolution> objs = new List<Resolution>();

            foreach (var item in entities)
            {
                objs.Add(MapEntityToObject(item));
            }

            return objs;
        }
   
        public IEnumerable<TWMRESOLUTION> MapObjectsToEntities(IEnumerable<Resolution> objs)
        {
            List<TWMRESOLUTION> entities = new List<TWMRESOLUTION>();

            foreach (var item in objs)
            {
                entities.Add(MapObjectToEntity(item));
            }

            return entities;
        }
    }
}

