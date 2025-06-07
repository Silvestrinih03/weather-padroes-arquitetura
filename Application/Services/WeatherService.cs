using Application.DTOs.Responses;
using Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public sealed class WeatherService : IWeatherService
    {
        private readonly ILogger _logger;

        public WeatherService(
            ILogger logger)
        {
            _logger = logger;
        }

        public async Task<List<WeatherResponse>> GetWeatherFromABC()
        {
            return null;
        }

        public async Task<List<WeatherResponse>> GetWeatherFromDFG()
        {
            return null;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}