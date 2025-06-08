using Application.DTOs.Requests;
using Application.Interfaces;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public sealed class IntegrationService : IIntegrationService
    {
        private readonly IWeatherRepository _weatherRepository;
        private readonly ILogger _logger;
        private readonly IEnumerable<IWeatherProviderAdapter> _adapters;

        public IntegrationService(
            IWeatherRepository weatherRepository,
            ILogger logger,
            IEnumerable<IWeatherProviderAdapter> adapters)
        {
            _weatherRepository = weatherRepository;
            _logger = logger;
            _adapters = adapters;
        }

        public async Task FetchAndSaveWeatherToLocalDatabase(List<WeatherRequest> weatherRequests)
        {
            foreach (var request in weatherRequests)
            {
                Weather weather;

                foreach (var adapter in _adapters)
                {
                    try
                    {
                        weather = await adapter.GetCurrentAsync(request.City, request.State, request.Country);

                        if (weather is null)
                            continue;

                        try
                        {
                            await _weatherRepository.AddOrUpdateWeatherReportAsync(weather);

                            await _weatherRepository.SaveChangesAsync();

                            _logger.LogInformation($"Clima salvo para {weather.City}, {weather.State} ({weather.Provider})");
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, $"Erro ao salvar clima no banco para {weather.City}, {weather.State}");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, $"Erro com o adapter '{adapter.Name}' para {request.City}/{request.State}/{request.Country}");
                    }
                }
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}