using BlackDigital.Test.Mock;

namespace BlackDigital.Test
{
    public class HttpHelperTest
    {
        [Fact]
        public void CastSimpleQueryString()
        {
            string queryString = "?id=5&name=BlackDigital";

            var simpleClass = queryString.FromQueryString<SimpleClass>();

            Assert.NotNull(simpleClass);
            Assert.Equal(5, simpleClass.Id);
            Assert.Equal("BlackDigital", simpleClass.Name);
        }

        [Fact]
        public void ComplexSimpleQueryString()
        {
            ComplexClass origin = new()
            {
                Id = 100,
                Name = "Origin Class Instance",
                Parent = new()
                {
                    Id  = 10,
                    Name = "Parent Class Instance"
                },
                /*ListChild = new()
                {
                    new()
                    {
                        Id = 1,
                        Name = "Simple 1"
                    },
                    new()
                    {
                        Id = 2,
                        Name = "Simple 2"
                    },
                    new()
                    {
                        Id = 3,
                        Name = "Simple 3"
                    }
                }*/
                ListChild = new SimpleClass[]
                {
                    new()
                    {
                        Id = 1,
                        Name = "Simple 1"
                    },
                    new()
                    {
                        Id = 2,
                        Name = "Simple 2"
                    },
                    new()
                    {
                        Id = 3,
                        Name = "Simple 3"
                    }
                }
            };
            
            string queryString = $"?{origin.ToUrlQueryString()}";

            var complexClass = queryString.FromQueryString<ComplexClass>();

            Assert.NotNull(complexClass);
            Assert.Equal(origin.Id, complexClass.Id);
            Assert.Equal(origin.Name, complexClass.Name);

            Assert.NotNull(complexClass.Parent);
            Assert.Equal(origin.Parent.Id, complexClass.Parent.Id);
            Assert.Equal(origin.Parent.Name, complexClass.Parent.Name);

            Assert.NotNull(complexClass.ListChild);
            //Assert.Equal(origin.ListChild.Count, complexClass.ListChild.Count);
        }
    }
}