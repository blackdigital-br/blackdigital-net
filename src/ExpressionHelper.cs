
using System.Linq.Expressions;
using System.Reflection;

namespace BlackDigital
{
    /// <summary>
    /// Provides helper methods for building dynamic LINQ query expressions.
    /// </summary>
    public static class ExpressionHelper
    {
        /// <summary>
        /// Filters a query by applying an equality predicate on the specified property.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="query">The query to filter.</param>
        /// <param name="property">An expression selecting the property to filter on.</param>
        /// <param name="value">The value to compare against.</param>
        /// <returns>The filtered query.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="property"/> does not select a property.</exception>
        public static IQueryable<TEntity> Filter<TEntity, TProperty>(IQueryable<TEntity> query,
                                                                      Expression<Func<TEntity, TProperty>> property,
                                                                      TProperty value)
        {
            if (property.Body is not MemberExpression memberExpression || !(memberExpression.Member is PropertyInfo))
                throw new ArgumentException("Property expected", "property");

            Expression left = property.Body;
            Expression right = Expression.Constant(value, typeof(TProperty));

            Expression searchExpression = Expression.Equal(left, right);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(searchExpression,
                                                                new ParameterExpression[] { property.Parameters.Single() });

            return query.Where(lambda);
        }
    }
}
