using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace MyAmazingWeatherApp.Converters
{
    public class ConditionToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string condition)
            {
                var key = condition.Trim().ToLowerInvariant();
                return key switch
                {
                    "sunny" => "sun.png",
                    "clear" => "sun.png",
                    "partly cloudy" => "sun_cloud.png",
                    "cloudy" => "cloud.png",
                    "overcast" => "cloud.png",
                    "rain" => "rain.png",
                    "thunderstorms" => "thunderstorms.png",
                    "snow" => "snow.png",
                    "fog" => "fog.png",
                    "windy" => "wind.png",
                    "drizzle" => "rain.png",
                    _ => "sun_cloud.png",
                };
            }

            if (value is int code)
            {
                return code switch
                {
                    0 => "sun.png",
                    1 => "sun_cloud.png",
                    2 => "sun_cloud.png",
                    3 => "cloud.png",
                    >= 45 and <= 48
                       => "fog.png",
                    >= 51 and <= 57
                       => "rain.png",
                    >= 61 and <= 67
                       => "rain.png",
                    >= 71 and <= 77
                       => "snow.png",
                    >= 95
                       => "thunderstorms.png",
                    _ => "sun_cloud.png",
                };
            }
            return "sun_cloud.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
