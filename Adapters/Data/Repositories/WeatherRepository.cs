using Data.EF;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class WeatherRepository(AppDataContext dataContext) : IWeatherRepository
    {
        public async Task<List<Weather>> GetAllWeatherReports()
            => await dataContext.Set<Weather>()
                                .ToListAsync();

        public async Task<Weather?> GetWeatherReportById(long id)
            => await dataContext.Set<Weather>()
                                .FindAsync(id)
                                .AsTask();

        public async Task<List<Weather>> GetWeatherReportsByLocation(string city, string state, string country)
            => await dataContext.Set<Weather>()
                                .Where(w =>
                                    w.City == city &&
                                    w.State == state &&
                                    w.Country == country)
                                .ToListAsync();

        public async Task<Weather> GetWeatherReportByProviderAndLocation(string provider, string city, string state, string country)
            => await dataContext.Set<Weather>()
                                .Where(w =>
                                    w.Provider == provider &&
                                    w.City == city &&
                                    w.State == state &&
                                    w.Country == country)
                                .FirstOrDefaultAsync();

        public async Task AddWeatherReportAsync(Weather weather)
            => await dataContext.Set<Weather>()
                                .AddAsync(weather);

        public Task UpdateWeatherReportAsync(Weather weather)
        {
            dataContext.Set<Weather>().Update(weather);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
            => await dataContext.SaveChangesAsync();
    }
}
