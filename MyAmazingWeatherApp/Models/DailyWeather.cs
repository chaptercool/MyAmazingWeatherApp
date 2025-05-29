using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyAmazingWeatherApp.Models
{
    public class DailyWeather
    {
        [JsonPropertyName("time")] public string[] Time { get; set; }

        [JsonPropertyName("uv_index_max")] public double[] UvIndexMax { get; set; }

        [JsonPropertyName("temperature_2m_max")] public double[] Temperature2mMax { get; set; }

        [JsonPropertyName("temperature_2m_min")] public double[] Temperature2mMin { get; set; }

        [JsonPropertyName("precipitation_probability_max")] public double[] PrecipitationProbabilityMax { get; set; }

        [JsonPropertyName("weather_code")] public int[] WeatherCode { get; set; }
    }
}
