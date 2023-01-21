using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackDigital.Test.Mock
{
    public class ComplexModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? Number { get; set; }

        public bool Status { get; set; }

        public List<SimpleModel> List { get; set; }
    }
}
