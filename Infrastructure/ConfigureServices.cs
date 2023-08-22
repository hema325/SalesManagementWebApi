using Infrastructure.Authentication;
using Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Infrastructure.FileStorage;

namespace Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity(); // it adds its default authentication but i do not need it sot it must come before auth service
            services.AddAuth(configuration);
            services.AddCommonServices();
            services.AddPersistence(configuration);
            services.AddFileStorage(configuration);

            return services;
        }

        public static async Task<IServiceProvider> InitializeDBAsync(this IServiceProvider services)
        {
            await services.CreateScope().ServiceProvider
                          .GetRequiredService<ApplicationDbContextInitialiser>()
                          .InitialiseAsync();

            return services;
        }
        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseFileStorage(configuration);
            app.UseAuth();

            return app;
        }
    }
}
