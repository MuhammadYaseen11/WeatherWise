namespace WeatherWise.View;
public partial class LandingPage : ContentPage
{
	public LandingPage()
	{
		InitializeComponent();
	}

    private void StartButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new AppShell());
    }
}