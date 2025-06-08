using Application.DTOs.Responses;

namespace Application.Interfaces
{
    public interface IWeatherService : IDisposable
    {
        Task<List<WeatherResponse>> GetAllWeather();

        Task<List<WeatherResponse>> GetAllWeatherFromProvider(string provider);

        Task<List<WeatherResponse>> GetWeatherFromCity(string city, string state, string country);
    }
}