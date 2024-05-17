//following code is taken from Openweatherapi
using Newtonsoft.Json;



namespace WeatherWise.Model
{

    public class City
    {
        public int id { get; set; }
        public string name { get; set; }
        public Coord coord { get; set; }
        public string country { get; set; }
        public int population { get; set; }
        public int timezone { get; set; }
        public int sunrise { get; set; }
        [JsonIgnore]
        public int sunset { get; set; }

    }

    public class Clouds
    {
        public int all { get; set; }
    }

    public class Coord
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }

    public class List
    {
        public int dt { get; set; }
        public string dateTime
        {
            get
            {
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(dt);
                DateTime utcDateTime = dateTimeOffset.UtcDateTime;
                return utcDateTime.ToString("h tt");
            }
        }
        public Main main { get; set; }
        public List<Weather> weather { get; set; }
        public Clouds clouds { get; set; }
        public Wind wind { get; set; }
        public City city { get; set; }
        public int visibility { get; set; }
        public double pop { get; set; }
        public Rain rain { get; set; }
        public Sys sys { get; set; }
        public string dt_txt { get; set; }
    }

    public class Main
    {
        //public double temp { get; set; }
        private float _temp;
        public float temp
        {
            get { return _temp; }
            set { _temp = (float)(value - 273.15); } // Convert Kelvin to Celsius
        }
        public double feels_like { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
        public int pressure { get; set; }
        public int sea_level { get; set; }
        public int grnd_level { get; set; }
        public int humidity { get; set; }
        public int speed { get; set; }
        public double temp_kf { get; set; }
    }

    public class Rain
    {
        [JsonProperty("3h")]
        public double _3h { get; set; }
    }

    public class Root
    {
        public string cod { get; set; }
        public int message { get; set; }
        public int cnt { get; set; }
        public List<List> list { get; set; }
        public City city { get; set; }
    }

    public class Sys
    {
        public string pod { get; set; }
    }

    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
        public string customIcon => $"img_{icon}.png";

    }

    public class Wind
    {
        public double speed { get; set; }
        public int deg { get; set; }
        public double gust { get; set; }
    }



}
