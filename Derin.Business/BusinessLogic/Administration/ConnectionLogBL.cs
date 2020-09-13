using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Derin.Data.Repository;
using Derin.Business.BusinessLogic.Base;
using Derin.Business.ViewModel.Administration;
using Derin.Data.Model;
using Derin.Data.UnitOfWork.Derin;

namespace Derin.Business.BusinessLogic.Administration
{
    public class ConnectionLogBL : BaseBL<ConnectionLog, ConnectionLogVM>
    {
        private IUnitOfWork _unitOfWork;
        public IGenericRepository<ConnectionLog> CRUD;
        public ConnectionLogBL(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CRUD = _unitOfWork.ConnectionLogRepository;
        }
        public override List<ConnectionLogVM> GetVM(Expression<Func<ConnectionLog, bool>> filter = null, Func<IQueryable<ConnectionLog>, IOrderedQueryable<ConnectionLog>> orderBy = null, int? take = default(int?), int? skip = default(int?), params Expression<Func<ConnectionLog, object>>[] includes)
        {
            return CRUD.Query(filter, orderBy, take, skip, includes).Select(x => new ConnectionLogVM
            {
                Username = x.Username,
                IdConnectionLog=x.IdConnectionLog,
                LogDate=x.LogDate,
                IpAddress=x.IpAddress,
                ErrorCode=x.ErrorCode

            }).ToList();
        }

        //public List<UserLastEvents_Result> GetUserLastEvents(long idUser)
        //{
        //    return EntityForSP.UserLastEvents(idUser).ToList();
        //}
        public override void Save()
        {
            _unitOfWork.Save();
        }
    }
}