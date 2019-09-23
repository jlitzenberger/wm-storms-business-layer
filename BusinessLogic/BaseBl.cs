using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Common;
using WM.STORMS.DataAccessLayer;
using WM.STORMS.DataAccessLayer.Repositories;

namespace WM.STORMS.BusinessLayer.BusinessLogic 
{
    public class BaseBl
    {
        private UnitOfWork _unitOfWork;

        protected UnitOfWork unitOfWork
        {
            get
            {
                if (_unitOfWork == null)
                {
                    StormsEntities _context = new StormsEntities();
                    //_context.Database.Connection.ConnectionString = Oracle_ConnectStr;
                    _unitOfWork = new UnitOfWork(_context);
                }

                return _unitOfWork;
            }
            set
            {
                _unitOfWork = value;
            }
        }
    }
}
