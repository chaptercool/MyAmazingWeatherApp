using System.Collections.ObjectModel;

namespace MyAmazingWeatherApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public class WeatherItem
        {
            public string Temperature { get; set; }
            public string Icon { get; set; }
            public string Time { get; set; }
        }

        public ObservableCollection<WeatherItem> WeatherItems { get; set; } = new ObservableCollection<WeatherItem>
        {
            new WeatherItem { Temperature = "6°", Icon = "burza.png", Time = "12:00" },
            new WeatherItem { Temperature = "7°", Icon = "mgla_slonce.png", Time = "13:00" },
            new WeatherItem { Temperature = "8°", Icon = "snieg.png", Time = "14:00" },
            new WeatherItem { Temperature = "10°", Icon = "slonce.png", Time = "15:00" },
            new WeatherItem { Temperature = "9°", Icon = "deszcz.png", Time = "16:00" },
            new WeatherItem { Temperature = "12°", Icon = "snieg.png", Time = "17:00" },
            new WeatherItem { Temperature = "12°", Icon = "snieg.png", Time = "18:00" },
            new WeatherItem { Temperature = "12°", Icon = "snieg.png", Time = "19:00" },
            new WeatherItem { Temperature = "12°", Icon = "snieg.png", Time = "20:00" },
            new WeatherItem { Temperature = "12°", Icon = "snieg.png", Time = "21:00" },
        };

    }

}
