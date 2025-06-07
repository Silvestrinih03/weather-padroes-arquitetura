namespace Domain.Models
{
    public class Weather
    {
        public long Id { get; set; }

        public string Source { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public long CelsiusTemperatureMin { get; set; }

        public long CelsiusTemperatureMax { get; set; }

        public DateTime LastUpdate { get; set; }
    }
}