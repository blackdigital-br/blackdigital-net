using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace BlackDigital
{
    /// <summary>
    /// Provides helper methods for retrieving display metadata from <see cref="DisplayAttribute"/> instances.
    /// </summary>
    public static class DisplayHelper
    {
        #region "GetDisplay"

        /// <summary>
        /// Gets the <see cref="DisplayAttribute"/> applied to a property.
        /// </summary>
        /// <param name="property">The property to inspect.</param>
        /// <returns>The <see cref="DisplayAttribute"/>, or <c>null</c> if not present.</returns>
        public static DisplayAttribute? GetDisplay(this PropertyInfo property)
            => property.GetSingleAttribute<DisplayAttribute>();

        /// <summary>
        /// Gets the <see cref="DisplayAttribute"/> applied to the member of a member expression.
        /// </summary>
        /// <param name="memberExpression">The member expression to inspect.</param>
        /// <returns>The <see cref="DisplayAttribute"/>, or <c>null</c> if not present.</returns>
        public static DisplayAttribute? GetDisplay(this MemberExpression memberExpression)
            => ReflectionHelper.GetSingleAttribute<DisplayAttribute>(memberExpression);

        /// <summary>
        /// Gets the <see cref="DisplayAttribute"/> from an expression selecting a member.
        /// </summary>
        /// <typeparam name="T">The type of the member.</typeparam>
        /// <param name="expression">The expression selecting the member.</param>
        /// <returns>The <see cref="DisplayAttribute"/>, or <c>null</c> if not present.</returns>
        public static DisplayAttribute? GetDisplay<T>(Expression<Func<T>> expression)
            => GetDisplay((MemberExpression)expression.Body);

        #endregion "GetDisplay"

        #region "GetDisplayData"

        /// <summary>
        /// Gets the display name from a <see cref="DisplayAttribute"/>, resolving resource values.
        /// </summary>
        /// <param name="attribute">The display attribute.</param>
        /// <returns>The display name, or <c>null</c>.</returns>
        public static string? GetDisplayName(this DisplayAttribute? attribute)
            => ObjectHelper.GetResourceValue(attribute?.Name, attribute?.ResourceType);

        /// <summary>
        /// Gets the short name from a <see cref="DisplayAttribute"/>, resolving resource values.
        /// </summary>
        /// <param name="attribute">The display attribute.</param>
        /// <returns>The short name, or <c>null</c>.</returns>
        public static string? GetShortName(this DisplayAttribute? attribute)
            => ObjectHelper.GetResourceValue(attribute?.ShortName, attribute?.ResourceType);

        /// <summary>
        /// Gets the description from a <see cref="DisplayAttribute"/>, resolving resource values.
        /// </summary>
        /// <param name="attribute">The display attribute.</param>
        /// <returns>The description, or <c>null</c>.</returns>
        public static string? GetDescription(this DisplayAttribute? attribute)
            => ObjectHelper.GetResourceValue(attribute?.Description, attribute?.ResourceType);

        /// <summary>
        /// Gets the prompt from a <see cref="DisplayAttribute"/>, resolving resource values.
        /// </summary>
        /// <param name="attribute">The display attribute.</param>
        /// <returns>The prompt, or <c>null</c>.</returns>
        public static string? GetPrompt(this DisplayAttribute? attribute)
            => ObjectHelper.GetResourceValue(attribute?.Prompt, attribute?.ResourceType);

        /// <summary>
        /// Gets the group name from a <see cref="DisplayAttribute"/>, resolving resource values.
        /// </summary>
        /// <param name="attribute">The display attribute.</param>
        /// <returns>The group name, or <c>null</c>.</returns>
        public static string? GetGroupName(this DisplayAttribute? attribute)
            => ObjectHelper.GetResourceValue(attribute?.GroupName, attribute?.ResourceType);

        #endregion "GetDisplayData"

        #region "Expression"

        /// <summary>
        /// Gets the display name from the <see cref="DisplayAttribute"/> of the member selected by the expression.
        /// </summary>
        /// <typeparam name="T">The type of the member.</typeparam>
        /// <param name="expression">The expression selecting the member.</param>
        /// <returns>The display name, or <c>null</c>.</returns>
        public static string? GetDisplayName<T>(Expression<Func<T>> expression)
            => GetDisplayName(GetDisplay(expression));

        /// <summary>
        /// Gets the short name from the <see cref="DisplayAttribute"/> of the member selected by the expression.
        /// </summary>
        /// <typeparam name="T">The type of the member.</typeparam>
        /// <param name="expression">The expression selecting the member.</param>
        /// <returns>The short name, or <c>null</c>.</returns>
        public static string? GetShortName<T>(Expression<Func<T>> expression)
            => GetShortName(GetDisplay(expression));

        /// <summary>
        /// Gets the description from the <see cref="DisplayAttribute"/> of the member selected by the expression.
        /// </summary>
        /// <typeparam name="T">The type of the member.</typeparam>
        /// <param name="expression">The expression selecting the member.</param>
        /// <returns>The description, or <c>null</c>.</returns>
        public static string? GetDescription<T>(Expression<Func<T>> expression)
            => GetDescription(GetDisplay(expression));

        /// <summary>
        /// Gets the prompt from the <see cref="DisplayAttribute"/> of the member selected by the expression.
        /// </summary>
        /// <typeparam name="T">The type of the member.</typeparam>
        /// <param name="expression">The expression selecting the member.</param>
        /// <returns>The prompt, or <c>null</c>.</returns>
        public static string? GetPrompt<T>(Expression<Func<T>> expression)
            => GetPrompt(GetDisplay(expression));

        /// <summary>
        /// Gets the group name from the <see cref="DisplayAttribute"/> of the member selected by the expression.
        /// </summary>
        /// <typeparam name="T">The type of the member.</typeparam>
        /// <param name="expression">The expression selecting the member.</param>
        /// <returns>The group name, or <c>null</c>.</returns>
        public static string? GetGroupName<T>(Expression<Func<T>> expression)
            => GetGroupName(GetDisplay(expression));

        /// <summary>
        /// Gets the order from the <see cref="DisplayAttribute"/> of the member selected by the expression.
        /// </summary>
        /// <typeparam name="T">The type of the member.</typeparam>
        /// <param name="expression">The expression selecting the member.</param>
        /// <returns>The order value, or <see cref="int.MaxValue"/> if not set.</returns>
        public static int GetOrder<T>(Expression<Func<T>> expression)
            => GetDisplay(expression)?.GetOrder() ?? int.MaxValue;

        /// <summary>
        /// Gets whether the field should be auto-generated from the <see cref="DisplayAttribute"/> of the member selected by the expression.
        /// </summary>
        /// <typeparam name="T">The type of the member.</typeparam>
        /// <param name="expression">The expression selecting the member.</param>
        /// <returns><c>true</c> if the field should be auto-generated; otherwise, <c>false</c>.</returns>
        public static bool GetAutoGenerateField<T>(Expression<Func<T>> expression)
            => GetDisplay(expression)?.GetAutoGenerateField() ?? false;

        /// <summary>
        /// Gets whether the filter should be auto-generated from the <see cref="DisplayAttribute"/> of the member selected by the expression.
        /// </summary>
        /// <typeparam name="T">The type of the member.</typeparam>
        /// <param name="expression">The expression selecting the member.</param>
        /// <returns><c>true</c> if the filter should be auto-generated; otherwise, <c>false</c>.</returns>
        public static bool GetAutoGenerateFilter<T>(Expression<Func<T>> expression)
            => GetDisplay(expression)?.GetAutoGenerateFilter() ?? false;

        #endregion "Expression"
    }
}
