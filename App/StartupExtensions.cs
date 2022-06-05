using App.Interfaces;
using App.Models;
using App.Services;

namespace App
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddHttpService(this IServiceCollection services, IConfiguration configuration, Action<ApiOptions>? setupAction = null)
        {
            services.AddTransient<IHttpService, HttpService>();
            services.AddHttpClient<HttpService>();

            services.AddOptions<ApiOptions>()
                .Bind(configuration.GetSection(ApiOptions.SectionName))
                .Configure(setupAction)
                .ValidateDataAnnotations();

            return services;
        }
    }
}