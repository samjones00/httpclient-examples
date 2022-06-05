using App.Interfaces;
using App.Models;
using App.Services;

namespace App
{
    public static class StartupExtensions
    {
        public static IServiceCollection MovieReviewService(this IServiceCollection services, IConfiguration configuration, Action<ApiOptions>? setupAction = null)
        {
            services.AddTransient<MovieReviewService, MovieReviewService>();
            services.AddHttpClient<Services.MovieReviewService>();

            services.AddOptions<ApiOptions>()
                .Bind(configuration.GetSection(ApiOptions.SectionName))
                .Configure(setupAction)
                .ValidateDataAnnotations();

            return services;
        }
    }
}