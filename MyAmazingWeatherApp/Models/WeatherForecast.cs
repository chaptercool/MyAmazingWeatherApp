using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyAmazingWeatherApp.Models
{
    public class WeatherForecast
    {
        [JsonPropertyName("current")] public CurrentWeather Current { get; set; }
        [JsonPropertyName("hourly")] public HourlyWeather Hourly { get; set; }
        [JsonPropertyName("daily")] public DailyWeather Daily { get; set; }
    }
}
