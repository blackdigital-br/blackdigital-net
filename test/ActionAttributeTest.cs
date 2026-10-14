using BlackDigital.Rest;

namespace BlackDigital.Test
{
    public class ActionAttributeTest
    {
        [Fact]
        public void Policy_WhenNotSet_DefaultsToNull()
        {
            var attribute = new ActionAttribute();

            Assert.Null(attribute.Policy);
        }

        [Fact]
        public void Policy_WhenSetViaConstructor_ReturnsValue()
        {
            var attribute = new ActionAttribute(policy: "Read");

            Assert.Equal("Read", attribute.Policy);
        }

        [Fact]
        public void Policy_DoesNotAffectOtherProperties()
        {
            var attribute = new ActionAttribute("search",
                                                 method: RestMethod.Post,
                                                 authorize: false,
                                                 returnIsSuccess: true,
                                                 keyIdentifier: "id",
                                                 version: "2025-10-08",
                                                 policy: "Create");

            Assert.Equal("search", attribute.Route);
            Assert.Equal(RestMethod.Post, attribute.Method);
            Assert.False(attribute.Authorize);
            Assert.True(attribute.ReturnIsSuccess);
            Assert.Equal("id", attribute.KeyIdentifier);
            Assert.Equal("2025-10-08", attribute.Version);
            Assert.Equal("Create", attribute.Policy);
        }

        [Fact]
        public void NameAndDescription_WhenNotSet_DefaultsToNull()
        {
            var attribute = new ActionAttribute();

            Assert.Null(attribute.Name);
            Assert.Null(attribute.Description);
            Assert.Null(attribute.ResourceType);
        }

        [Fact]
        public void GetName_ReturnsName()
        {
            var attribute = new ActionAttribute(name: "Search Users");

            Assert.Equal("Search Users", attribute.GetName());
        }

        [Fact]
        public void GetDescription_ReturnsDescription()
        {
            var attribute = new ActionAttribute(description: "Searches users by filter");

            Assert.Equal("Searches users by filter", attribute.GetDescription());
        }

        [Fact]
        public void GetNameAndDescription_WhenValuesAreNull_ReturnsEmptyString()
        {
            var attribute = new ActionAttribute();

            Assert.Equal(string.Empty, attribute.GetName());
            Assert.Equal(string.Empty, attribute.GetDescription());
        }
    }
}
