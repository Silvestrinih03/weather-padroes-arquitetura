using Domain.Models;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

public class WeatherBitAdapter : BaseWeatherAdapter
{
    public override string Name => "WeatherBit";

    public WeatherBitAdapter(HttpClient http, IConfiguration config)
      : base(http, config, "WeatherBit:ApiKey")
    { }

    protected override string BuildUrl(string city, string state, string country, string apiKey)
        => "https://api.weatherbit.io/v2.0/forecast/hourly" +
           $"?city={Uri.EscapeDataString(city)}" +
           (string.IsNullOrEmpty(state)
               ? ""
               : $"&state={Uri.EscapeDataString(state)}") +
           $"&country={Uri.EscapeDataString(country)}" +
           $"&hours=24" +
           $"&key={apiKey}";

    protected override Weather ParseWeather(JsonDocument doc, string city, string state, string country)
    {
        var temps = doc.RootElement
                       .GetProperty("data")
                       .EnumerateArray()
                       .Select(el => el.GetProperty("temp").GetDouble())
                       .ToList();

        double minTemp = temps.Min();
        double maxTemp = temps.Max();

        return new Weather
        {
            City = city,
            State = state,
            Country = country,
            CelsiusTemperatureMin = (long)Math.Round(minTemp),
            CelsiusTemperatureMax = (long)Math.Round(maxTemp),
            LastUpdate = DateTime.UtcNow
        };
    }
}