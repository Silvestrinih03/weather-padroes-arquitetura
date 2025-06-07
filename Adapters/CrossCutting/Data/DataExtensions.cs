using Data.EF;
using Data.Repositories;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.Data
{
    public static class DataExtensions
    {
        public static void RegisterData(this IServiceCollection services, IConfiguration configuration)
        {
            AddDatabase(services, configuration);

            AddRepositories(services);
        }

        private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var defaultConnectionString = configuration.GetConnectionString("MySQLConnection");
            _ = services.AddDbContext<AppDataContext>(options => options.UseMySql(defaultConnectionString, ServerVersion.AutoDetect(defaultConnectionString), mysqlOptions => mysqlOptions.EnableRetryOnFailure()));
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            _ = services.AddScoped<IWeatherRepository, WeatherRepository>();
        }
    }
}