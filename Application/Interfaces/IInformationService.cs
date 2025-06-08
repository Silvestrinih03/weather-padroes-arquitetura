using Application.DTOs.Responses;

namespace Application.Interfaces
{
    public interface IInformationService : IDisposable
    {
        Task<List<WeatherResponse>> GetAllWeather();

        Task<List<WeatherResponse>> GetAllWeatherFromProvider(string provider);

        Task<List<WeatherResponse>> GetWeatherFromCity(string city, string state, string country);
    }
}