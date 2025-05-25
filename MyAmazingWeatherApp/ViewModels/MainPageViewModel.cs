using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using MyAmazingWeatherApp.Models;
using MyAmazingWeatherApp.Services;

namespace MyAmazingWeatherApp.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        readonly IWeatherService _weatherService;

        public MainPageViewModel(IWeatherService weatherService)
        {
            _weatherService = weatherService;

            HourlyForecasts = new ObservableCollection<HourlyForecastItem>();
            DailyForecasts = new ObservableCollection<DailyForecastItem>();

            LoadWeatherCommand = new Command<City>(async city =>
                await LoadWeatherAsync(city.Lat, city.Lon, city.Name));
        }

        // INotifyPropertyChanged boilerplate
        public event PropertyChangedEventHandler PropertyChanged;
        protected bool SetProperty<T>(ref T backingField, T value,
                                     [CallerMemberName] string propName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value)) return false;
            backingField = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
            return true;
        }

        // — Bindable properties —
        string _cityName;
        public string CityName
        {
            get => _cityName;
            set => SetProperty(ref _cityName, value);
        }

        string _currentTemperature;
        public string CurrentTemperature
        {
            get => _currentTemperature;
            set => SetProperty(ref _currentTemperature, value);
        }

        string _currentCondition;
        public string CurrentCondition
        {
            get => _currentCondition;
            set => SetProperty(ref _currentCondition, value);
        }

        bool _isDataStale;
        public bool IsDataStale
        {
            get => _isDataStale;
            set => SetProperty(ref _isDataStale, value);
        }

        // ** New detail properties **
        string _uvIndex;
        public string UvIndex
        {
            get => _uvIndex;
            set => SetProperty(ref _uvIndex, value);
        }

        string _feelsLike;
        public string FeelsLike
        {
            get => _feelsLike;
            set => SetProperty(ref _feelsLike, value);
        }

        string _pressure;
        public string Pressure
        {
            get => _pressure;
            set => SetProperty(ref _pressure, value);
        }

        string _precipitationProbability;
        public string PrecipitationProbability
        {
            get => _precipitationProbability;
            set => SetProperty(ref _precipitationProbability, value);
        }

        string _windSpeed;
        public string WindSpeed
        {
            get => _windSpeed;
            set => SetProperty(ref _windSpeed, value);
        }

        string _humidity;
        public string Humidity
        {
            get => _humidity;
            set => SetProperty(ref _humidity, value);
        }



        // Forecast collections
        public ObservableCollection<HourlyForecastItem> HourlyForecasts { get; }
        public ObservableCollection<DailyForecastItem> DailyForecasts { get; }

        // Trigger load
        public ICommand LoadWeatherCommand { get; }

        // Core loader
        async Task LoadWeatherAsync(double lat, double lon, string cityName)
        {
            CityName = cityName;
            var (forecast, stale) = await _weatherService.GetWeatherAsync(lat, lon);
            if (forecast == null) return;

            IsDataStale = stale;

            // Current temp
            CurrentTemperature = $"{forecast.Current.Temperature:0}°C";

            // If you pull weathercode, you can fill CurrentCondition (string or code)
            CurrentCondition = "";

            // Hourly (next 24h)
            HourlyForecasts.Clear();
            for (int i = 0; i < 24 && i < forecast.Hourly.Time.Length; i++)
            {
                var dt = DateTime.Parse(forecast.Hourly.Time[i]);
                var temp = forecast.Hourly.Temperature2m[i];
                HourlyForecasts.Add(new HourlyForecastItem
                {
                    Time = dt.ToString("HH:mm"),
                    Temperature = $"{temp:0}°",
                    Icon = "sun.svg" // swap in converter‐mapped icon
                });
            }

            // Daily (7‐day)
            DailyForecasts.Clear();
            for (int i = 0; i < forecast.Daily.Time.Length; i++)
            {
                var dt = DateTime.Parse(forecast.Daily.Time[i]);
                DailyForecasts.Add(new DailyForecastItem
                {
                    Day = dt.ToString("dddd"),
                    MinTemperature = $"{forecast.Daily.Temperature2mMin[i]:0}°",
                    MaxTemperature = $"{forecast.Daily.Temperature2mMax[i]:0}°",
                    Icon = "sun-cloud.svg"
                });
            }

            // ** Populate new detail props **
            // UV index is from today's daily uv_index_max (first element)
            UvIndex = $"{forecast.Daily.UvIndexMax[0]:0}";
            // Apparent temperature (feels like)
            FeelsLike = $"{forecast.Current.FeelsLike:0}°C";
            // Pressure in hPa
            Pressure = $"{forecast.Current.Pressure:0} hPa";
            // Precipitation probability from today's daily max
            PrecipitationProbability = $"{forecast.Daily.PrecipitationProbabilityMax[0]:0}%";
            Humidity = $"{forecast.Current.Humidity:0}%";
            WindSpeed = $"{forecast.Current.Windspeed10m:0} km/h";

            Debug.WriteLine($"Loaded weather for {CityName}: " +
                $"{CurrentTemperature}, {UvIndex} UV, {FeelsLike} feels like, " +
                $"{Pressure} pressure, {PrecipitationProbability} precip prob, " +
                $"{Humidity} humidity, {WindSpeed} wind speed.");
        }
    }

    // DTOs for binding
    public class HourlyForecastItem
    {
        public string Time { get; set; }
        public string Temperature { get; set; }
        public string Icon { get; set; }
    }

    public class DailyForecastItem
    {
        public string Day { get; set; }
        public string MinTemperature { get; set; }
        public string MaxTemperature { get; set; }
        public string Icon { get; set; }
    }
}
