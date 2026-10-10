namespace BlackDigital.Test
{
    public class BusinessExceptionTest
    {
        [Fact]
        public void Constructor_WithMessage_UsesProvidedCodeOrDefault()
        {
            var defaultException = new BusinessException("custom");
            var customCodeException = new BusinessException("custom", 499);

            Assert.Equal((int)BusinessExceptionType.ExpectationFailed, defaultException.Code);
            Assert.Equal(499, customCodeException.Code);
            Assert.Equal("custom", customCodeException.Message);
        }

        [Fact]
        public void Constructor_WithType_UsesEnumNameAndValue()
        {
            var exception = new BusinessException(BusinessExceptionType.NotFound);

            Assert.Equal("NotFound", exception.Message);
            Assert.Equal(404, exception.Code);
        }

        [Fact]
        public void ThrowIfNullHelpers_ReturnValueOrThrowExpectedException()
        {
            var value = "ok";

            Assert.Equal(value, BusinessException.ThrowIfNull(value, "error"));

            var exception = Assert.Throws<BusinessException>(() => BusinessException.ThrowNotFoundIfNull<string>(null));
            Assert.Equal((int)BusinessExceptionType.NotFound, exception.Code);
            Assert.Equal("NotFound", exception.Message);
        }
    }
}
