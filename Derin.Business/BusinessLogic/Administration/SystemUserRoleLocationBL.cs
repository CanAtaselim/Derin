using Derin.Business.BusinessLogic.Base;
using Derin.Business.ViewModel.Administration;
using Derin.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Derin.Data.UnitOfWork.Derin;
using Derin.Data.Repository;

namespace Derin.Business.BusinessLogic
{
    public class SystemUserRoleLocationBL : BaseBL<SystemUserRoleLocation, SystemUserRoleLocationVM>
    {
        private IUnitOfWork _unitOfWork;
        public IGenericRepository<SystemUserRoleLocation> CRUD;
        public SystemUserRoleLocationBL(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CRUD = _unitOfWork.SystemUserRoleLocationRepository;
        }
        public override List<SystemUserRoleLocationVM> GetVM(Expression<Func<SystemUserRoleLocation, bool>> filter = null, Func<IQueryable<SystemUserRoleLocation>, IOrderedQueryable<SystemUserRoleLocation>> orderBy = null, int? take = null, int? skip = null, params Expression<Func<SystemUserRoleLocation, object>>[] includes)
        {
            return null;
        }
        public override void Save()
        {
            _unitOfWork.Save();
        }
    }
}
