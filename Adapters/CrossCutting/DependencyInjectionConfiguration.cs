using Application.Interfaces;
using CrossCutting.Api;
using CrossCutting.Application;
using CrossCutting.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace CrossCutting
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjectionConfiguration
    {
        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            try
            {
                DataExtensions.RegisterData(services, configuration);

                services.AddLogging();

                ApplicationExtensions.RegisterApplication(services);

                ApiExtensions.RegisterApi(services, configuration);

                services.AddWeatherIntegration();
            }
            catch (Exception ex)
            {
                var loggerFactory = services.BuildServiceProvider().GetService<ILoggerFactory>();

                var logger = loggerFactory.CreateLogger(typeof(DependencyInjectionConfiguration));

                logger?.LogError(ex, "An error occurred while registering dependencies.");

                throw;
            }
        }

        public static IServiceCollection AddWeatherIntegration(this IServiceCollection services)
        {
            services.AddHttpClient<IWeatherProviderAdapter, OpenWeatherAdapter>();

            services.AddScoped<WeatherSyncService>();

            return services;
        }
    }
}