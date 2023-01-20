using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackDigital.Rest
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class FromRouteAttribute : Attribute
    {
        public FromRouteAttribute(string? name = null)
        {
            Name = name;
        }

        public string? Name { get; }
    }
}
