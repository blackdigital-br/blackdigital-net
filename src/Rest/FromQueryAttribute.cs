﻿
namespace BlackDigital.Rest
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class FromQueryAttribute : Attribute
    {
        public FromQueryAttribute(QueryParameterType type = QueryParameterType.Raw, string name = null)
        {
            Type = type;
            Name = name;
        }

        public string Name { get; }
        public QueryParameterType Type { get; set; }
    }
}
