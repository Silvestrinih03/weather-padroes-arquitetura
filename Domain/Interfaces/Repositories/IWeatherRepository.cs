using Domain.Models;

namespace Domain.Interfaces.Repositories
{
    public interface IWeatherRepository
    {
        Task<List<Weather>> GetAllWeatherReports();

        Task<Weather?> GetWeatherReportById(long id);

        Task<List<Weather>> GetWeatherReportsByLocation(string city, string state, string country);

        Task<Weather> GetWeatherReportByProviderAndLocation(string source, string city, string state, string country);

        Task AddWeatherReportAsync(Weather weather);

        Task UpdateWeatherReportAsync(Weather weather);

        Task SaveChangesAsync();
    }
}