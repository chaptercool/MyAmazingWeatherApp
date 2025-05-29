using System;
using System.Globalization;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace MyAmazingWeatherApp.Converters
{
    public class ConditionToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string resourceKey = GetResourceKeyForWeather(value);

            if (!string.IsNullOrEmpty(resourceKey))
            {
                var startKey = $"{resourceKey}BgStart";
                var endKey = $"{resourceKey}BgEnd";

                if (Application.Current.Resources.TryGetValue(startKey, out var startObj)
                 && Application.Current.Resources.TryGetValue(endKey, out var endObj)
                 && startObj is Color startColor
                 && endObj is Color endColor)
                {
                    return new LinearGradientBrush(
                        new GradientStopCollection {
                            new GradientStop(startColor, 0.1f),
                            new GradientStop(endColor, 1.0f),
                        },
                        new Point(0, 0),
                        new Point(0, 1)
                    );
                }
            }

            return new LinearGradientBrush(
                new GradientStopCollection {
                    new GradientStop(Color.FromArgb("#90D5FF"), 0.1f),
                    new GradientStop(Color.FromArgb("#08C0FF"), 1f)
                },
                new Point(0, 0), new Point(0, 1)
            );
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();

        private string GetResourceKeyForWeather(object value)
        {
            if (value is int code)
            {
                return GetResourceKeyFromWeatherCode(code);
            }
            
            if (value is string condition)
            {
                var key = condition.Trim().ToLowerInvariant();
                return key switch
                {
                    "sunny" or "clear" => "Sunny",
                    "partly cloudy" => "PartlyCloudy",
                    "cloudy" => "Cloudy",
                    "overcast" => "Overcast",
                    "rain" or "drizzle" or "showers" => "Rainy",
                    "thunderstorms" => "Thunderstorm",
                    "snow" or "sleet" => "Snowy",
                    "fog" or "mist" => "Foggy",
                    "windy" => "Windy",
                    _ => "Default",
                };
            }
            
            return "Default";
        }

        private string GetResourceKeyFromWeatherCode(int code)
        {
            return code switch
            {
                0 => "Sunny",
                1 => "PartlyCloudy",
                2 => "PartlyCloudy",
                3 => "Overcast",
                >= 45 and <= 48 => "Foggy",
                >= 51 and <= 57 => "Rainy",
                >= 61 and <= 67 => "Rainy",
                >= 71 and <= 77 => "Snowy",
                >= 80 and <= 82 => "Rainy",
                >= 85 and <= 86 => "Snowy",
                >= 95 and <= 99 => "Thunderstorm",
                _ => "Default",
            };
        }
    }
}
