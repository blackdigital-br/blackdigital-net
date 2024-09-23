using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace BlackDigital
{
    public static class DisplayHelper
    {
        #region "GetDisplay"

        public static DisplayAttribute? GetDisplay(this PropertyInfo property)
            => property.GetSingleAttribute<DisplayAttribute>();

        public static DisplayAttribute? GetDisplay(this MemberExpression memberExpression)
            => ReflectionHelper.GetSingleAttribute<DisplayAttribute>(memberExpression);

        public static DisplayAttribute? GetDisplay<T>(Expression<Func<T>> expression)
            => GetDisplay((MemberExpression)expression.Body);

        #endregion "GetDisplay"

        #region "GetDisplayData"

        public static string? GetDisplayName(this DisplayAttribute? attribute)
            => ObjectHelper.GetResourceValue(attribute?.Name, attribute?.ResourceType);

        public static string? GetShortName(this DisplayAttribute? attribute)
            => ObjectHelper.GetResourceValue(attribute?.ShortName, attribute?.ResourceType);

        public static string? GetDescription(this DisplayAttribute? attribute)
            => ObjectHelper.GetResourceValue(attribute?.Description, attribute?.ResourceType);

        public static string? GetPrompt(this DisplayAttribute? attribute)
            => ObjectHelper.GetResourceValue(attribute?.Prompt, attribute?.ResourceType);

        public static string? GetGroupName(this DisplayAttribute? attribute)
            => ObjectHelper.GetResourceValue(attribute?.GroupName, attribute?.ResourceType);

        #endregion "GetDisplayData"

        #region "Expression"

        public static string? GetDisplayName<T>(Expression<Func<T>> expression)
            => GetDisplayName(GetDisplay(expression));

        public static string? GetShortName<T>(Expression<Func<T>> expression)
            => GetShortName(GetDisplay(expression));

        public static string? GetDescription<T>(Expression<Func<T>> expression)
            => GetDescription(GetDisplay(expression));

        public static string? GetPrompt<T>(Expression<Func<T>> expression)
            => GetPrompt(GetDisplay(expression));

        public static string? GetGroupName<T>(Expression<Func<T>> expression)
            => GetGroupName(GetDisplay(expression));

        public static int GetOrder<T>(Expression<Func<T>> expression)
            => GetDisplay(expression)?.GetOrder() ?? int.MaxValue;

        public static bool GetAutoGenerateField<T>(Expression<Func<T>> expression)
            => GetDisplay(expression)?.GetAutoGenerateField() ?? false;

        public static bool GetAutoGenerateFilter<T>(Expression<Func<T>> expression)
            => GetDisplay(expression)?.GetAutoGenerateFilter() ?? false;

        #endregion "Expression"
    }
}
