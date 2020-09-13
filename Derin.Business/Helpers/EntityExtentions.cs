using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;


namespace Derin.Business.Helpers
{
    public static class EntityExtentions
    {
        public static IQueryable<TEntity> GetActive<TEntity>(this IEnumerable<TEntity> queryable) where TEntity : class => GetActive(queryable.AsQueryable());
        public static IQueryable<TEntity> GetActive<TEntity>(this IQueryable<TEntity> queryable) where TEntity : class
        {
            return queryable.Where(Active<TEntity>());
        }

        public static string ToTraceQuery<T>(this IQueryable<T> query)
        {
            ObjectQuery<T> objectQuery = GetQueryFromQueryable(query);

            var result = objectQuery.ToTraceString();
            foreach (var parameter in objectQuery.Parameters)
            {
                var name = "@" + parameter.Name;
                var value = "'" + parameter.Value.ToString() + "'";
                result = result.Replace(name, value);
            }

            return result;
        }

        /// <summary>
        /// For an Entity Framework IQueryable, returns the SQL and Parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public static string ToTraceString<T>(this IQueryable<T> query)
        {
            ObjectQuery<T> objectQuery = GetQueryFromQueryable(query);

            var traceString = new StringBuilder();

            traceString.AppendLine(objectQuery.ToTraceString());
            traceString.AppendLine();

            foreach (var parameter in objectQuery.Parameters)
            {
                traceString.AppendLine(parameter.Name + " [" + parameter.ParameterType.FullName + "] = " + parameter.Value);
            }

            return traceString.ToString();
        }

        private static System.Data.Entity.Core.Objects.ObjectQuery<T> GetQueryFromQueryable<T>(IQueryable<T> query)
        {
            var internalQueryField = query.GetType().GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Where(f => f.Name.Equals("_internalQuery")).FirstOrDefault();
            var internalQuery = internalQueryField.GetValue(query);
            var objectQueryField = internalQuery.GetType().GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Where(f => f.Name.Equals("_objectQuery")).FirstOrDefault();
            return objectQueryField.GetValue(internalQuery) as System.Data.Entity.Core.Objects.ObjectQuery<T>;
        }

        public static Expression<Func<TEntity, bool>> Active<TEntity>() where TEntity : class
        {
            var type = typeof(TEntity);

            var property = type.GetProperty("OperationIsDeleted") ??
                           throw new ArgumentNullException(
                               $"{nameof(TEntity)} has no property named: \"OperationIsDeleted\"");

            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var constantValue = Expression.Constant((short)1, typeof(short));

            var equality = Expression.Equal(propertyAccess, constantValue);

            return Expression.Lambda<Func<TEntity, bool>>(equality, parameter);
        }
    }
}
