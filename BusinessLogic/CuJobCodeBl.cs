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
    public class CuJobCodeBl : BaseBl
    {


        public CuJobCode Get(TWMCUJOBCODE entity)
        {
            if (entity != null)
            {
                CuJobCode obj = MapEntityToObject(entity);

                return obj;
            }

            return null;
        }
        public List<CuJobCode> Get(IEnumerable<TWMCUJOBCODE> entities)
        {
            IEnumerable<CuJobCode> objs = MapEntitiesToObjects(entities);

            if (objs != null)
            {
                return objs.ToList();
            }

            return null;
        }
        public List<CuJobCode> Get()
        {
            throw new NotImplementedException();
        }

        public CuJobCode GetCuJobCode(string JobCode, string Mu, string Cu)
        {
            return Get(unitOfWork.CuJobCodeRepo.Get(m => m.CD_JOB == JobCode).FirstOrDefault());
        }     
        public List<CuJobCode> GetCuJobCodes(string JobCode)
        {
            return Get(unitOfWork.CuJobCodeRepo.Get(m => m.CD_JOB == JobCode));
        }

        public void Create(CuJobCode obj)
        {
            if (obj != null)
            {
                Delete(obj);
                Insert(obj);
            }
        }
        public void Create(List<CuJobCode> objs)
        {
            foreach (var item in objs)
            {
                Create(item);
            }
        }
        public void Update(CuJobCode obj)
        {
            UpdateRoot(obj);
        }
        public void Update(List<CuJobCode> objs)
        {
            foreach (var item in objs)
            {
                Update(item);
            }
        }
        private void UpdateRoot(CuJobCode obj)
        {
            var entity = unitOfWork.CuJobCodeRepo.GetSingle(m => m.CD_JOB == obj.CD_JOB && m.CD_MU == obj.CD_MU && m.CD_CU == obj.CD_CU);
            entity = MapRootObjectToEntity(obj, entity);

            unitOfWork.CuJobCodeRepo.Update(entity);
            unitOfWork.Save();
        }
        public void Delete(CuJobCode obj)
        {
            unitOfWork.CuJobCodeRepo.Delete(obj.CD_JOB, obj.CD_MU, obj.CD_CU);
            unitOfWork.Save();
        }
        public void Delete(List<CuJobCode> objs)
        {
            throw new NotImplementedException();
        }
        public void Insert(CuJobCode obj)
        {
            unitOfWork.CuJobCodeRepo.Insert(MapObjectToEntity(obj));
            unitOfWork.Save();
        }
        public void Insert(List<CuJobCode> objs)
        {
            throw new NotImplementedException();
        }
        
        public CU TransformToCu(string workRequestId, CuJobCode obj)
        {
            CU cu = new CU();

            cu.WorkRequest = workRequestId;
            cu.UnitCode = obj.CD_CU;
            cu.UnitAction = obj.IND_ACTION;
            cu.Account = obj.IND_ACCT;
            cu.PointNumber = obj.NO_POINT;
            cu.PointSpanNumber = obj.NO_POINT_SPAN;
            cu.Usage = obj.CD_USAGE;
            cu.OnOff = obj.IND_ON_OFF;
            cu.MUID = obj.CD_MU;
            cu.CrewClass = obj.CD_CREW_CLASS;
            cu.Quantity = Convert.ToInt16(obj.QT_ACTION).ToString();
            cu.SupplyMethod = " ";
            cu.EquipNumber = " ";

            cu.UnitDetail = null;
            cu.CUFacilityAttributes = null;
            cu.LaborDetails = null;
            
            return cu;
        }
        private List<CU> TransformToCus(List<CuJobCode> objs)
        {
            List<CU> cus = new List<CU>();

            foreach (var item in objs)
            {
                cus.Add(TransformToCu("", item));
            }

            return cus;
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
                        cus.Add(TransformToCu(workRequestId, cu));
                    }
                }

                Point p = TransformToPoint(distinctPoint);
                p.WorkRequest = workRequestId;
                p.CUs.AddRange(cus);

                //add all the CUS to the distinct point
                points.Add(p);

            }

            return points;
        }

        public TWMCUJOBCODE MapObjectToEntity(CuJobCode obj)
        {
            TWMCUJOBCODE entity = new TWMCUJOBCODE();

            entity = MapRootObjectToEntity(obj, entity);
            
            return entity;
        }
        public IEnumerable<TWMCUJOBCODE> MapObjectsToEntities(IEnumerable<CuJobCode> objs)
        {
            List<TWMCUJOBCODE> entities = new List<TWMCUJOBCODE>();

            foreach (var item in objs)
            {
                entities.Add(MapObjectToEntity(item));
            }

            return entities;
        }
        public CuJobCode MapEntityToObject(TWMCUJOBCODE entity)
        {
            CuJobCode obj = new CuJobCode();

            obj.CD_JOB = entity.CD_JOB;
            obj.CD_CU = entity.CD_CU;
            obj.NO_POINT = entity.NO_POINT;
            obj.NO_POINT_SPAN = entity.NO_POINT_SPAN;
            obj.CD_USAGE = entity.CD_USAGE;
            obj.IND_ACCT = entity.IND_ACCT;
            obj.IND_ACTION = entity.IND_ACTION;
            obj.IND_ON_OFF = entity.IND_ON_OFF;
            obj.QT_ACTION = entity.QT_ACTION;
            obj.CD_FACILITY = entity.CD_FACILITY;
            obj.CD_JOBCDPACKET = entity.CD_JOBCDPACKET;
            obj.HR_LBR = entity.HR_LBR;
            obj.CD_CREW_CLASS = entity.CD_CREW_CLASS;
            obj.CD_MU = entity.CD_MU;

            //obj.MilestoneRequirements = new MilestoneRequirementBl(dbManagerBase.iOrigin, dbManagerBase.iEnvironment).GetMilestoneRequirements(entity.TWMMILESTONERQMTs);

            return obj;
        }
        public IEnumerable<CuJobCode> MapEntitiesToObjects(IEnumerable<TWMCUJOBCODE> entities)
        {
            List<CuJobCode> objs = new List<CuJobCode>();

            foreach (var item in entities)
            {
                objs.Add(MapEntityToObject(item));
            }

            return objs;
        }
        public TWMCUJOBCODE MapRootObjectToEntity(CuJobCode obj, TWMCUJOBCODE entity)
        {
            entity.CD_JOB = obj.CD_JOB;
            entity.CD_CU = obj.CD_CU;
            entity.NO_POINT = obj.NO_POINT;
            entity.NO_POINT_SPAN = obj.NO_POINT_SPAN;
            entity.CD_USAGE = obj.CD_USAGE;
            entity.IND_ACCT = obj.IND_ACCT;
            entity.IND_ACTION = obj.IND_ACTION;
            entity.IND_ON_OFF = obj.IND_ON_OFF;
            entity.QT_ACTION = obj.QT_ACTION;
            entity.CD_FACILITY = obj.CD_FACILITY;
            entity.CD_JOBCDPACKET = obj.CD_JOBCDPACKET;
            entity.HR_LBR = obj.HR_LBR;
            entity.CD_CREW_CLASS = obj.CD_CREW_CLASS;
            entity.CD_MU = obj.CD_MU;

            return entity;
        }
    }
}
