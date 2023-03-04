using BlackDigital.Rest;
using BlackDigital.Test.Mock;
using System.ComponentModel.DataAnnotations;

namespace BlackDigital.Test
{
    public class ReflectionHelperTest
    {
        [Fact]
        public void GetAttributeFromType()
        {
            var attributes = typeof(IServiceTest).GetAttributes<ServiceAttribute>();

            Assert.Single(attributes);
            Assert.Equal(typeof(ServiceAttribute), attributes.First().GetType());

            var attribute = typeof(IServiceTest).GetSingleAttribute<ServiceAttribute>();
            Assert.NotNull(attribute);
            Assert.Equal(typeof(ServiceAttribute), attribute.GetType());
        }

        [Fact]
        public void GetAttributeFromMethod()
        {
            var attributes = typeof(IServiceTest).GetMethod("MyAction").GetAttributes<ActionAttribute>();

            Assert.Single(attributes);
            Assert.Equal(typeof(ActionAttribute), attributes.First().GetType());

            var attribute = typeof(IServiceTest).GetMethod("MyAction").GetSingleAttribute<ActionAttribute>();
            Assert.NotNull(attribute);
            Assert.Equal(typeof(ActionAttribute), attribute.GetType());
        }

        [Fact]
        public void GetAttributeFromProperty()
        {
            var attributes = typeof(SimpleClass).GetProperty("Id").GetAttributes<KeyAttribute>();

            Assert.Single(attributes);
            Assert.Equal(typeof(KeyAttribute), attributes.First().GetType());

            var attribute = typeof(SimpleClass).GetProperty("Id").GetSingleAttribute<KeyAttribute>();
            Assert.NotNull(attribute);
            Assert.Equal(typeof(KeyAttribute), attribute.GetType());
        }

        [Fact]
        public void GetAttributeFromMember()
        {
            var attributes = typeof(SimpleClass).GetMember("Name").First().GetAttributes<RequiredAttribute>();

            Assert.Single(attributes);
            Assert.Equal(typeof(RequiredAttribute), attributes.First().GetType());

            var attribute = typeof(SimpleClass).GetMember("Name").First().GetSingleAttribute<RequiredAttribute>();
            Assert.NotNull(attribute);
            Assert.Equal(typeof(RequiredAttribute), attribute.GetType());
        }

        [Fact]
        public void GetAttributeFromParameter()
        {
            var attributes = typeof(IServiceTest).GetMethod("MyAction").GetParameters().First().GetAttributes<FromRouteAttribute>();

            Assert.Single(attributes);
            Assert.Equal(typeof(FromRouteAttribute), attributes.First().GetType());

            var attribute = typeof(IServiceTest).GetMethod("MyAction").GetParameters().First().GetSingleAttribute<FromRouteAttribute>();
            Assert.NotNull(attribute);
            Assert.Equal(typeof(FromRouteAttribute), attribute.GetType());
        }
    }
}
