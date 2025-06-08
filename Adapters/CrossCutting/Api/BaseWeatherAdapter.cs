using Application.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

public abstract class BaseWeatherAdapter : IWeatherProviderAdapter
{
    public abstract string Name { get; }

    private readonly HttpClient _http;
    private readonly string _apiKey;

    protected BaseWeatherAdapter(HttpClient http, IConfiguration config, string configKey)
    {
        _http = http;
        _apiKey = config[configKey]
                  ?? throw new ArgumentException($"Configuration key '{configKey}' not found");
    }

    protected abstract string BuildUrl(string city, string state, string country, string apiKey);

    protected abstract Weather ParseWeather(JsonDocument doc, string city, string state, string country);

    public async Task<Weather> GetCurrentAsync(string city, string state, string country)
    {
        var url = BuildUrl(city, state, country, _apiKey);
        using var response = await _http.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception(
              $"{Name} call failed ({response.StatusCode}): {error}"
            );
        }

        using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var weather = ParseWeather(json, city, state, country);
        weather.Provider = Name;
        return weather;
    }
}