using Application.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

public class OpenWeatherAdapter : IWeatherProviderAdapter
{
    public string Name => "OpenWeatherMap";

    private readonly HttpClient _http;
    private readonly string _apiKey;

    public OpenWeatherAdapter(HttpClient http, IConfiguration config)
    {
        _http = http;
        _apiKey = config["OpenWeather:ApiKey"];
    }

    public async Task<Weather> GetCurrentAsync(string city, string state, string country)
    {
        var url = $"https://api.openweathermap.org/data/2.5/weather?q={city},{state},{country}&appid={_apiKey}&units=metric";
        var json = await _http.GetStringAsync(url);
        using var doc = JsonDocument.Parse(json);
        var main = doc.RootElement.GetProperty("main");

        return new Weather
        {
            Provider = Name,
            City = city,
            State = state,
            Country = country,
            CelsiusTemperatureMin = (long)main.GetProperty("temp_min").GetDouble(),
            CelsiusTemperatureMax = (long)main.GetProperty("temp_max").GetDouble(),
            LastUpdate = DateTime.UtcNow
        };
    }
}