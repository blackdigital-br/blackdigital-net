
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace BlackDigital
{
    /// <summary>
    /// Provides helper methods for retrieving custom attributes from reflection members and for building and applying custom attributes dynamically.
    /// </summary>
    public static class ReflectionHelper
    {
        /*public static TAttribute[] GetAttributes<TAttribute>(this Assembly assembly)
            where TAttribute : Attribute =>
                assembly.GetCustomAttributes(typeof(TAttribute), true)
                        .Cast<TAttribute>()
                        .ToArray();

        public static TAttribute? GetSingleAttribute<TAttribute>(this Assembly assembly)
            where TAttribute : Attribute =>
                assembly.GetCustomAttributes(typeof(TAttribute), true)
                        .Cast<TAttribute>()
                        .SingleOrDefault();*/

        #region "Get Attributes"

        /// <summary>
        /// Gets all attributes of the specified type applied to a type.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute to retrieve.</typeparam>
        /// <param name="type">The type to inspect.</param>
        /// <returns>An array of attributes of type <typeparamref name="TAttribute"/>.</returns>
        public static TAttribute[] GetAttributes<TAttribute>(this Type type)
            where TAttribute : Attribute =>
                type.GetCustomAttributes(typeof(TAttribute), true)
                        .Cast<TAttribute>()
                        .ToArray();


        /// <summary>
        /// Gets the single attribute of the specified type applied to a type.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute to retrieve.</typeparam>
        /// <param name="type">The type to inspect.</param>
        /// <returns>The attribute, or <c>null</c> if not found.</returns>
        public static TAttribute? GetSingleAttribute<TAttribute>(this Type type)
            where TAttribute : Attribute =>
                type.GetCustomAttributes(typeof(TAttribute), true)
                        .Cast<TAttribute>()
                        .SingleOrDefault();

        /// <summary>
        /// Gets all attributes of the specified type applied to a method.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute to retrieve.</typeparam>
        /// <param name="method">The method to inspect.</param>
        /// <returns>An array of attributes of type <typeparamref name="TAttribute"/>.</returns>
        public static TAttribute[] GetAttributes<TAttribute>(this MethodInfo method)
            where TAttribute : Attribute =>
                method.GetCustomAttributes(typeof(TAttribute), true)
                        .Cast<TAttribute>()
                        .ToArray();


        /// <summary>
        /// Gets the single attribute of the specified type applied to a method.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute to retrieve.</typeparam>
        /// <param name="method">The method to inspect.</param>
        /// <returns>The attribute, or <c>null</c> if not found.</returns>
        public static TAttribute? GetSingleAttribute<TAttribute>(this MethodInfo method)
            where TAttribute : Attribute =>
                method.GetCustomAttributes(typeof(TAttribute), true)
                        .Cast<TAttribute>()
                        .SingleOrDefault();

        /// <summary>
        /// Gets all attributes of the specified type applied to a property.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute to retrieve.</typeparam>
        /// <param name="property">The property to inspect.</param>
        /// <returns>An array of attributes of type <typeparamref name="TAttribute"/>.</returns>
        public static TAttribute[] GetAttributes<TAttribute>(this PropertyInfo property)
            where TAttribute : Attribute =>
                property.GetCustomAttributes(typeof(TAttribute), true)
                        .Cast<TAttribute>()
                        .ToArray();


        /// <summary>
        /// Gets the single attribute of the specified type applied to a property.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute to retrieve.</typeparam>
        /// <param name="property">The property to inspect.</param>
        /// <returns>The attribute, or <c>null</c> if not found.</returns>
        public static TAttribute? GetSingleAttribute<TAttribute>(this PropertyInfo property)
            where TAttribute : Attribute =>
                property.GetCustomAttributes(typeof(TAttribute), true)
                        .Cast<TAttribute>()
                        .SingleOrDefault();

        /// <summary>
        /// Gets all attributes of the specified type applied to a member.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute to retrieve.</typeparam>
        /// <param name="member">The member to inspect.</param>
        /// <returns>An array of attributes of type <typeparamref name="TAttribute"/>.</returns>
        public static TAttribute[] GetAttributes<TAttribute>(this MemberInfo member)
            where TAttribute : Attribute =>
                member.GetCustomAttributes(typeof(TAttribute), true)
                        .Cast<TAttribute>()
                        .ToArray();


        /// <summary>
        /// Gets the single attribute of the specified type applied to a member.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute to retrieve.</typeparam>
        /// <param name="member">The member to inspect.</param>
        /// <returns>The attribute, or <c>null</c> if not found.</returns>
        public static TAttribute? GetSingleAttribute<TAttribute>(this MemberInfo member)
            where TAttribute : Attribute =>
                member.GetCustomAttributes(typeof(TAttribute), true)
                        .Cast<TAttribute>()
                        .SingleOrDefault();

        /// <summary>
        /// Gets the single attribute of the specified type from a member expression, also checking the associated metadata type.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute to retrieve.</typeparam>
        /// <param name="memberExpression">The member expression to inspect.</param>
        /// <returns>The attribute, or <c>null</c> if not found.</returns>
        public static TAttribute? GetSingleAttribute<TAttribute>(this MemberExpression memberExpression)
            where TAttribute : Attribute
        {
            var value = memberExpression.Member.GetCustomAttribute(typeof(TAttribute), true) as TAttribute;

            if (value == null)
            {
                var declaringType = memberExpression.Member.DeclaringType;

                if (declaringType == null)
                    return null;

                var metaData = declaringType.GetCustomAttribute(typeof(MetadataTypeAttribute), true) as MetadataTypeAttribute;
                if (metaData != null)
                {
                    var mdProp = metaData.MetadataClassType.GetProperties()
                             .ToList()
                             .Where(p => p.Name == memberExpression.Member.Name)
                             .FirstOrDefault();
                    if (mdProp != null)
                    {
                        value = mdProp.GetCustomAttribute(typeof(TAttribute)) as TAttribute;
                    }
                }
            }

            return value;
        }

        /// <summary>
        /// Gets all attributes of the specified type applied to a parameter.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute to retrieve.</typeparam>
        /// <param name="parameter">The parameter to inspect.</param>
        /// <returns>An array of attributes of type <typeparamref name="TAttribute"/>.</returns>
        public static TAttribute[] GetAttributes<TAttribute>(this ParameterInfo parameter)
            where TAttribute : Attribute =>
                parameter.GetCustomAttributes(typeof(TAttribute), true)
                        .Cast<TAttribute>()
                        .ToArray();

        /// <summary>
        /// Gets the single attribute of the specified type applied to a parameter.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute to retrieve.</typeparam>
        /// <param name="parameter">The parameter to inspect.</param>
        /// <returns>The attribute, or <c>null</c> if not found.</returns>
        public static TAttribute? GetSingleAttribute<TAttribute>(this ParameterInfo parameter)
            where TAttribute : Attribute =>
                parameter.GetCustomAttributes(typeof(TAttribute), true)
                        .Cast<TAttribute>()
                        .SingleOrDefault();

        #endregion "Get Attributes"

        #region "Get Data From Attributes"

        

        /// <summary>
        /// Gets the display name of a property from its <see cref="DisplayAttribute"/>, or the property name if the attribute is not present.
        /// </summary>
        /// <param name="property">The property to inspect.</param>
        /// <returns>The display name, or the property name.</returns>
        public static string GetDisplayName(this PropertyInfo property)
            => property.GetDisplay()?.Name ?? property.Name;

        #endregion "Get Data From Attributes"

        #region "Create Attributes"

        /// <summary>
        /// Creates a <see cref="CustomAttributeBuilder"/> for the specified attribute type.
        /// </summary>
        /// <param name="attributeType">The type of attribute to build.</param>
        /// <param name="argsTypes">The types of the constructor arguments.</param>
        /// <param name="argsValues">The values of the constructor arguments.</param>
        /// <param name="propertyInfos">The properties to set on the attribute.</param>
        /// <param name="provertyValues">The values for the properties.</param>
        /// <returns>A <see cref="CustomAttributeBuilder"/>, or <c>null</c> if the constructor cannot be found.</returns>
        public static CustomAttributeBuilder? CreateCustomAttribute(Type attributeType,
                                                                    IEnumerable<Type>? argsTypes = null,
                                                                    IEnumerable<object>? argsValues = null,
                                                                    IEnumerable<PropertyInfo>? propertyInfos = null,
                                                                    IEnumerable<object>? provertyValues = null)
        {
            argsTypes ??= Array.Empty<Type>();
            argsValues ??= Array.Empty<object>();
            var constructor = attributeType.GetConstructor(argsTypes.ToArray());

            if (constructor == null)
                return null;

            return new CustomAttributeBuilder(constructor,
                                              argsValues.ToArray(),
                                              propertyInfos?.ToArray() ?? Array.Empty<PropertyInfo>(),
                                              provertyValues?.ToArray() ?? Array.Empty<object>());
        }

        /// <summary>
        /// Creates a <see cref="CustomAttributeBuilder"/> for the specified attribute type.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute to build.</typeparam>
        /// <param name="argsTypes">The types of the constructor arguments.</param>
        /// <param name="argsValues">The values of the constructor arguments.</param>
        /// <param name="propertyInfos">The properties to set on the attribute.</param>
        /// <param name="propertyValues">The values for the properties.</param>
        /// <returns>A <see cref="CustomAttributeBuilder"/>, or <c>null</c> if the constructor cannot be found.</returns>
        public static CustomAttributeBuilder? CreateCustomAttribute<TAttribute>(IEnumerable<Type>? argsTypes = null, 
                                                                                IEnumerable<object>? argsValues = null,
                                                                                IEnumerable<PropertyInfo>? propertyInfos = null,
                                                                                IEnumerable<object>? propertyValues = null)
            where TAttribute : Attribute
        {
            return CreateCustomAttribute(typeof(TAttribute), argsTypes, argsValues, propertyInfos, propertyValues);
        }



        /// <summary>
        /// Adds a custom attribute to a type builder.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute to add.</typeparam>
        /// <param name="typeBuilder">The type builder to modify.</param>
        /// <param name="argsTypes">The types of the constructor arguments.</param>
        /// <param name="argsValues">The values of the constructor arguments.</param>
        /// <param name="propertyInfos">The properties to set on the attribute.</param>
        /// <param name="propertyValues">The values for the properties.</param>
        public static void AddCustomAttributeToType<TAttribute>(this TypeBuilder typeBuilder,
                                                                   IEnumerable<Type>? argsTypes = null, 
                                                                   IEnumerable<object>? argsValues = null,
                                                                   IEnumerable<PropertyInfo>? propertyInfos = null,
                                                                   IEnumerable<object>? propertyValues = null)
            where TAttribute : Attribute
        {
            CustomAttributeBuilder? attributeBuilder = CreateCustomAttribute<TAttribute>(argsTypes, argsValues, propertyInfos, propertyValues);
            
            if (attributeBuilder != null)
                typeBuilder.SetCustomAttribute(attributeBuilder);
        }

        /// <summary>
        /// Adds a custom attribute to a type builder using the provided constructor arguments.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute to add.</typeparam>
        /// <param name="typeBuilder">The type builder to modify.</param>
        /// <param name="args">The constructor argument values.</param>
        public static void AddCustomAttributeToType<TAttribute>(this TypeBuilder typeBuilder, params object[] args)
            where TAttribute : Attribute
        {
            AddCustomAttributeToType<TAttribute>(typeBuilder, args.Select(a => a.GetType()), args);
        }




        /// <summary>
        /// Adds a custom attribute to a method builder.
        /// </summary>
        /// <param name="methodBuilder">The method builder to modify.</param>
        /// <param name="attributeType">The type of attribute to add.</param>
        /// <param name="argsTypes">The types of the constructor arguments.</param>
        /// <param name="argsValues">The values of the constructor arguments.</param>
        public static void AddCustomAttributeToMethod(this MethodBuilder methodBuilder,
                                                      Type attributeType,
                                                      IEnumerable<Type>? argsTypes = null,
                                                      IEnumerable<object>? argsValues = null)
        {
            CustomAttributeBuilder? attributeBuilder = CreateCustomAttribute(attributeType, argsTypes, argsValues);

            if (attributeBuilder != null)
                methodBuilder.SetCustomAttribute(attributeBuilder);
        }

        /// <summary>
        /// Adds a custom attribute to a method builder.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute to add.</typeparam>
        /// <param name="methodBuilder">The method builder to modify.</param>
        /// <param name="argsTypes">The types of the constructor arguments.</param>
        /// <param name="argsValues">The values of the constructor arguments.</param>
        public static void AddCustomAttributeToMethod<TAttribute>(this MethodBuilder methodBuilder,
                                                                   IEnumerable<Type>? argsTypes = null,
                                                                   IEnumerable<object>? argsValues = null)
            where TAttribute : Attribute
        {
            AddCustomAttributeToMethod(methodBuilder, typeof(TAttribute), argsTypes, argsValues);
        }

        /// <summary>
        /// Adds a custom attribute to a method builder using the provided constructor arguments.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute to add.</typeparam>
        /// <param name="methodBuilder">The method builder to modify.</param>
        /// <param name="args">The constructor argument values.</param>
        public static void AddCustomAttributeToMethod<TAttribute>(this MethodBuilder methodBuilder, params object[] args)
        {
            AddCustomAttributeToMethod<TAttribute>(methodBuilder, args.Select(a => a.GetType()), args);
        }

        /// <summary>
        /// Adds a custom attribute to a method builder using the provided constructor arguments.
        /// </summary>
        /// <param name="methodBuilder">The method builder to modify.</param>
        /// <param name="attribute">The type of attribute to add.</param>
        /// <param name="args">The constructor argument values.</param>
        public static void AddCustomAttributeToMethod(this MethodBuilder methodBuilder, Type attribute, params object[] args)
        {
            var argsValues = args.ToList();
            argsValues.RemoveAll(a => a == null);

            AddCustomAttributeToMethod(methodBuilder, attribute, argsValues.Select(a => a.GetType()), argsValues);
        }



        /// <summary>
        /// Adds a custom attribute to a parameter builder.
        /// </summary>
        /// <param name="parameterBuilder">The parameter builder to modify.</param>
        /// <param name="attributeType">The type of attribute to add.</param>
        /// <param name="argsTypes">The types of the constructor arguments.</param>
        /// <param name="argsValues">The values of the constructor arguments.</param>
        /// <param name="propertyInfos">The properties to set on the attribute.</param>
        /// <param name="propertyValues">The values for the properties.</param>
        public static void AddCustomAttributeToParameter(this ParameterBuilder parameterBuilder,
                                                      Type attributeType,
                                                      IEnumerable<Type>? argsTypes = null,
                                                      IEnumerable<object>? argsValues = null,
                                                      IEnumerable<PropertyInfo>? propertyInfos = null,
                                                      IEnumerable<object>? propertyValues = null)
        {
            CustomAttributeBuilder? attributeBuilder = CreateCustomAttribute(attributeType, argsTypes, argsValues, propertyInfos, propertyValues);

            if (attributeBuilder != null)
                parameterBuilder.SetCustomAttribute(attributeBuilder);
        }

        /// <summary>
        /// Adds a custom attribute to a parameter builder.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute to add.</typeparam>
        /// <param name="parameterBuilder">The parameter builder to modify.</param>
        /// <param name="argsTypes">The types of the constructor arguments.</param>
        /// <param name="argsValues">The values of the constructor arguments.</param>
        /// <param name="propertyInfos">The properties to set on the attribute.</param>
        /// <param name="propertyValues">The values for the properties.</param>
        public static void AddCustomAttributeToParameter<TAttribute>(this ParameterBuilder parameterBuilder,
                                                                   IEnumerable<Type>? argsTypes = null,
                                                                   IEnumerable<object>? argsValues = null,
                                                                   IEnumerable<PropertyInfo>? propertyInfos = null,
                                                                   IEnumerable<object>? propertyValues = null)
            where TAttribute : Attribute
        {
            AddCustomAttributeToParameter(parameterBuilder, typeof(TAttribute), argsTypes, argsValues, propertyInfos, propertyValues);
        }

        /// <summary>
        /// Adds a custom attribute to a parameter builder using the provided constructor arguments.
        /// </summary>
        /// <param name="parameterBuilder">The parameter builder to modify.</param>
        /// <param name="attribute">The type of attribute to add.</param>
        /// <param name="args">The constructor argument values.</param>
        public static void AddCustomAttributeToParameter(this ParameterBuilder parameterBuilder, Type attribute, params object[] args)
        {
            var argsValues = args.ToList();
            argsValues.RemoveAll(a => a == null);

            AddCustomAttributeToParameter(parameterBuilder, attribute, argsValues.Select(a => a.GetType()), argsValues);
        }

        #endregion "Create Attributes"
    }
}
