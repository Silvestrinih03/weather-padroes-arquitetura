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
        => $"https://api.weatherbit.io/v2.0/current" +
           $"?city={Uri.EscapeDataString(city)}" +
           $"&country={Uri.EscapeDataString(country)}" +
           $"&key={apiKey}";

    protected override Weather ParseWeather(JsonDocument doc, string city, string state, string country)
    {
        var data = doc.RootElement
                      .GetProperty("data")[0];

        var temp = data.GetProperty("temp").GetDouble();
        return new Weather
        {
            City = city,
            State = state,
            Country = country,
            CelsiusTemperatureMin = (long)temp,
            CelsiusTemperatureMax = (long)temp,
            LastUpdate = DateTime.UtcNow
        };
    }
}