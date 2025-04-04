namespace MyAmazingWeatherApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = new Window(new MainPage());
            // window setting only for Desktop platforms (and for ease of testing)
            window.Width = 432;
            window.Height = 768;

            return window;
        }
    }
}