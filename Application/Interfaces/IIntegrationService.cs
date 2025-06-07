using Application.DTOs.Requests;

namespace Application.Interfaces
{
    public interface IIntegrationService : IDisposable
    {
        Task SaveWeatherToLocalDatabase(List<WeatherRequest> weatherRequest);
    }
}