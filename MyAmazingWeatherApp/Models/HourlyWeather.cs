using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyAmazingWeatherApp.Models
{
    public class HourlyWeather
    {
        [JsonPropertyName("time")] public string[] Time { get; set; }

        [JsonPropertyName("temperature_2m")] public double[] Temperature2m { get; set; }
    }
}
