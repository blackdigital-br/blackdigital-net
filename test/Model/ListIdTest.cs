using BlackDigital.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackDigital.Test.Model
{
    public class ListIdTest
    {
        [Fact]
        public void SeriarizableEnurable()
        {
            var list = new List<Id>
            {
                new Id(1),
                new Id(2),
                new Id(3),
                new Id(4),
                new Id(5),
            };

            var enumrableId = new ListId(list);

            var json = JsonCast.ToJson(enumrableId);

            var enumrableId2 = JsonCast.To<ListId>(json);
        }
    }
}
