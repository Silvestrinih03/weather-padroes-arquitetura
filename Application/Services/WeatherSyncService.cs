using Application.Interfaces;
using Domain.Interfaces.Repositories;

public class WeatherSyncService
{
    private readonly IEnumerable<IWeatherProviderAdapter> _providers;
    private readonly IWeatherRepository _weatherRepository;

    public WeatherSyncService(
        IEnumerable<IWeatherProviderAdapter> providers,
        IWeatherRepository weatherRepository)
    {
        _providers = providers;
        _weatherRepository = weatherRepository;
    }

    public async Task SyncAllAsync(string city, string state, string country)
    {
        foreach (var prov in _providers)
        {
            var weather = await prov.GetCurrentAsync(city, state, country);

            var exists = await _weatherRepository
                .GetWeatherReportByProviderAndLocation(prov.Name, city, state, country);

            if (exists is null)
                await _weatherRepository.AddWeatherReportAsync(weather);
            else
            {
                exists.CelsiusTemperatureMin = weather.CelsiusTemperatureMin;
                exists.CelsiusTemperatureMax = weather.CelsiusTemperatureMax;
                exists.LastUpdate = weather.LastUpdate;

                await _weatherRepository.UpdateWeatherReportAsync(exists);
            }
        }
        await _weatherRepository.SaveChangesAsync();
    }
}