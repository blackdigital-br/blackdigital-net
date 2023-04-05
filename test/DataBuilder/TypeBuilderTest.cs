using BlackDigital.DataBuilder;
using BlackDigital.Test.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackDigital.Test.DataBuilder
{
    public class TypeBuilderTest
    {
        [Fact]
        public void CreateTypeBuilder()
        {
            var typeBuilder = TypeBuilder.Create<SimpleClass>();

            Assert.Equal(1, typeBuilder?.Properties.Count);
            
        }
    }
}
