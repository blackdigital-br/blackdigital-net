using BlackDigital.Test.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackDigital.Test
{
    [Trait("ObjectHelper", "Test Object helper")]
    public class ObjectHelperTest
    {
        [Fact(DisplayName = "Null To QueryString")]
        public void NullToQueryString()
        {
            var queryString = ObjectHelper.ToUrlQueryString<string>(null);
            Assert.Equal(queryString, string.Empty);

            queryString = ObjectHelper.ToUrlQueryString<string>(null, "test");
            Assert.Equal(queryString, string.Empty);
        }

        [Fact(DisplayName = "String and Value To QueryString")]
        public void StringAndValueToQueryString()
        {
            var queryString = "MyValue".ToUrlQueryString();

            Assert.Equal("MyValue", queryString);

            queryString = "MyValue".ToUrlQueryString("test");
            Assert.Equal("test=MyValue", queryString);

            queryString = 15.ToUrlQueryString();
            Assert.Equal("15", queryString);

            queryString = 15.ToUrlQueryString("test");
            Assert.Equal("test=15", queryString);

            queryString = (new DateTime(2022, 8, 11, 10, 45, 26, 70, DateTimeKind.Utc)).ToUrlQueryString();
            Assert.Equal("2022-08-11T10:45:26.0700000Z", queryString);

            queryString = (new DateTime(2022, 8, 11, 10, 45, 26, 70, DateTimeKind.Utc)).ToUrlQueryString("test");
            Assert.Equal("test=2022-08-11T10:45:26.0700000Z", queryString);
        }

        [Fact(DisplayName = "List To QueryString")]
        public void ListToQueryString()
        {
            List<object> list = new()
            {
                15,
                "MyValue",
                new int[] { 1, 2 },
            };

            var queryString = list.ToUrlQueryString();
            Assert.Equal("15&MyValue&1&2", queryString);

            queryString = list.ToUrlQueryString("test");
            Assert.Equal("test[0]=15&test[1]=MyValue&test[2][0]=1&test[2][1]=2", queryString);
        }

        [Fact(DisplayName = "Object To QueryString")]
        public void ObjectToQueryString()
        {
            var myObject = new ComplexModel()
            {
                Id = 12,
                Name = "My Name",
                Number = null,
                Status = true,
                List = new()
                {
                    new SimpleModel
                    {
                        Name = "Item 1",
                        Description = null,
                        HttpStatus = System.Net.HttpStatusCode.Accepted,
                        Value = 1,
                    },
                    new SimpleModel
                    {
                        Name = "Item 2",
                        Description = "Description 2",
                        HttpStatus = System.Net.HttpStatusCode.Accepted,
                        Value = 2,
                    }
                }
            };

            var queryString = myObject.ToUrlQueryString();
            Assert.Equal("Id=12&Name=My+Name&Status=True&List[0].Value=1&List[0].Name=Item+1&List[0].HttpStatus=Accepted&List[1].Value=2&List[1].Name=Item+2&List[1].Description=Description+2&List[1].HttpStatus=Accepted", queryString);
        }
    }
}
