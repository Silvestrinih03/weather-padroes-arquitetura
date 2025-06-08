using Newtonsoft.Json;

namespace Application.DTOs.Responses
{
    public class WeatherResponse
    {
        public long Id { get; set; }

        public string Provider { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public long CelsiusTemperatureMin { get; set; }

        public long CelsiusTemperatureMax { get; set; }

        public DateTime LastUpdate { get; set; }
    }
}