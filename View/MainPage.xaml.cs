using Service;
using Newtonsoft.Json;
using WeatherWise.Service;

namespace WeatherWise.View;

public partial class MainPage : ContentPage
{
    public List<Model.List> WeatherList;
    private double latitude, longitude;
    public MainPage()
    {
        InitializeComponent();
        WeatherList = new List<Model.List>();
    }

    //weather to change upon loading this page
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await RetrieveLocation();
        await FetchWeatherByLocation(latitude, longitude);
    }

    private async void TapLocation_Tapped(object sender, EventArgs e)
    {
        HapticFeedbackHelper.PerformHapticFeedback();
        await RetrieveLocation();
        await FetchWeatherByLocation(latitude, longitude);
    }

    public async Task RetrieveLocation()
    {
        var location = await Geolocation.GetLocationAsync();
        latitude = location.Latitude;
        longitude = location.Longitude;
    }


    private async void SearchButton_Clicked(object sender, EventArgs e)
    {
        HapticFeedbackHelper.PerformHapticFeedback();
        await QueryCityForWeather(); // Replace the direct call to DisplayPromptAsync with this method
    }

    public async Task FetchWeatherByLocation(double latitude, double longitude)
    {
        var result = await ApiService.GetWeather(latitude, longitude);
        RefreshUI(result);


    }
    private async Task QueryCityForWeather()
    {
        string response = await DisplayPromptAsync("Search Weather", "Enter the name of the city:", accept: "Search", cancel: "Cancel", placeholder: "City name");
        if (!string.IsNullOrWhiteSpace(response))
        {
            await RetrieveWeatherByCity(response);
        }
    }
    private bool IsValidCityName(string cityName)
    {
        return !string.IsNullOrWhiteSpace(cityName) && cityName.All(c => char.IsLetter(c) || c == ' ');
    }
    public async Task RetrieveWeatherByCity(string city)
    {
        // Ensure the city name is properly formatted before making the API request.
        if (!IsValidCityName(city))
        {
            await DisplayAlert("Invalid Input", "Please enter a valid city name.", "OK");
            await QueryCityForWeather();
            return;
        }

        try
        {
            // Attempt to retrieve weather data for the specified city.
            var result = await ApiService.GetWeatherByCity(city);
            if (result != null && result.cod == "200")
            {
                // Successfully retrieved data, update the UI accordingly.
                RefreshUI(result);
            }
            else
            {
                // The API did not return a successful status code.
                await DisplayAlert("City Not Found", "Please enter a valid city.", "OK");
                await QueryCityForWeather();
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions that occur during the API call.
            await DisplayAlert("Error", "An error occurred while fetching weather data: " + ex.Message, "OK");
            await QueryCityForWeather(); // Optionally prompt again to recover from the error.
        }
    }
    private async void BookmarkButton_Clicked(object sender, EventArgs e) 
    {
        HapticFeedbackHelper.PerformHapticFeedback();
        var currentWeather = WeatherList.FirstOrDefault(); // Assuming this holds current weather data
        if (currentWeather != null)
        {
            var bookmarkJson = JsonConvert.SerializeObject(currentWeather);
            await Shell.Current.GoToAsync($"{nameof(SavedPage)}?bookmark={bookmarkJson}");
        }
    }

    private void OnHowToUseClicked(object sender, EventArgs e)
    {
        HapticFeedbackHelper.PerformHapticFeedback();
        DisplayAlert("How to Use", "This page allows you to view the current weather conditions for your location or a city of your choice. Tap Location icon on top left to fetch weather for your current location, or click on the Search icon to enter a city. Click the bookmark icon to add data to your journal. The weather details will display below, including temperature, humidity, and atmospheric pressure. Swipe down to see upcoming weather updates of 5 day forecast with a 3-hours gap", "OK");
    }
    public void RefreshUI(dynamic result)
    {
        WeatherList.Clear();
        foreach (var item in result.list)
        {
            WeatherList.Add(item);
        }

        // to referesh the collection view
        WeatherCollection.ItemsSource = null;
        WeatherCollection.ItemsSource = WeatherList;

        LabelCity.Text = result.city.name;
        LabelWeatherDescription.Text = result.list[0].weather[0].description;

        // Format the temperature to round it and append the degree symbol and "C"
        LabelTemperature.Text = $"{Math.Round(result.list[0].main.temp)}°C";

        LabelHumidity.Text = result.list[0].main.humidity + "%";
        LabelPressure.Text = result.list[0].main.pressure + " mbar";
        WeatherIcon.Source = result.list[0].weather[0].customIcon;
    }

}
