using Derin.Business.BusinessLogic.Base;
using Derin.Business.ViewModel.Administration;
using Derin.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Derin.Data.Repository;
using Derin.Data.UnitOfWork.Derin;

namespace Derin.Business.BusinessLogic.Administration
{
    public class SystemUserTicketBL : BaseBL<SystemUserTicket, SystemUserTicketVM>
    {
        private IUnitOfWork _unitOfWork;
        public IGenericRepository<SystemUserTicket> CRUD;
        public SystemUserTicketBL(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CRUD = _unitOfWork.SystemUserTicketRepository;
        }
        public override List<SystemUserTicketVM> GetVM(Expression<Func<SystemUserTicket, bool>> filter = null, Func<IQueryable<SystemUserTicket>, IOrderedQueryable<SystemUserTicket>> orderBy = null, int? take = default(int?), int? skip = default(int?), params Expression<Func<SystemUserTicket, object>>[] includes)
        {
            return null;
        }

        public override void Save()
        {
            _unitOfWork.Save();
        }
    }
}
