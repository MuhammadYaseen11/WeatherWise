using WeatherWise.View;
namespace WeatherWise;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        //using version tracking 
        VersionTracking.Track();
        //set the landingpage as on first launch, otherwise go to mainpage
        if (VersionTracking.IsFirstLaunchEver == true)
        {
            MainPage = new LandingPage();
        }
        else
        {
            MainPage = new AppShell();
        }
    }
}
