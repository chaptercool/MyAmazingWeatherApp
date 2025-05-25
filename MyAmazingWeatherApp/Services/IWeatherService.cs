using System.Threading.Tasks;
using MyAmazingWeatherApp.Models;

namespace MyAmazingWeatherApp.Services
{
    public interface IWeatherService
    {
        /// <summary>
        /// Fetches fresh data for the given lat/lon if online; 
        /// otherwise returns the last‐cached data (if any) and marks it stale.
        /// </summary>
        /// <param name="lat">Latitude</param>
        /// <param name="lon">Longitude</param>
        /// <returns>
        /// A tuple of (WeatherForecast?, bool isDataStale).
        /// If forecast is null, no data is available (first launch offline).
        /// </returns>
        Task<(WeatherForecast forecast, bool isDataStale)> GetWeatherAsync(double lat, double lon);
    }
}
