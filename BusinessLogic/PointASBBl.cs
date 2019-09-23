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
    public class PointASBBl : BaseBl
    {


        public PointAsb Get(TWMPOINT_ASB entity)
        {
            if (entity != null)
            {
                PointAsb obj = MapEntityToObject(entity);

                return obj;
            }

            return null;
        }
   
        public List<PointAsb> Get(IEnumerable<TWMPOINT_ASB> entities)
        {
            IEnumerable<PointAsb> objs = MapEntitiesToObjects(entities);

            if (objs != null)
            {
                return objs.ToList();
            }

            return null;
        }

        public List<PointAsb> GetPoints(long workRequestId)
        {
            List<PointAsb> obj = Get(unitOfWork.PointAsbRepo.Get(m => m.CD_WR == workRequestId));

            return obj;
        }

        public void Update(PointAsb obj)
        {
            var entity = MapObjectToEntity(obj);

            if (entity != null)
            {
                unitOfWork.PointAsbRepo.Update(entity);
                unitOfWork.Save();
            }
        }

        public PointAsb MapEntityToObject(TWMPOINT_ASB entity)
        {
            PointAsb obj = new PointAsb();

            obj.WorkRequestId = entity.CD_WR;                        
            obj.PointID = entity.ID_POINT;
            obj.PointNumber = entity.NO_POINT;
            obj.PointSpanNumber = entity.NO_POINT_SPAN;
            obj.Length = entity.LN_SPAN.ToString();
            obj.AsbuiltDesignNumber = entity.NO_ASB_DESIGN;
            obj.District = entity.CD_DIST;
            obj.TimeStampLastChanged = entity.TS_LAST_CHANGED;
            obj.MainStatusIndicator = entity.IND_MAIN_STATUS;

            obj.CUAsbs = entity.TWMCUPOINT_ASBs != null && entity.TWMCUPOINT_ASBs.Count > 0 ? new CUAsbBl().GetByWorkRequestIdandPoint((long)entity.CD_WR, obj.PointNumber, obj.PointSpanNumber) : null;
           
            return obj;
        }

        public IEnumerable<PointAsb> MapEntitiesToObjects(IEnumerable<TWMPOINT_ASB> entities)
        {
            List<PointAsb> objs = new List<PointAsb>();

            foreach (var item in entities)
            {
                objs.Add(MapEntityToObject(item));
            }

            return objs;
        }

        public TWMPOINT_ASB MapObjectToEntity(PointAsb obj)
        {
            TWMPOINT_ASB entity = new TWMPOINT_ASB();        

            entity = MapRootObjectToEntity(obj, entity);

            if (obj.CUAsbs != null && obj.CUAsbs.Count > 0)
            {
                entity.TWMCUPOINT_ASBs = new CUAsbBl().MapObjectsToEntities(obj.CUAsbs).ToList();
            }

            return entity;
        }

        public IEnumerable<TWMPOINT_ASB> MapObjectsToEntities(IEnumerable<PointAsb> objs)
        {
            List<TWMPOINT_ASB> entities = new List<TWMPOINT_ASB>();

            foreach (var item in objs)
            {
                entities.Add(MapObjectToEntity(item));
            }

            return entities;
        }

        public TWMPOINT_ASB MapRootObjectToEntity(PointAsb obj, TWMPOINT_ASB entity)
        {
            entity.CD_WR = obj.WorkRequestId;         
            entity.ID_POINT = obj.PointID;
            entity.NO_POINT = obj.PointNumber;
            entity.NO_POINT_SPAN = obj.PointSpanNumber;
            entity.LN_SPAN = (decimal)int.Parse(obj.Length);
            //entity.LN_SPAN = 0;
            entity.CD_DIST = obj.District;
            entity.IND_MAIN_STATUS = obj.MainStatusIndicator;
            entity.NO_ASB_DESIGN = obj.AsbuiltDesignNumber;
            entity.TS_LAST_CHANGED = obj.TimeStampLastChanged;

            return entity;
        }



        //public List<PointAsb> TransformToPointsAsb(string workRequestId, List<CuJobCode> objs)
        //{
        //    //get the distinct points
        //    List<WM.STORMS.BusinessLayer.Models.CuJobCode> distinctAsbPoints = objs.GroupBy(x => new { x.NO_POINT, x.NO_POINT_SPAN })
        //                                                                                    .Select(g => g.First())
        //                                                                                    .ToList();

        //    List<PointAsb> pointAsbs = new List<PointAsb>();
        //    foreach (var distinctAsbPoint in distinctAsbPoints)
        //    {
        //        List<CUAsb> cuAsbs = new List<CUAsb>();
        //        foreach (var cuAsb in objs)
        //        {
        //            if (distinctPoint.NO_POINT == cu.NO_POINT && distinctPoint.NO_POINT_SPAN == cu.NO_POINT_SPAN)
        //            {
        //                cus.Add(new CuJobCodeBl().TransformToCu(workRequestId, cu));
        //            }
        //        }

        //        Point p = TransformToPoint(distinctPoint);
        //        p.WorkRequest = workRequestId;
        //        p.CUs = new List<CU>();
        //        p.CUs.AddRange(cus);

        //        //add all the CUS to the distinct point
        //        points.Add(p);
        //    }

        //    return points;
        //}

    }
}
