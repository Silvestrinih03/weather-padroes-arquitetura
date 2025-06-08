using Application.DTOs.Responses;
using Application.Interfaces;
using Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public sealed class WeatherService : IWeatherService
    {
        private readonly IWeatherRepository _repository;
        private readonly ILogger _logger;

        public WeatherService(
            IWeatherRepository repository,
            ILogger logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<List<WeatherResponse>> GetAllWeather()
        {
            var weatherList = await _repository.GetAllWeatherReports();

            return weatherList.Select(w => new WeatherResponse
            {
                Id = w.Id,
                Provider = w.Provider,
                City = w.City,
                State = w.State,
                Country = w.Country,
                CelsiusTemperatureMin = w.CelsiusTemperatureMin,
                CelsiusTemperatureMax = w.CelsiusTemperatureMax,
                LastUpdate = w.LastUpdate
            }).ToList();
        }

        public async Task<List<WeatherResponse>> GetAllWeatherFromProvider(string provider)
        {
            var weatherReports = await _repository.GetWeatherReportByProvider(provider);

            return weatherReports.Select(w => new WeatherResponse
            {
                Id = w.Id,
                Provider = w.Provider,
                City = w.City,
                State = w.State,
                Country = w.Country,
                CelsiusTemperatureMin = w.CelsiusTemperatureMin,
                CelsiusTemperatureMax = w.CelsiusTemperatureMax,
                LastUpdate = w.LastUpdate
            }).ToList();
        }

        public Task<List<WeatherResponse>> GetWeatherFromCity(string city, string state, string country)
        {
            return _repository.GetWeatherReportsByLocation(city, state, country)
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