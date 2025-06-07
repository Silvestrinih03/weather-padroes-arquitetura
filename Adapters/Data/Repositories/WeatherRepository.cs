using Data.EF;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class WeatherRepository(AppDataContext dataContext) : IWeatherRepository
    {
        public async Task<List<Weather>> GetAllWeatherReports()
        {
            return await dataContext.Set<Weather>()
                .ToListAsync();
        }
    }
}