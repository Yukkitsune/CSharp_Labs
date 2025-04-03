using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Policy;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json.Nodes;

namespace WeatherApp
{

    public partial class Form1 : Form
    {
        private readonly string apiKey = "90f3c903d03c09a887221c3c9993a558";
        private readonly string apiUrl = "https://api.openweathermap.org/data/2.5/weather";
        private readonly string cityPath = "D:\\C#_Labs\\CSharp_lab09\\CSharp_lab09\\Files\\city.txt";
        private readonly List<City> _cities = new List<City>();
        public Form1()
        {
            InitializeComponent();
            LoadCitiesAsync(cityPath);
        }
        private async Task LoadCitiesAsync(string filePath)
        {
            try
            {
                var cities = File.ReadAllLines(filePath);
                foreach (var city in cities)
                {

                    var parts = city.Split('\t');
                    if (parts.Length == 2)
                    {
                        var name = parts[0];
                        var coord = parts[1].Replace(" ", "").Split(',');
                        var latitude = Convert.ToDouble(coord[0].Replace(".", ","));
                        var longitude = Convert.ToDouble(coord[1].Replace(".", ","));
                        var info = new City(name,latitude,longitude);
                        //MessageBox.Show(info.ToString());
                        //Console.WriteLine(info.Name, info.Latitude, info.Longitude);
                        _cities.Add(info);
                        
                        
                    }
                }
                CityComboBox.DataSource = _cities;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading cities: {ex.Message}");
            }
        }

    private async void GetWeatherButton_Click(object sender, EventArgs e)
        {
            string apiUrl = "https://api.openweathermap.org/data/2.5/weather";
            if (CityComboBox.SelectedItem is City selectedCity)
            {
                try
                {
                    var weather = await FetchWeatherAsync(apiUrl,selectedCity);

                    if (weather != null)
                    {
                        //MessageBox.Show(weather.ToString());
                        ResulttextBox.Text = weather.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Failed fetching weather data. Try again later.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error occured: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please choose a city");
            }
        }
        private async Task<Weather> FetchWeatherAsync(string URL,City city)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(URL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var urlParameters = $"?lat={city.Latitude}&lon={city.Longitude}&appid={apiKey}&units=metric";
                var fullUrl = URL + urlParameters;
                var response = await client.GetAsync(fullUrl);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var json = JsonObject.Parse(responseString);
                    Weather res = new Weather();
                    res.Country = (string)json["sys"]["country"];
                    res.Name = (string)json["name"];
                    res.Temp = (double)json["main"]["temp"];
                    res.Description = (string)json["weather"][0]["main"];
                    //MessageBox.Show($"API response: {responseString}");
                    return res;

                    
                }
                else
                {
                    MessageBox.Show($"API error: {response.StatusCode}");
                }
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed fetching weather data: {ex.Message}");
                return null;
            }
        }
    }
    public class City
    {
        public string Name { get; }
        public double Latitude { get; }
        public double Longitude { get; }

        public City(string name, double latitude, double longitude)
        {
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
        }

        public override string ToString()
        {
            return Name;
        }
    }
    
    public class Weather
    {
        public string Country { get; set; }
        public string Name { get; set; }
        public double Temp { get; set; }
        public string Description { get; set; }


        public Weather(string country, string name, double temp, string description)
        {
            Country = country;
            Name = name;
            Temp = temp;
            Description = description;
        }

        public Weather()
        {
        }

        public override string ToString()
        {
            return $"Country: {Country}, City: {Name},Temperature: {Temp} °C, Description: {Description}";
        }
    }
}
