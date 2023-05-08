
using System.Linq.Expressions;

namespace BlackDigital.Model
{
    public static class BaseFilterFilter
    {
        public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> query, BaseFilter filter)
        {
            return query
                .ApplyOnlyOrderBy(filter)
                .ApplyOnlyFilter(filter);
        }

        public static IQueryable<T> ApplyOnlyFilter<T>(this IQueryable<T> query, BaseFilter filter)
        {
            if (filter.Skip.HasValue)
                query = query.Skip(filter.Skip.Value);

            if (filter.Take.HasValue)
                query = query.Take(filter.Take.Value);

            return query;
        }

        public static IQueryable<T> ApplyOnlyOrderBy<T>(this IQueryable<T> query, BaseFilter filter)
        {
            if (filter.Sort == null || filter.Sort.Count <= 0)
                return query;

            ParameterExpression parameter = Expression.Parameter(typeof(T), "");
            var first = true;

            foreach (var orderBy in filter.Sort)
            {
                MemberExpression property = Expression.Property(parameter, orderBy.Name);
                LambdaExpression lambda = Expression.Lambda(property, parameter);

                string methodName = orderBy.Asc ? "OrderBy" : "OrderByDescending";

                if (!first)
                    methodName = orderBy.Asc ? "ThenBy" : "ThenByDescending";

                Expression methodCallExpression = Expression.Call(typeof(Queryable), methodName,
                                      new Type[] { query.ElementType, property.Type },
                                      query.Expression, Expression.Quote(lambda));

                query = query.Provider.CreateQuery<T>(methodCallExpression);
                first = false;
            }

            return query;
        }
    }
}
