
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

        public static IEnumerable<PropertyInfo> GetPropertiesInfoFromExpression<TModel>(this Expression<Func<TModel, object>>? expression)
        {
            if (expression == null)
                yield break;

            if (expression.Body is NewExpression newExpr)
            {
                foreach (var arg in newExpr.Arguments)
                {
                    if (arg is MemberExpression memberExpr && memberExpr.Member is PropertyInfo propInfo)
                        yield return propInfo;
                    else
                        throw new InvalidOperationException("All arguments in the expression must be properties.");
                }
            }
            else if (expression.Body is MemberExpression memberExprSingle && memberExprSingle.Member is PropertyInfo propInfoSingle)
            {
                yield return propInfoSingle;
            }
            else if (expression.Body is UnaryExpression unaryExpr && unaryExpr.Operand is MemberExpression memberExprUnary && memberExprUnary.Member is PropertyInfo propInfoUnary)
            {
                yield return propInfoUnary;
            }
            else
            {
                throw new InvalidOperationException("IgnoreProperties expression must be a NewExpression.");
            }
        }
    }
}
