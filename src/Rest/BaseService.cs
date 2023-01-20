namespace BlackDigital.Rest
{
    public abstract class BaseService<BaseType>
    {
        public BaseService(RestClient client)
        {
            Client = client;
        }

        protected readonly RestClient Client;

        protected async Task<T> ExecuteRequest<T>(string name, Dictionary<string, object> arguments)
        {
            return Activator.CreateInstance<T>();
        }
    }
}
