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

        public async Task<List<WeatherResponse>> GetAllWeather()
        {
            var weatherResponse = await _weatherService.GetAllWeather();

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

        public Task<List<WeatherResponse>> GetAllWeatherFromProvider(string provider)
        {
            return _weatherService.GetAllWeatherFromProvider(provider);
        }

        public Task<List<WeatherResponse>> GetWeatherFromCity(string city, string state = null, string country = null)
        {
            return _weatherService.GetWeatherFromCity(city, state, country)
                .ContinueWith(task => task.Result.Select(w => new WeatherResponse
                {
                    Id = w.Id,
                    Provider = w.Provider,
                    City = w.City,
                    State = w.State,
                    Country = w.Country,
                    CelsiusTemperatureMin = w.CelsiusTemperatureMin,
                    CelsiusTemperatureMax = w.CelsiusTemperatureMax,
                    LastUpdate = w.LastUpdate
                }).ToList());
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}