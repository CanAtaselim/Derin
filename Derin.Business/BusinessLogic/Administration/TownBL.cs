using Derin.Business.BusinessLogic.Base;
using Derin.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Derin.Data.Repository;
using Derin.Data.UnitOfWork.Derin;
using Derin.Business.ViewModel.Administration;

namespace Derin.Business.BusinessLogic.Administration
{
    public class TownBL : BaseBL<Town, TownVM>
    {
        private IUnitOfWork _unitOfWork;
        public IGenericRepository<Town> CRUD;
        public TownBL(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CRUD = _unitOfWork.TownRepository;
        }
        public override List<TownVM> GetVM(Expression<Func<Town, bool>> filter = null, Func<IQueryable<Town>, IOrderedQueryable<Town>> orderBy = null, int? take = default(int?), int? skip = default(int?), params Expression<Func<Town, object>>[] includes)
        {
            return CRUD.Query(filter, orderBy).Select(x => new TownVM
            {
                TownCode = x.TownCode,
                TownName = x.TownName,
                IdTown = x.IdTown
            }).ToList();
        }
        public List<TownVM> GetVMWithPermission(Expression<Func<Town, bool>> filter = null, Func<IQueryable<Town>, IOrderedQueryable<Town>> orderBy = null, int? take = default(int?), int? skip = default(int?), string roleName = null, params Expression<Func<Town, object>>[] includes)
        {
            return CRUD.Query(filter, orderBy).Select(x => new TownVM
            {
                TownCode = x.TownCode,
                TownName = x.TownName,
                IdTown = x.IdTown,
                RoleCode = roleName
            }).ToList();
        }

        public override void Save()
        {
            throw new NotImplementedException();
        }
    }
}
