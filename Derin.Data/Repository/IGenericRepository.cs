using Derin.Data.DataCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Data.Repository
{
    public interface IGenericRepository<TEntity>
    {

        List<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int? take = null, int? skip = null,
            params Expression<Func<TEntity, object>>[] includes);

        List<TEntity> GetExtended(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int? take = null, int? skip = null, string orderByS = null, short? orderDirection = null,
           params Expression<Func<TEntity, object>>[] includes);

        IQueryable<TEntity> QueryExtended(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int? take = null, int? skip = null, string orderByS = null, short? orderDirection = null,
           params Expression<Func<TEntity, object>>[] includes);

        long GetCount(
            Expression<Func<TEntity, bool>> filter = null,
            params Expression<Func<TEntity, object>>[] includes);

        bool Any(Expression<Func<TEntity, bool>> filter = null,
            params Expression<Func<TEntity, object>>[] includes);

        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int? take = null, int? skip = null,
            params Expression<Func<TEntity, object>>[] includes);

        TEntity GetById(object id);

        TEntity GetFirstOrDefault(
            Expression<Func<TEntity, bool>> filter = null,
            params Expression<Func<TEntity, object>>[] includes);

        void BulkInsert(List<TEntity> entities);

        void Insert(TEntity entity);

        void InsertNoDetect(TEntity entity);

        void Update(TEntity entity, HttpRequestInfo reqInfo);

        void Delete(object id, HttpRequestInfo reqInfo);

        void DeleteWithoutLogging(object id);

        void DetachAllEntities();
    }
}
