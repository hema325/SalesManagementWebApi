using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace Infrastructure.FileStorage
{
    internal static class ConfigureFileStorageService
    {
        public static IServiceCollection AddFileStorage(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IFileStorage, LocalFileStorageService>();

            services.Configure<FileStorageSettings>(configuration.GetSection(FileStorageSettings.SectionName));

            return services;
        }

        public static IApplicationBuilder UseFileStorage(this IApplicationBuilder app,IConfiguration configuration)
        {
            var fileSttings = configuration.GetSection(FileStorageSettings.SectionName).Get<FileStorageSettings>();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), fileSttings.RootPath)),
                RequestPath = "/Files"
            });

            return app;
        }
    }
}
