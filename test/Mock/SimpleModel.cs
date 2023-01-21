using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BlackDigital.Test.Mock
{
    public class SimpleModel
    {
        public int Value { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public HttpStatusCode HttpStatus { get; set; }
    }
}
