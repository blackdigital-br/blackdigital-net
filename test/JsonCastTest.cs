using BlackDigital.Test.Mock;
using System.Net;

namespace BlackDigital.Test
{
    public class JsonCastTest
    {
        [Fact]
        public void JsonCast()
        {
            var obj = new SimpleModel
            {
                Name = "test",
                Value = 12,
                HttpStatus = HttpStatusCode.OK,
            };

            var json = obj.ToJson();

            Assert.Equal("{\"Value\":12,\"Name\":\"test\",\"HttpStatus\":\"OK\"}", json);

            var objFromJson = json.To<SimpleModel>();

            Assert.NotNull(objFromJson);
            Assert.Equal(obj.Name, objFromJson.Name);
            Assert.Equal(obj.Description, objFromJson.Description);
            Assert.Equal(obj.HttpStatus, objFromJson.HttpStatus);
            Assert.Equal(obj.Value, objFromJson.Value);
        }

        [Fact]
        public void Clone()
        {
            var obj = new SimpleModel
            {
                Name = "test",
                Value = 12,
                HttpStatus = HttpStatusCode.OK,
            };

            var objFromJson = obj.CloneOject();

            Assert.NotNull(objFromJson);
            Assert.Equal(obj.Name, objFromJson.Name);
            Assert.Equal(obj.Description, objFromJson.Description);
            Assert.Equal(obj.HttpStatus, objFromJson.HttpStatus);
            Assert.Equal(obj.Value, objFromJson.Value);
        }
    }
}