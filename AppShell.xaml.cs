namespace WeatherWise
{

    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(View.MainPage), typeof(View.MainPage));
            Routing.RegisterRoute(nameof(View.SavedPage), typeof(View.SavedPage));

        }
    }
}