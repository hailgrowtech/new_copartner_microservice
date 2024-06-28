using System;
using System.Linq;
using System.Linq.Expressions;

namespace CommonLibrary.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> OrderByField<T>(this IQueryable<T> source, string fieldName)
        {
            var parameter = Expression.Parameter(typeof(T), "e");
            var property = Expression.Property(parameter, fieldName);
            var lambda = Expression.Lambda(property, parameter);
            var methodName = "OrderBy";
            var method = typeof(Queryable).GetMethods()
                                          .Where(m => m.Name == methodName && m.IsGenericMethodDefinition)
                                          .Where(m => m.GetParameters().Length == 2)
                                          .Single();

            var genericMethod = method.MakeGenericMethod(typeof(T), property.Type);
            var newQuery = (IQueryable<T>)genericMethod.Invoke(null, new object[] { source, lambda });
            return newQuery;
        }
    }
}
