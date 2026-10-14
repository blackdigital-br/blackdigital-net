using BlackDigital.Rest;

namespace BlackDigital.Test
{
    public class ServiceAttributeTest
    {
        [Fact]
        public void GetServiceName_ReturnsName()
        {
            var attribute = new ServiceAttribute("api/users", name: "Users");

            var result = attribute.GetServiceName();

            Assert.Equal("Users", result);
        }

        [Fact]
        public void GetServiceDescription_ReturnsDescription()
        {
            var attribute = new ServiceAttribute("api/users", description: "Users service");

            var result = attribute.GetServiceDescription();

            Assert.Equal("Users service", result);
        }

        [Fact]
        public void GetServiceData_WhenValueIsNull_ReturnsEmptyString()
        {
            var attribute = new ServiceAttribute("api/users");

            Assert.Equal(string.Empty, attribute.GetServiceName());
            Assert.Equal(string.Empty, attribute.GetServiceDescription());
        }

        [Fact]
        public void Policy_WhenNotSet_DefaultsToNull()
        {
            var attribute = new ServiceAttribute("api/users");

            Assert.Null(attribute.Policy);
        }

        [Fact]
        public void Policy_WhenSetViaConstructor_ReturnsValue()
        {
            var attribute = new ServiceAttribute("api/users", policy: "Category");

            Assert.Equal("Category", attribute.Policy);
        }

        [Fact]
        public void Policy_CanBeSetViaProperty()
        {
            var attribute = new ServiceAttribute("api/users")
            {
                Policy = "Category"
            };

            Assert.Equal("Category", attribute.Policy);
        }
    }
}
