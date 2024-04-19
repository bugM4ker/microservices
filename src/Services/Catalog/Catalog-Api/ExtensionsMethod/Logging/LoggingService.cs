using Catalog_Api.Middlewares;

namespace Catalog_Api.ExtensionsMethod.Logging
{

    public static class LoggingServices
    {
        public static IServiceCollection AddLoggingServices(this IServiceCollection services)
        {
            services.AddSingleton<ILogger>(provider => provider.GetRequiredService<ILogger<LoggingMiddleware>>());
            return services;
        }
    }
}
