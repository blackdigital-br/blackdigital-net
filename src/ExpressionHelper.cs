
using System.Linq.Expressions;
using System.Reflection;

namespace BlackDigital
{
    public static class ExpressionHelper
    {
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
