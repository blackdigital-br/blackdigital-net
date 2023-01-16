
namespace BlackDigital.Test
{
    public class UriHelperTest
    {
        private static readonly string UrlTest = "https://blackdigital.com.br/test?txt=my%20Text&nmb=10&myDate=2023-01-01";

        [Fact]
        public void AppendUri()
        {
            Uri uri = new(UrlTest);
            uri = uri.Append("test2", "test3");

            Assert.Equal("https://blackdigital.com.br/test/test2/test3?txt=my%20Text&nmb=10&myDate=2023-01-01", uri.AbsoluteUri);
        }

        [Fact]
        public void GetQueryString()
        {
            Uri uri = new(UrlTest);

            var queryString = uri.GetQueryString();

            Assert.Equal("my%20Text", queryString["txt"]);
            Assert.Equal("10", queryString["nmb"]);
            Assert.Equal("2023-01-01", queryString["myDate"]);
        }
    }
}
