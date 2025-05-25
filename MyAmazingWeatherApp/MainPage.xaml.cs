using System.Collections.ObjectModel;
using MyAmazingWeatherApp.ViewModels;
using MyAmazingWeatherApp.Models;

namespace MyAmazingWeatherApp
{
    public partial class MainPage : ContentPage
    {
        readonly MainPageViewModel _vm;

        public MainPage(MainPageViewModel vm)
        {
            InitializeComponent();
            BindingContext = _vm = vm;
        }

        protected override void OnAppearing() // Moved this method inside the class
        {
            base.OnAppearing();
            // Example: default to Warsaw
            _vm.LoadWeatherCommand.Execute(new City
            {
                Name = "Warsaw",
                Lat = 52.2297,
                Lon = 21.0122
            });
        }
    }
}
