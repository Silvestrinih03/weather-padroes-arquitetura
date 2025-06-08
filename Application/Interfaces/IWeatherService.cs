using Application.DTOs.Responses;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IWeatherService : IDisposable
    {
        Task<List<WeatherResponse>> GetWeatherFromABC();

        Task<List<WeatherResponse>> GetWeatherFromDFG();
    }
}