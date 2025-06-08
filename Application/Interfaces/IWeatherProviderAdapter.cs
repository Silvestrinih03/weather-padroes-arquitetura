using Domain.Models;

namespace Application.Interfaces
{
    public interface IWeatherProviderAdapter
    {
        Task<Weather> GetCurrentAsync(string city, string state, string country);

        string Name { get; }
    }
}