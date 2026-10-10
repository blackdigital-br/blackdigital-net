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
    }
}
