using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Networking;
using MyAmazingWeatherApp.Models;
using System.Globalization;
using System.Diagnostics;

namespace MyAmazingWeatherApp.Services
{
    public class WeatherService : IWeatherService
    {
        const string CacheFileName = "weathercache.json";
        readonly HttpClient _http = new HttpClient();
        readonly JsonSerializerOptions _opts = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<(WeatherForecast forecast, bool isDataStale)> GetWeatherAsync(double lat, double lon)
        {
            var cachePath = Path.Combine(FileSystem.AppDataDirectory, CacheFileName);

            var latS = lat.ToString(CultureInfo.InvariantCulture);
            var lonS = lon.ToString(CultureInfo.InvariantCulture);

            var url = $"https://api.open-meteo.com/v1/forecast" +
                      $"?latitude={latS}&longitude={lonS}" +
                      $"&daily=uv_index_max,temperature_2m_max,temperature_2m_min,precipitation_probability_max" +
                      $"&hourly=temperature_2m" +
                      $"&current=temperature_2m,relative_humidity_2m,apparent_temperature,pressure_msl,windspeed_10m";

            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    var json = await _http.GetStringAsync(url);
                    var forecast = JsonSerializer.Deserialize<WeatherForecast>(json, _opts);

                    // cache it
                    await File.WriteAllTextAsync(cachePath, json);
                    return (forecast, false);
                }
                throw new Exception("No internet");
                Debug.WriteLine("FAILED to fetch weather");
            }
            catch
            {
                if (File.Exists(cachePath))
                {
                    var cachedJson = await File.ReadAllTextAsync(cachePath);
                    var forecast = JsonSerializer.Deserialize<WeatherForecast>(cachedJson, _opts);
                    return (forecast, true);
                    Debug.WriteLine("Using cached weather data");
                }
                return (null, true);
            }
        }
    }
}
