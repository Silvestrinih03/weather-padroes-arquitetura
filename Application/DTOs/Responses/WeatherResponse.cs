using Newtonsoft.Json;

namespace Application.DTOs.Responses
{
    public class WeatherResponse
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}