using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml.Linq;
using Derin.Common;
using Derin.Data.Model;
using Derin.Data.DataCommon;
using System.Web.Routing;

namespace Derin.Data.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private DbContext context;
        private DbSet<TEntity> dbSet;

        public GenericRepository(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual List<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int? take = null, int? skip = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = dbSet;

            foreach (Expression<Func<TEntity, object>> include in includes)
                query = query.Include(include);

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            if (skip != null)
                query = query.Skip(skip.Value);

            if (take != null)
                query = query.Take(take.Value);
            return query.ToList();


        }
        public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int? take = null, int? skip = null, params Expression<Func<TEntity, object>>[] includes)
        {

            IQueryable<TEntity> query = dbSet;
            foreach (Expression<Func<TEntity, object>> include in includes)
                query = query.Include(include);
            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            if (skip != null)
                query = query.Skip(skip.Value);

            if (take != null)
                query = query.Take(take.Value);

            return query;

        }
        public virtual IQueryable<TEntity> QueryExtended(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int? take = null, int? skip = null, string orderByS = null, short? orderDirection = null, params Expression<Func<TEntity, object>>[] includes)
        {

            IQueryable<TEntity> query = dbSet;
            foreach (Expression<Func<TEntity, object>> include in includes)
                query = query.Include(include);
            if (filter != null)
                query = query.Where(filter);


            if (orderByS == null)
                orderByS = "OperationDate";
            if (orderDirection == null)
                orderDirection = 2;

            switch (orderDirection)
            {
                case 1:
                    query = LinqExtensions.OrderBy<TEntity>(query, orderByS);
                    break;
                case 2:
                    query = LinqExtensions.OrderByDescending<TEntity>(query, orderByS);
                    break;
                default:
                    break;
            }


            if (orderBy != null)
                query = orderBy(query);

            if (skip != null)
                query = query.Skip(skip.Value);

            if (take != null)
                query = query.Take(take.Value);

            return query;
        }

        public virtual List<TEntity> GetExtended(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int? take = null, int? skip = null, string orderByS = null, short? orderDirection = null, params Expression<Func<TEntity, object>>[] includes)
        {

            IQueryable<TEntity> query = dbSet;

            foreach (Expression<Func<TEntity, object>> include in includes)
                query = query.Include(include);

            if (filter != null)
                query = query.Where(filter);

            if (orderByS == null)
                orderByS = "OperationDate";
            if (orderDirection == null)
                orderDirection = 2;
            switch (orderDirection)
            {
                case 1:
                    query = LinqExtensions.OrderBy<TEntity>(query, orderByS);
                    break;
                case 2:
                    query = LinqExtensions.OrderByDescending<TEntity>(query, orderByS);
                    break;
                default:
                    break;
            }

            if (orderBy != null)
                query = orderBy(query);


            if (skip != null)
                query = query.Skip(skip.Value);

            if (take != null)
                query = query.Take(take.Value);
            return query.ToList();

        }

        public virtual long GetCount(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes)
        {

            IQueryable<TEntity> query = dbSet;

            foreach (Expression<Func<TEntity, object>> include in includes)
                query = query.Include(include);

            if (filter != null)
                query = query.Where(filter);

            return query.Count();

        }

        public virtual bool Any(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = dbSet;
            var result = false;

            foreach (Expression<Func<TEntity, object>> include in includes)
                query = query.Include(include);

            if (filter != null)
                result = query.Any(filter);

            return result;
        }

        public virtual TEntity GetById(object id)
        {
            return dbSet.Find(id);
        }

        public virtual TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = dbSet;

            foreach (Expression<Func<TEntity, object>> include in includes)
                query = query.Include(include);

            return query.FirstOrDefault(filter);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void BulkInsert(List<TEntity> entities)
        {
            context.Configuration.AutoDetectChangesEnabled = false;
            dbSet.AddRange(entities);
        }

        public virtual void InsertNoDetect(TEntity entity)
        {
            context.Configuration.AutoDetectChangesEnabled = false;
            dbSet.Add(entity);
        }

        public virtual void Update(TEntity entity, HttpRequestInfo reqInfo)
        {
            CDCLogging(entity, reqInfo, (short)_Enumeration._TypeCDCOperation.Update, (short)_Enumeration._TypeOperationIsDeleted.UnDeleted);
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(object id, HttpRequestInfo reqInfo)
        {
            TEntity entityToDelete = dbSet.Find(id);
            CDCLogging(entityToDelete, reqInfo, (short)_Enumeration._TypeCDCOperation.Delete, (short)_Enumeration._TypeOperationIsDeleted.Deleted);
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }
        public virtual void DeleteWithoutLogging(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }
        public void DetachAllEntities()
        {
            foreach (var entity in context.ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted))
            {
                context.Entry(entity.Entity).State = EntityState.Detached;
            }
        }
        private bool CDCLogging(TEntity entity, HttpRequestInfo reqInfo, short type, short isDeleted)
        {
            try
            {
                var key = EntityHelper.GetEntityKey<TEntity>(context, entity);
                var oldEntity = dbSet.Find(key.EntityKeyValues.FirstOrDefault().Value);
                var data = new RouteValueDictionary(oldEntity);
                var xml = new XElement(
                        "items",
                        data.Where(x => x.Value != null).Select(x => new XElement("item", new XAttribute("id", x.Key), new XAttribute("value", x.Value)))
                     ).ToString();
                CDC cdc = new CDC();
                cdc.OperationIdUserRef = reqInfo.UserID;
                cdc.OperationDate = DateTime.Now;
                cdc.OperationIP = reqInfo.IpAddress;
                cdc.OperationIsDeleted = isDeleted;
                cdc.OperationType = type;
                cdc.TableName = key.EntitySetName;
                cdc.Source = xml;
                long keyLong = -1;
                Guid keyGuid;
                long.TryParse(key.EntityKeyValues.FirstOrDefault().Value.ToString(), out keyLong);
                Guid.TryParse(key.EntityKeyValues.FirstOrDefault().Value.ToString(), out keyGuid);
                if (keyLong != -1)
                    cdc.TableId = keyLong;
                cdc.TableIdGuid = keyGuid;
                cdc.TablePrimaryKeyName = key.EntityKeyValues.FirstOrDefault().Key;
                var cdcContext = new DerinEntities();
                var cdcRec = new GenericRepository<CDC>(cdcContext);
                var cdcUnitOfWork = new UnitOfWork.Derin.UnitOfWork(cdcContext);
                cdcRec.Insert(cdc);
                cdcUnitOfWork.Save();
                cdcUnitOfWork.Dispose();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }

        }


    }
    public static class LinqExtensions
    {
        private static PropertyInfo GetPropertyInfo<T>(Type objType, string name, IEnumerable<T> query)
        {
            var properties = objType.GetProperties();
            var matchedProperty = properties.FirstOrDefault(p => p.Name == name);
            if (matchedProperty == null)
                throw new ArgumentException("name");

            return matchedProperty;
        }
        private static object GetPropertyValue(object obj, string propertyName)
        {
            var objType = obj.GetType();
            var prop = objType.GetProperty(propertyName);

            return prop.GetValue(obj, null);
        }
        private static PropertyInfo GetProperty(object obj, string propertyName)
        {
            var objType = obj.GetType();
            var prop = objType.GetProperty(propertyName);

            return prop;
        }
        private static LambdaExpression GetOrderExpression(Type objType, PropertyInfo pi)
        {
            var paramExpr = Expression.Parameter(objType);
            var propAccess = Expression.PropertyOrField(paramExpr, pi.Name);
            var expr = Expression.Lambda(propAccess, paramExpr);
            return expr;
        }

        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> query, string name)
        {
            string[] names = name.Split('.');
            if (names.Count() < 2)
            {
                var propInfo = GetPropertyInfo(typeof(T), name, query);
                var expr = GetOrderExpression(typeof(T), propInfo);

                var method = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == "OrderBy" && m.GetParameters().Length == 2);
                var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);
                return (IEnumerable<T>)genericMethod.Invoke(null, new object[] { query, expr });
            }
            var paramExpr = Expression.Parameter(typeof(T));
            var propAccess = Expression.PropertyOrField(paramExpr, names[0]);
            var pA2 = Expression.PropertyOrField(propAccess, names[1]);
            var exprL = Expression.Lambda(pA2, paramExpr);
            var method2 = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == "OrderBy" && m.GetParameters().Length == 2);
            var genericMethod2 = method2.MakeGenericMethod(typeof(T), ((PropertyInfo)pA2.Member).PropertyType);
            return (IEnumerable<T>)genericMethod2.Invoke(null, new object[] { query, exprL });
        }
        public static IEnumerable<T> OrderByDescending<T>(this IEnumerable<T> query, string name)
        {
            string[] names = name.Split('.');
            if (names.Count() < 2)
            {
                var propInfo = GetPropertyInfo(typeof(T), name, query);
                var expr = GetOrderExpression(typeof(T), propInfo);

                var method = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == "OrderByDescending" && m.GetParameters().Length == 2);
                var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);
                return (IEnumerable<T>)genericMethod.Invoke(null, new object[] { query, expr });
            }
            var paramExpr = Expression.Parameter(typeof(T));
            var propAccess = Expression.PropertyOrField(paramExpr, names[0]);
            var pA2 = Expression.PropertyOrField(propAccess, names[1]);
            var exprL = Expression.Lambda(pA2, paramExpr);
            var method2 = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == "OrderByDescending" && m.GetParameters().Length == 2);
            var genericMethod2 = method2.MakeGenericMethod(typeof(T), ((PropertyInfo)pA2.Member).PropertyType);
            return (IEnumerable<T>)genericMethod2.Invoke(null, new object[] { query, exprL });
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string name)
        {

            string[] names = name.Split('.');
            if (names.Count() < 2)
            {
                var propInfo = GetPropertyInfo(typeof(T), name, query);
                var expr = GetOrderExpression(typeof(T), propInfo);

                var method = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == "OrderBy" && m.GetParameters().Length == 2);
                var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);
                return (IQueryable<T>)genericMethod.Invoke(null, new object[] { query, expr });
            }
            var paramExpr = Expression.Parameter(typeof(T));
            var propAccess = Expression.PropertyOrField(paramExpr, names[0]);
            var pA2 = Expression.PropertyOrField(propAccess, names[1]);
            var exprL = Expression.Lambda(pA2, paramExpr);
            var method2 = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == "OrderBy" && m.GetParameters().Length == 2);
            var genericMethod2 = method2.MakeGenericMethod(typeof(T), ((PropertyInfo)pA2.Member).PropertyType);
            return (IQueryable<T>)genericMethod2.Invoke(null, new object[] { query, exprL });
        }
        public static IQueryable<T> OrderByDescending<T>(this IQueryable<T> query, string name)
        {
            string[] names = name.Split('.');
            if (names.Count() < 2)
            {
                var propInfo = GetPropertyInfo(typeof(T), name, query);
                var expr = GetOrderExpression(typeof(T), propInfo);

                var method = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == "OrderByDescending" && m.GetParameters().Length == 2);
                var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);
                return (IQueryable<T>)genericMethod.Invoke(null, new object[] { query, expr });
            }
            var paramExpr = Expression.Parameter(typeof(T));
            var propAccess = Expression.PropertyOrField(paramExpr, names[0]);
            var pA2 = Expression.PropertyOrField(propAccess, names[1]);
            var exprL = Expression.Lambda(pA2, paramExpr);
            var method2 = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == "OrderByDescending" && m.GetParameters().Length == 2);
            var genericMethod2 = method2.MakeGenericMethod(typeof(T), ((PropertyInfo)pA2.Member).PropertyType);
            return (IQueryable<T>)genericMethod2.Invoke(null, new object[] { query, exprL });


        }
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
    (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

    }
}
