using Domain.Models;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

public class OpenWeatherAdapter : BaseWeatherAdapter
{
    public override string Name => "OpenWeatherMap";

    public OpenWeatherAdapter(HttpClient http, IConfiguration config)
      : base(http, config, "OpenWeather:ApiKey")
    { }

    protected override string BuildUrl(string city, string state, string country, string apiKey)
        => $"https://api.openweathermap.org/data/2.5/weather" +
           $"?q={Uri.EscapeDataString(city)},{Uri.EscapeDataString(country)}" +
           $"&appid={apiKey}" +
           $"&units=metric";

    protected override Weather ParseWeather(JsonDocument doc, string city, string state, string country)
    {
        var main = doc.RootElement.GetProperty("main");
        return new Weather
        {
            City = city,
            State = state,
            Country = country,
            CelsiusTemperatureMin = (long)main.GetProperty("temp_min").GetDouble(),
            CelsiusTemperatureMax = (long)main.GetProperty("temp_max").GetDouble(),
            LastUpdate = DateTime.UtcNow
        };
    }
}