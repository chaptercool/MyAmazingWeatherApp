using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyAmazingWeatherApp.Models
{
    public class CurrentWeather
    {
        [JsonPropertyName("time")] public DateTimeOffset Time { get; set; }
        [JsonPropertyName("temperature_2m")] public double Temperature { get; set; }
        [JsonPropertyName("relative_humidity_2m")] public int Humidity { get; set; }
        [JsonPropertyName("apparent_temperature")] public double FeelsLike { get; set; }
        [JsonPropertyName("pressure_msl")] public double Pressure { get; set; }
        [JsonPropertyName("windspeed_10m")] public double Windspeed10m { get; set; }
        [JsonPropertyName("weather_code")] public int? WeatherCode { get; set; }
    }
}
