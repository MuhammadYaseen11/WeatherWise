using Newtonsoft.Json;
using WeatherWise.Model;

namespace WeatherWise.Service
{
    public static class ApiService
    {
        public static async Task<Root> GetWeather(double latitude, double longitude)
        {
            // Initialize an instance of HttpClient
            HttpClient client = new HttpClient();

            // Construct the URL with latitude and longitude
            string requestUrl = $"https://api.openweathermap.org/data/2.5/forecast?lat={latitude}&lon={longitude}&appid=382a40977f954e1ec3b76c137bd17757";

            // Fetch the JSON string asynchronously using HttpClient
            string jsonResponse = await client.GetStringAsync(requestUrl);

            // Deserialize the JSON string to a Root object
            return JsonConvert.DeserializeObject<Root>(jsonResponse);
        }

        public static async Task<Root> GetWeatherByCity(string city)
        {
            // Create a new HttpClient object
            HttpClient client = new HttpClient();

            // Build the request URL with the city parameter
            string apiUrl = $"https://api.openweathermap.org/data/2.5/forecast?q={city}&appid=382a40977f954e1ec3b76c137bd17757";

            // Retrieve the weather data as a JSON string
            string responseString = await client.GetStringAsync(apiUrl);

            // Convert the JSON response into a Root object
            return JsonConvert.DeserializeObject<Root>(responseString);
        }


    }
}
