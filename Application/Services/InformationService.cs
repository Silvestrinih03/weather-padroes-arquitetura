using Application.DTOs.Responses;
using Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public sealed class InformationService : IInformationService
    {
        private readonly IWeatherService _weatherService;
        private readonly ILogger _logger;

        public InformationService(
            IWeatherService weatherService,
            ILogger logger)
        {
            _weatherService = weatherService;
            _logger = logger;
        }

        public async Task<List<WeatherResponse>> GetWeather()
        {
            var weatherResponse = await _weatherService.GetWeatherFromABC();

            List<WeatherResponse> weatherList = new List<WeatherResponse>();

            foreach (var weather in weatherResponse)
            {
                weatherList.Add(new WeatherResponse
                {
                    Id = weather.Id,
                    Provider = weather.Provider,
                    City = weather.City,
                    State = weather.State,
                    Country = weather.Country,
                    CelsiusTemperatureMin = weather.CelsiusTemperatureMin,
                    CelsiusTemperatureMax = weather.CelsiusTemperatureMax,
                    LastUpdate = weather.LastUpdate
                });
            }

            return weatherList;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}