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
using MyAmazingWeatherApp.Converters;

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

        public event PropertyChangedEventHandler PropertyChanged;
        protected bool SetProperty<T>(ref T backingField, T value,
                                     [CallerMemberName] string propName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value)) return false;
            backingField = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
            return true;
        }

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

        public ObservableCollection<HourlyForecastItem> HourlyForecasts { get; }
        public ObservableCollection<DailyForecastItem> DailyForecasts { get; }

        public ICommand LoadWeatherCommand { get; }

        async Task LoadWeatherAsync(double lat, double lon, string cityName)
        {
            CityName = cityName;
            var (forecast, stale) = await _weatherService.GetWeatherAsync(lat, lon);
            if (forecast == null) return;

            IsDataStale = stale;

            CurrentTemperature = $"{forecast.Current.Temperature:0}°C";

            CurrentCondition = "";

            HourlyForecasts.Clear();
            for (int i = 0; i < 24 && i < forecast.Hourly.Time.Length; i++)
            {
                var dt = DateTime.Parse(forecast.Hourly.Time[i]);
                var temp = forecast.Hourly.Temperature2m[i];
                HourlyForecasts.Add(new HourlyForecastItem
                {
                    Time = dt.ToString("HH:mm"),
                    Temperature = $"{temp:0}°",
                    Icon = "sun.png"
                });
            }

            DailyForecasts.Clear();
            for (int i = 0; i < forecast.Daily.Time.Length; i++)
            {
                var dt = DateTime.Parse(forecast.Daily.Time[i]);
                DailyForecasts.Add(new DailyForecastItem
                {
                    Day = dt.ToString("dddd"),
                    MinTemperature = $"{forecast.Daily.Temperature2mMin[i]:0}°",
                    MaxTemperature = $"{forecast.Daily.Temperature2mMax[i]:0}°",
                    Icon = "sun-cloud.png"
                });
            }

            UvIndex = $"{forecast.Daily.UvIndexMax[0]:0}";
            FeelsLike = $"{forecast.Current.FeelsLike:0}°C";
            Pressure = $"{forecast.Current.Pressure:0} hPa";
            PrecipitationProbability = $"{forecast.Daily.PrecipitationProbabilityMax[0]:0}%";
            Humidity = $"{forecast.Current.Humidity:0}%";
            WindSpeed = $"{forecast.Current.Windspeed10m:0} km/h";

            Debug.WriteLine($"Loaded weather for {CityName}: " +
                $"{CurrentTemperature}, {UvIndex} UV, {FeelsLike} feels like, " +
                $"{Pressure} pressure, {PrecipitationProbability} precip prob, " +
                $"{Humidity} humidity, {WindSpeed} wind speed.");
        }
    }

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
