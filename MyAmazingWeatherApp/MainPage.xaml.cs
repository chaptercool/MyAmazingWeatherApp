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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _vm.LoadWeatherCommand.Execute(new City
            {
                Name = "Rzeszow",
                Lat = 50.0333,
                Lon = 22.0000
            });
        }
    }
}
