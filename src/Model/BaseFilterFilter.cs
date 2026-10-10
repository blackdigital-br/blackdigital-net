
using System.Linq.Expressions;

namespace BlackDigital.Model
{
    /// <summary>
    /// Provides extension methods for applying <see cref="BaseFilter"/> parameters to <see cref="IQueryable{T}"/> queries.
    /// </summary>
    public static class BaseFilterFilter
    {
        /// <summary>
        /// Applies both ordering and pagination from a <see cref="BaseFilter"/> to a query.
        /// </summary>
        /// <typeparam name="T">The type of the query elements.</typeparam>
        /// <param name="query">The query to filter.</param>
        /// <param name="filter">The filter to apply.</param>
        /// <returns>The filtered query.</returns>
        public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> query, BaseFilter filter)
        {
            return query
                .ApplyOnlyOrderBy(filter)
                .ApplyOnlyFilter(filter);
        }

        /// <summary>
        /// Applies only the pagination (Skip/Take) from a <see cref="BaseFilter"/> to a query.
        /// </summary>
        /// <typeparam name="T">The type of the query elements.</typeparam>
        /// <param name="query">The query to filter.</param>
        /// <param name="filter">The filter to apply.</param>
        /// <returns>The filtered query.</returns>
        public static IQueryable<T> ApplyOnlyFilter<T>(this IQueryable<T> query, BaseFilter filter)
        {
            if (filter.Skip.HasValue)
                query = query.Skip(filter.Skip.Value);

            if (filter.Take.HasValue)
                query = query.Take(filter.Take.Value);

            return query;
        }

        /// <summary>
        /// Applies only the ordering from a <see cref="BaseFilter"/> to a query.
        /// </summary>
        /// <typeparam name="T">The type of the query elements.</typeparam>
        /// <param name="query">The query to filter.</param>
        /// <param name="filter">The filter to apply.</param>
        /// <returns>The ordered query.</returns>
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

                string methodName = orderBy.SortAscending != false ? "OrderBy" : "OrderByDescending";

                if (!first)
                    methodName = orderBy.SortAscending != false ? "ThenBy" : "ThenByDescending";

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
