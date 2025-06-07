using Application.DTOs.Responses;

namespace Application.Interfaces
{
    public interface IWeatherService : IDisposable
    {
        Task<List<WeatherResponse>> GetWeatherFromABC();

        Task<List<WeatherResponse>> GetWeatherFromDFG();
    }
}