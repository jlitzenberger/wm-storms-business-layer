using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WM.Common;
using WM.Common.Interfaces;
using WM.STORMS.BusinessLayer.Models;
using WM.STORMS.DataAccessLayer;
using WM.STORMS.DataAccessLayer.Repositories;

namespace WM.STORMS.BusinessLayer.BusinessLogic
{
    public class OperBl : BaseBl
    {
        public OperBl(string environment)
        {
            if (unitOfWork != null)
            {
                StormsEntities _context = new StormsEntities();

                switch (environment)
                {
                    case "DEV":
                        _context.Database.Connection.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["EnvironmentDEV"];
                        break;
                    case "TRN":
                        _context.Database.Connection.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["EnvironmentTRN"];
                        break;
                    case "TST":
                        _context.Database.Connection.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["EnvironmentSTG"];
                        break;
                    case "STG":
                        _context.Database.Connection.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["EnvironmentSTG"];
                        break;
                    case "PRD":
                        _context.Database.Connection.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["EnvironmentPRD"];
                        break;
                    default:
                        break;
                }                

                unitOfWork = new UnitOfWork(_context);
            }
        }

        public OperBl()
        {
        }

        public Oper GetByEntity(TWMOPER entity)
        {
            return MapEntityToObject(entity);
        }

        public Oper GetById(string operId)
        {
            return GetByEntity(unitOfWork.OperRepo.GetSingle(m => m.ID_OPER == operId));
        }

        public int ResetPassword(string idOper, string password)
        {
            return unitOfWork.OperRepo.ResetPassword(idOper, password);
        }

        public Oper MapEntityToObject(TWMOPER entity)
        {
            if (entity != null)
            {
                Oper obj = new Oper();
                    
                obj.District = entity.CD_DIST;
                obj.OperatorId = entity.ID_OPER;
                obj.Company = entity.CD_CMPNY;
                obj.CrewHq = entity.CD_CREWHQ;
                obj.EmailId = entity.ID_EMAIL;
                obj.EmployeeId = entity.ID_EMPL;
                obj.IsActive = entity.FG_ACTIVE;
                obj.LoginId = entity.ID_LOGIN;
                obj.OperatorName = entity.NM_OPER;
                obj.Division = entity.CD_DIV;
                obj.JobDescription = entity.TXT_JOB_DESC;
                obj.OperatorPassword = new OperatorPasswordBl().MapEntityToObject(entity.TWMOPERPASSWORD);
                
                return obj;
            }
            return null;
        }

        public TWMOPER MapObjectToEntity(Oper obj)
        {
            if (obj != null)
            {
                TWMOPER entity = new TWMOPER();

                entity.CD_DIST = obj.District;
                entity.ID_OPER = obj.OperatorId;
                entity.CD_CMPNY = obj.Company;
                entity.CD_CREWHQ = obj.CrewHq;
                entity.ID_EMAIL = obj.EmailId;
                entity.ID_EMPL = obj.EmployeeId;
                entity.FG_ACTIVE = obj.IsActive;
                entity.ID_LOGIN = obj.LoginId;
                entity.NM_OPER = obj.OperatorName;
                entity.CD_DIV = obj.Division;
                entity.TXT_JOB_DESC = obj.JobDescription;

                return entity;
            }            
            return null;
        }

    }
}

