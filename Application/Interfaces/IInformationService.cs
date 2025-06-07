using Application.DTOs.Responses;

namespace Application.Interfaces
{
    public interface IInformationService : IDisposable
    {
        Task<List<WeatherResponse>> GetWeather();
    }
}