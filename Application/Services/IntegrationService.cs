using Application.DTOs.Requests;
using Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public sealed class IntegrationService : IIntegrationService
    {
        private readonly IWeatherService _weatherService;
        private readonly ILogger _logger;

        public IntegrationService(
            IWeatherService weatherService,
            ILogger logger)
        {
            _weatherService = weatherService;
            _logger = logger;
        }

        public async Task SaveWeatherToLocalDatabase(List<WeatherRequest> weatherRequests)
        {
            // Code
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}