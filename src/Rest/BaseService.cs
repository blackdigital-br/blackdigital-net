namespace BlackDigital.Rest
{
    public class BaseService
    {
        public BaseService(RestClient client)
        {
            Client = client;
        }

        protected readonly RestClient Client;
    }
}
