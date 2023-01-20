using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackDigital.Rest
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class ActionAttribute : Attribute
    {
        public ActionAttribute(string? route = null, HttpMethod? method = null, bool authorize = true)
        {
            Route = route;
            Method = method ?? HttpMethod.Get;
            Authorize = authorize;
        }

        public string? Route { get; private set; }

        public HttpMethod Method { get; private set; }

        public bool Authorize { get; private set; }
    }
}
