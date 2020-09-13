using Derin.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Derin.Business.BusinessLogic.Base
{
    public abstract class BaseBL<TEntity, TViewModel>
    {
        public abstract List<TViewModel> GetVM(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int? take = null, int? skip = null, params Expression<Func<TEntity, object>>[] includes);

        public abstract void Save();

        private DerinEntities _entityForSP;

        protected DerinEntities EntityForSP
        {
            get { return _entityForSP == null ? _entityForSP = new DerinEntities(ConnectionStrings.Derin_Prod) : _entityForSP; }
        }

    }
}
