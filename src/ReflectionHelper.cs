
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Reflection.Emit;

namespace BlackDigital
{
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

        public static TAttribute[] GetAttributes<TAttribute>(this Type type)
            where TAttribute : Attribute =>
                type.GetCustomAttributes(typeof(TAttribute), true)
                        .Cast<TAttribute>()
                        .ToArray();


        public static TAttribute? GetSingleAttribute<TAttribute>(this Type type)
            where TAttribute : Attribute =>
                type.GetCustomAttributes(typeof(TAttribute), true)
                        .Cast<TAttribute>()
                        .SingleOrDefault();

        public static TAttribute[] GetAttributes<TAttribute>(this MethodInfo method)
            where TAttribute : Attribute =>
                method.GetCustomAttributes(typeof(TAttribute), true)
                        .Cast<TAttribute>()
                        .ToArray();


        public static TAttribute? GetSingleAttribute<TAttribute>(this MethodInfo method)
            where TAttribute : Attribute =>
                method.GetCustomAttributes(typeof(TAttribute), true)
                        .Cast<TAttribute>()
                        .SingleOrDefault();

        public static TAttribute[] GetAttributes<TAttribute>(this PropertyInfo property)
            where TAttribute : Attribute =>
                property.GetCustomAttributes(typeof(TAttribute), true)
                        .Cast<TAttribute>()
                        .ToArray();


        public static TAttribute? GetSingleAttribute<TAttribute>(this PropertyInfo property)
            where TAttribute : Attribute =>
                property.GetCustomAttributes(typeof(TAttribute), true)
                        .Cast<TAttribute>()
                        .SingleOrDefault();

        public static TAttribute[] GetAttributes<TAttribute>(this MemberInfo member)
            where TAttribute : Attribute =>
                member.GetCustomAttributes(typeof(TAttribute), true)
                        .Cast<TAttribute>()
                        .ToArray();


        public static TAttribute? GetSingleAttribute<TAttribute>(this MemberInfo member)
            where TAttribute : Attribute =>
                member.GetCustomAttributes(typeof(TAttribute), true)
                        .Cast<TAttribute>()
                        .SingleOrDefault();

        public static TAttribute[] GetAttributes<TAttribute>(this ParameterInfo parameter)
            where TAttribute : Attribute =>
                parameter.GetCustomAttributes(typeof(TAttribute), true)
                        .Cast<TAttribute>()
                        .ToArray();

        public static TAttribute? GetSingleAttribute<TAttribute>(this ParameterInfo parameter)
            where TAttribute : Attribute =>
                parameter.GetCustomAttributes(typeof(TAttribute), true)
                        .Cast<TAttribute>()
                        .SingleOrDefault();


        public static DisplayAttribute? GetDisplay(this PropertyInfo property)
            => property.GetSingleAttribute<DisplayAttribute>();

        public static string GetDisplayName(this PropertyInfo property)
            => property.GetDisplay()?.Name ?? property.Name;

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

        public static CustomAttributeBuilder? CreateCustomAttribute<TAttribute>(IEnumerable<Type>? argsTypes = null, 
                                                                                IEnumerable<object>? argsValues = null,
                                                                                IEnumerable<PropertyInfo>? propertyInfos = null,
                                                                                IEnumerable<object>? propertyValues = null)
            where TAttribute : Attribute
        {
            return CreateCustomAttribute(typeof(TAttribute), argsTypes, argsValues);
        }

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

        public static void AddCustomAttributeToType<TAttribute>(this TypeBuilder typeBuilder, params object[] args)
            where TAttribute : Attribute
        {
            AddCustomAttributeToType<TAttribute>(typeBuilder, args.Select(a => a.GetType()), args);
        }

        public static void AddCustomAttributeToMethod(this MethodBuilder methodBuilder,
                                                      Type attributeType,
                                                      IEnumerable<Type>? argsTypes = null,
                                                      IEnumerable<object>? argsValues = null)
        {
            CustomAttributeBuilder? attributeBuilder = CreateCustomAttribute(attributeType, argsTypes, argsValues);

            if (attributeBuilder != null)
                methodBuilder.SetCustomAttribute(attributeBuilder);
        }

        public static void AddCustomAttributeToMethod<TAttribute>(this MethodBuilder methodBuilder,
                                                                   IEnumerable<Type>? argsTypes = null,
                                                                   IEnumerable<object>? argsValues = null)
            where TAttribute : Attribute
        {
            AddCustomAttributeToMethod(methodBuilder, typeof(TAttribute), argsTypes, argsValues);
        }

        public static void AddCustomAttributeToMethod<TAttribute>(this MethodBuilder methodBuilder, params object[] args)
        {
            AddCustomAttributeToMethod<TAttribute>(methodBuilder, args.Select(a => a.GetType()), args);
        }

        public static void AddCustomAttributeToMethod(this MethodBuilder methodBuilder, Type attribute, params object[] args)
        {
            var argsValues = args.ToList();
            argsValues.RemoveAll(a => a == null);

            AddCustomAttributeToMethod(methodBuilder, attribute, argsValues.Select(a => a.GetType()), argsValues);
        }


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

        public static void AddCustomAttributeToParameter<TAttribute>(this ParameterBuilder parameterBuilder,
                                                                   IEnumerable<Type>? argsTypes = null,
                                                                   IEnumerable<object>? argsValues = null,
                                                                   IEnumerable<PropertyInfo>? propertyInfos = null,
                                                                   IEnumerable<object>? propertyValues = null)
            where TAttribute : Attribute
        {
            AddCustomAttributeToParameter(parameterBuilder, typeof(TAttribute), argsTypes, argsValues, propertyInfos, propertyValues);
        }

        public static void AddCustomAttributeToParameter(this ParameterBuilder parameterBuilder, Type attribute, params object[] args)
        {
            var argsValues = args.ToList();
            argsValues.RemoveAll(a => a == null);

            AddCustomAttributeToParameter(parameterBuilder, attribute, argsValues.Select(a => a.GetType()), argsValues);
        }
    }
}
