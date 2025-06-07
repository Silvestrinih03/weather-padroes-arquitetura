using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace CrossCutting.Application
{
    [ExcludeFromCodeCoverage]
    public static class ApplicationExtensions
    {
        public static void RegisterApplication(this IServiceCollection service)
        {
            AddServices(service);
        }

        private static void AddServices(this IServiceCollection service)
        {
            _ = service.AddSingleton(sp => sp.GetRequiredService<ILoggerFactory>().CreateLogger("Logger"));
            _ = service.AddScoped<IIntegrationService, IntegrationService>();
            _ = service.AddScoped<IInformationService, InformationService>();
            _ = service.AddScoped<IWeatherService, WeatherService>();
        }
    }
}