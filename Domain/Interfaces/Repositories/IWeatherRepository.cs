using Domain.Models;

namespace Domain.Interfaces.Repositories
{
    public interface IWeatherRepository
    {
        Task<List<Weather>> GetAllWeatherReports();
    }
}