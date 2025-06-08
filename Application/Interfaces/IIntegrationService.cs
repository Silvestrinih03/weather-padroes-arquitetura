using Application.DTOs.Requests;

namespace Application.Interfaces
{
    public interface IIntegrationService : IDisposable
    {
        Task FetchAndSaveWeatherToLocalDatabase(List<WeatherRequest> weatherRequest);
    }
}