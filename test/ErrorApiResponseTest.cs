namespace BlackDigital.Test
{
    public class ErrorApiResponseTest
    {
        [Fact]
        public void Create_WithUnexpectedExceptionInProduction_HidesDetails()
        {
            var previousEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var previousDebug = Environment.GetEnvironmentVariable("DEBUG__ACTIVE");

            try
            {
                Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Production");
                Environment.SetEnvironmentVariable("DEBUG__ACTIVE", "false");

                var response = ErrorApiResponse.Create(new Exception("database offline"));

                Assert.Equal("An unexpected error occurred. Please try again later.", response.Message);
                Assert.Null(response.Errors);
            }
            finally
            {
                Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", previousEnvironment);
                Environment.SetEnvironmentVariable("DEBUG__ACTIVE", previousDebug);
            }
        }

        [Fact]
        public void Create_WithBusinessExceptionInProduction_ShowsRealError()
        {
            var previousEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var previousDebug = Environment.GetEnvironmentVariable("DEBUG__ACTIVE");

            try
            {
                Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Production");
                Environment.SetEnvironmentVariable("DEBUG__ACTIVE", "false");

                var response = ErrorApiResponse.Create(BusinessException.New("conflict", 409));

                Assert.Equal("conflict", response.Message);
                Assert.NotNull(response.Errors);
                Assert.Single(response.Errors);
                Assert.Contains("BusinessException: conflict", response.Errors[0]);
            }
            finally
            {
                Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", previousEnvironment);
                Environment.SetEnvironmentVariable("DEBUG__ACTIVE", previousDebug);
            }
        }

        [Fact]
        public void Create_InDevelopment_ExtractsNestedMessages()
        {
            var previousEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var previousDebug = Environment.GetEnvironmentVariable("DEBUG__ACTIVE");

            try
            {
                Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
                Environment.SetEnvironmentVariable("DEBUG__ACTIVE", "false");

                var exception = new InvalidOperationException("outer", new ArgumentException("inner"));
                var response = ErrorApiResponse.Create(exception);

                Assert.Equal("outer", response.Message);
                Assert.NotNull(response.Errors);
                Assert.Equal(2, response.Errors.Count);
                Assert.Contains(response.Errors, error => error.Contains("ArgumentException: inner"));
                Assert.Contains(response.Errors, error => error.Contains("InvalidOperationException: outer"));
            }
            finally
            {
                Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", previousEnvironment);
                Environment.SetEnvironmentVariable("DEBUG__ACTIVE", previousDebug);
            }
        }
    }
}
