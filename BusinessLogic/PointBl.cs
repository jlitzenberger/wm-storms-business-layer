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
    public class PointBl : BaseBl
    {


        public Point Get(TWMPOINT_EST entity)
        {
            if (entity != null)
            {
                Point obj = MapEntityToObject(entity);

                return obj;
            }

            return null;
        }
        public List<Point> Get(IEnumerable<TWMPOINT_EST> entities)
        {
            IEnumerable<Point> objs = MapEntitiesToObjects(entities);

            if (objs != null)
            {
                return objs.ToList();
            }

            return null;
        }

        public List<Point> GetPoints(long workRequestId, int specId)
        {
            List<Point> obj = Get(unitOfWork.PointEstRepo.Get(m => m.CD_WR == workRequestId && m.NO_DESIGN == specId));

            return obj;
        }
        public Point GetPoint(long workRequestId, int specId, string pointId, string pointSpanId)
        {
            Point obj = Get(unitOfWork.PointEstRepo.GetSingle(m => m.CD_WR == workRequestId
                                                                      && m.NO_DESIGN == specId
                                                                      && m.NO_POINT == pointId
                                                                      && m.NO_POINT_SPAN == pointSpanId));

            return obj;
        }

        public int CreateExternalDesign(Design design, List<Point> points, ExtDesignKey key)
        {
            IEnumerable<TWMIFEXTDSGN_PT> entities = MapObjectsToIfEntities(design, points, key);

            if (entities != null && entities.Count() > 0)
            {
                foreach (var item in entities)
                {
                    unitOfWork.IfExtDesignPtRepo.Insert(item);
                    unitOfWork.Save();
                }
            }

            return 0;
        }

        public void Update(Point obj)
        {
            var entity = MapObjectToEntity(obj);

            if (entity != null)
            {
                unitOfWork.PointEstRepo.Update(entity);
                unitOfWork.Save();
            }
        }

        public Point MapEntityToObject(TWMPOINT_EST entity)
        {
            Point obj = new Point();

            obj.WorkRequest = entity.CD_WR.ToString();
            obj.DesignNumber = entity.NO_DESIGN.ToString();
            obj.PointID = entity.ID_POINT;
            obj.PointNumber = entity.NO_POINT;
            obj.PointSpanNumber = entity.NO_POINT_SPAN;
            obj.Length = entity.LN_SPAN.ToString();
            obj.MainStatusIndicator = entity.IND_MAIN_STATUS;
            obj.RestorationFlag = entity.FG_RWORKS;
            obj.District = entity.CD_DIST;

            obj.CUs = entity.TWMCUPOINT_ESTs != null && entity.TWMCUPOINT_ESTs.Count > 0 ? new CUBl().Get(entity.TWMCUPOINT_ESTs) : null;

            return obj;
        }
        public IEnumerable<Point> MapEntitiesToObjects(IEnumerable<TWMPOINT_EST> entities)
        {
            List<Point> objs = new List<Point>();

            foreach (var item in entities)
            {
                objs.Add(MapEntityToObject(item));
            }

            return objs;
        }
        public TWMPOINT_EST MapObjectToEntity(Point obj)
        {
            TWMPOINT_EST entity = new TWMPOINT_EST();

            entity = MapRootObjectToEntity(obj, entity);

            if (obj.CUs != null && obj.CUs.Count > 0)
            {
                entity.TWMCUPOINT_ESTs = new CUBl().MapObjectsToEntities(obj.CUs).ToList();
            }

            return entity;
        }
        public IEnumerable<TWMPOINT_EST> MapObjectsToEntities(IEnumerable<Point> objs)
        {
            List<TWMPOINT_EST> entities = new List<TWMPOINT_EST>();

            foreach (var item in objs)
            {
                entities.Add(MapObjectToEntity(item));
            }

            return entities;
        }
        public TWMPOINT_EST MapRootObjectToEntity(Point obj, TWMPOINT_EST entity)
        {
            entity.CD_WR = (long)int.Parse(obj.WorkRequest);
            //entity.NO_DESIGN = (short)int.Parse(obj.DesignNumber);
            entity.ID_POINT = obj.PointID;
            entity.NO_POINT = obj.PointNumber;
            entity.NO_POINT_SPAN = obj.PointSpanNumber;
            //entity.LN_SPAN = (decimal)int.Parse(obj.Length);
            entity.CD_DIST = obj.District;
            entity.FG_RWORKS = obj.RestorationFlag;
            entity.IND_MAIN_STATUS = obj.MainStatusIndicator;

            entity.NO_DESIGN = 1;
            entity.LN_SPAN = 0;

            return entity;
        }

        public Point TransformToPoint(CuJobCode obj)
        {
            Point point = new Point();

            point.PointNumber = obj.NO_POINT;
            point.PointSpanNumber = obj.NO_POINT_SPAN;

            return point;
        }
        public List<Point> TransformToPoints(string workRequestId, List<CuJobCode> objs)
        {
            //get the distinct points
            List<WM.STORMS.BusinessLayer.Models.CuJobCode> distinctPoints = objs.GroupBy(x => new { x.NO_POINT, x.NO_POINT_SPAN })
                                                                                .Select(g => g.First())
                                                                                .ToList();

            List<Point> points = new List<Point>();
            foreach (var distinctPoint in distinctPoints)
            {
                List<CU> cus = new List<CU>();
                foreach (var cu in objs)
                {
                    if (distinctPoint.NO_POINT == cu.NO_POINT && distinctPoint.NO_POINT_SPAN == cu.NO_POINT_SPAN)
                    {
                        cus.Add(new CuJobCodeBl().TransformToCu(workRequestId, cu));
                    }
                }

                Point p = TransformToPoint(distinctPoint);
                p.WorkRequest = workRequestId;
                p.CUs = new List<CU>();
                p.CUs.AddRange(cus);

                //add all the CUS to the distinct point
                points.Add(p);
            }

            return points;
        }

        private TWMIFEXTDSGN_PT MapObjectToIfEntity(Design design, Point point, ExtDesignKey key)
        {
            TWMIFEXTDSGN_PT entity = new TWMIFEXTDSGN_PT();

            entity.CD_SEQ_EXTDSGN = GetIfSequenceNo();
            entity.TS_EXTDSGN = key.TsExtDsgn;
            entity.ID_OPER = key.IdOper;
            entity.NO_POINT = point.PointNumber;
            entity.NO_POINT_SPAN = point.PointSpanNumber;
            entity.CD_DIST = design.District;
            entity.CD_WR = (long)int.Parse(point.WorkRequest);
            entity.AD_GR_1 = string.Empty;
            entity.AD_GR_2 = string.Empty;
            entity.TXT_DESN = string.Empty;
            entity.DT_RPTD = null;
            entity.DT_IN_SERVICE = null;
            entity.CD_TOWN_RANGE_SECT = string.Empty;
            entity.CD_TAX_DIST = string.Empty;
            entity.CD_SIDE_OF_STREET = string.Empty;
            entity.IND_WORK_STATUS = " ";
            entity.IND_MAIN_STATUS = "N";   //N = not designated, L = long, S = short
            entity.IND_PROCESS = "A";       //A = add, M = modify, D = delete
            entity.CD_ENTITY = string.Empty;
            entity.CD_ISOLATION_SECT = string.Empty;
            entity.CD_LANDMARK = string.Empty;
            entity.CD_POLITICAL_SUB = string.Empty;
            entity.CD_SCHOOL_TAX = string.Empty;
            entity.AMT_FIXED_BID = null;
            entity.NO_DRAWING = string.Empty;
            if (point.Length == null)
            {
                entity.LN_SPAN = (decimal?)0.00;
            }
            else
            {
                entity.LN_SPAN = (decimal?)Convert.ToDecimal(point.Length);
            }
            entity.NO_MAP = string.Empty;
            entity.AD_POINT = string.Empty;
            entity.ID_POINT = string.IsNullOrEmpty(point.PointID) ? string.Empty : point.PointID;
            entity.CD_CREW = string.Empty;
            entity.IND_CNTCR_CALC_MTH = "1";   //1, 2, 3, 4
            entity.FG_ERROR = "N";
            entity.CD_BID_ITEM = string.Empty;
            entity.FG_RWORKS = "N";   //"N" or "Y", default is "N"
            entity.CD_SEQ_ERROR_RUN = null;
            entity.NO_COMPLEXITY = null;
            entity.QT_BID_ITEM = 0;

            return entity;
        }
        public IEnumerable<TWMIFEXTDSGN_PT> MapObjectsToIfEntities(Design design, List<Point> objs, ExtDesignKey key)
        {
            List<TWMIFEXTDSGN_PT> entities = new List<TWMIFEXTDSGN_PT>();

            foreach (var item in objs)
            {
                entities.Add(MapObjectToIfEntity(design, item, key));
            }

            return entities;
        }

        private long GetIfSequenceNo()
        {
            List<Decimal> a = unitOfWork.GenericSqlRepo.RunRawSql<Decimal>(" select we_s_so_extdsg.nextval FROM dual").ToList();

            return Convert.ToInt64(a[0]);
        }
    }
}
