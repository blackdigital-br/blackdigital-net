using Microsoft.Extensions.DependencyInjection;

namespace BlackDigital.Rest
{
    public static class RestServiceDependencyInjection
    {
        public static RestServiceBuilder AddRestService(this IServiceCollection services)
        {
            return new(services);
        }
    }
}
