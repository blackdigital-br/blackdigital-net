
using System.Reflection;

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


    }
}
