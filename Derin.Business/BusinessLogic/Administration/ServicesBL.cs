using Derin.Business.BusinessLogic.Base;
using Derin.Business.ViewModel.Administration;
using Derin.Data.Model;
using Derin.Data.Repository;
using Derin.Data.UnitOfWork.Derin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Derin.Business.BusinessLogic.Administration
{
    public class ServicesBL : BaseBL<Services, ServicesVM>
    {
        private IUnitOfWork _unitOfWork;
        public IGenericRepository<Services> CRUD;

        public ServicesBL(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CRUD = _unitOfWork.ServicesRepository;
        }

        public override List<ServicesVM> GetVM(Expression<Func<Services, bool>> filter = null, Func<IQueryable<Services>, IOrderedQueryable<Services>> orderBy = null, int? take = null, int? skip = null, params Expression<Func<Services, object>>[] includes)
        {
            return CRUD.Query(filter, orderBy, take, skip, includes).Select(x => new ServicesVM
            {
                IdServices = x.IdServices,
                Title = x.Title,
                FullText = x.FullText,
                Summary = x.Summary,
                Icon = x.Icon
            }).ToList();
        }

        public override void Save()
        {
            _unitOfWork.Save();
        }
    }
}
