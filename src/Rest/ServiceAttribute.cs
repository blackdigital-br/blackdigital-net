using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackDigital.Rest
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
    public class ServiceAttribute : Attribute
    {
        public ServiceAttribute(string? baseRoute = null)
        {
            BaseRoute = baseRoute;
        }

        public string? BaseRoute { get; set; }
    }
}
