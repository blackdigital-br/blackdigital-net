using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackDigital.Rest
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class ActionAttribute : Attribute
    {
        public ActionAttribute(string? route = null, 
                               RestMethod method = RestMethod.Get, 
                               bool authorize = true,
                               bool returnIsSuccess = false)
        {
            Route = route;
            Method = method;
            Authorize = authorize;
            ReturnIsSuccess = returnIsSuccess;
        }

        public string? Route { get; private set; }

        public RestMethod Method { get; private set; }

        public bool Authorize { get; private set; }

        public bool ReturnIsSuccess { get; private set; }
    }
}
