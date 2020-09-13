using Derin.Business.BusinessLogic.Base;
using Derin.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Derin.Data.Repository;
using Derin.Business.ViewModel.Administration;
using Derin.Data.UnitOfWork.Derin;

namespace Derin.Business.BusinessLogic.Administration
{
    public class CDCBL : BaseBL<CDC, CDCVM>
    {
        private IUnitOfWork _unitOfWork;
        public IGenericRepository<CDC> CRUD;
        public CDCBL(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CRUD = _unitOfWork.CDCRepository;
        }
        public override List<CDCVM> GetVM(Expression<Func<CDC, bool>> filter = null, Func<IQueryable<CDC>, IOrderedQueryable<CDC>> orderBy = null, int? take = default(int?), int? skip = default(int?), params Expression<Func<CDC, object>>[] includes)
        {
            return null;
        }

        public override void Save()
        {
            _unitOfWork.Save();
        }
    }
}
