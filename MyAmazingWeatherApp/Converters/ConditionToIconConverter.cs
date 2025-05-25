using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace MyAmazingWeatherApp.Converters
{
    public class ConditionToIconConverter : IValueConverter
    {
        // Map your condition strings (or codes) to the correct SVG/PNG filename.
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string condition)
            {
                // normalize
                var key = condition.Trim().ToLowerInvariant();
                return key switch
                {
                    "sunny" => "sun.svg",
                    "clear" => "sun.svg",
                    "partly cloudy"
                                 => "sun-cloud.svg",
                    "cloudy" => "fog_sun.svg",
                    "overcast" => "fog.svg",
                    "rain" => "rain.svg",
                    "thunderstorms"
                                 => "thunderstorms.svg",
                    "snow" => "snow.svg",
                    "fog" => "fog.svg",
                    "windy" => "wind.svg",
                    _ => "sun-cloud.svg",
                };
            }

            // Fallback if you ever bind an int weather code instead:
            if (value is int code)
            {
                // Example based on Open-Meteo codes:
                return code switch
                {
                    0 => "sun.svg",          // clear sky
                    1 => "sun-cloud.svg",     // mainly clear
                    2 => "sun-cloud.svg",     // partly cloudy
                    3 => "fog.svg",           // overcast
                    >= 45 and <= 48
                       => "fog.svg",           // fog
                    >= 51 and <= 57
                       => "rain.svg",          // drizzle
                    >= 61 and <= 67
                       => "rain.svg",          // rain
                    >= 71 and <= 77
                       => "snow.svg",          // snow
                    >= 95
                       => "thunderstorms.svg", // thunderstorm
                    _ => "sun-cloud.svg",
                };
            }

            // Default icon
            return "sun-cloud.svg";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
