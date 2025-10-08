namespace BlackDigital.Rest
{
    public class RestCallConfig
    {
        public List<KeyValuePair<string, string>>? ExtraHeaders { get; private set; }

        public List<KeyValuePair<string, string>>? QueryParameters { get; private set; }


        public RestCallConfig AddHeader(string key, string value)
        {
            if (ExtraHeaders == null)
                ExtraHeaders = new();

            ExtraHeaders.Add(new KeyValuePair<string, string>(key, value));

            return this;
        }

        public RestCallConfig AddQueryParameter(string key, string value)
        {
            if (QueryParameters == null)
                QueryParameters = new();

            QueryParameters.Add(new KeyValuePair<string, string>(key, value));
            return this;
        }

        public RestCallConfig AddVersion(string version)
         => AddQueryParameter("api-version", version);

        public static RestCallConfig Create()
            => new RestCallConfig();
    }
}
