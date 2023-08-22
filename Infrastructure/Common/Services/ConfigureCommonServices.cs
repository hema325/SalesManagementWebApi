using Infrastructure.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Common.Services
{
    internal static class ConfigureCommonServices
    {
        public static IServiceCollection AddCommonServices(this IServiceCollection services)
        {
            services.AddScoped<IDateTime, DateTimeService>();
            services.AddScoped<IFileBuilder, FileBuilder>();

            return services;
        }
    }
}
