using Derin.Business.BusinessLogic.Base;
using Derin.Business.ViewModel.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Derin.Data.Repository;
using Derin.Data.Model;
using Derin.Data.UnitOfWork.Derin;

namespace Derin.Business.BusinessLogic.Administration
{
    public class CityBL : BaseBL<City, CityVM>
    {
        private IUnitOfWork _unitOfWork;
        public IGenericRepository<City> CRUD;
        public CityBL(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CRUD = _unitOfWork.CityRepository;
        }
        public override List<CityVM> GetVM(Expression<Func<City, bool>> filter = null, Func<IQueryable<City>, IOrderedQueryable<City>> orderBy = null, int? take = default(int?), int? skip = default(int?), params Expression<Func<City, object>>[] includes)
        {
            return CRUD.Query(filter, orderBy, take, skip, includes).Select(x => new CityVM
            {
                CityCode = x.CityCode,
                CityName = x.CityName,
                IdCity = x.IdCity
            }).ToList();
        }
        public List<CityVM> GetVMWithPermission(Expression<Func<City, bool>> filter = null, Func<IQueryable<City>, IOrderedQueryable<City>> orderBy = null, int? take = default(int?), int? skip = default(int?), string roleCode = null, params Expression<Func<City, object>>[] includes)
        {
            return CRUD.Query(filter, orderBy, take, skip, includes).Select(x => new CityVM
            {
                CityCode = x.CityCode,
                CityName = x.CityName,
                IdCity = x.IdCity,
                RoleCode = roleCode
            }).ToList();
        }
        public override void Save()
        {
            _unitOfWork.Save();
        }
    }
}
