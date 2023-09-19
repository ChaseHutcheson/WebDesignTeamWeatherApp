using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace WebDesignTeamWeatherApp
{
    internal class Program
    {
        static async Task GetWeatherData()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://api.weatherapi.com/v1/forecast.json?key=37d68cfe17b446eea21145120231909&q=London&days=10");

                try
                {
                    HttpResponseMessage response = await client.GetAsync(client.BaseAddress);

                    if (response.IsSuccessStatusCode)
                    {
                        string response_body = await response.Content.ReadAsStringAsync();

                        JObject jsonResponse = JObject.Parse(response_body);

                        JToken weather = jsonResponse["forecast"]["forecastday"];

                        for (int i = 0; i < 10; i++)
                        {
                            var weather_object = weather[i];
                            Console.WriteLine($"{weather_object["date"]} will have a max temp of {weather_object["day"]["maxtemp_f"]} degrees fahrenhiet \nwith a min temp of {weather_object["day"]["mintemp_f"]} degrees fahrenhiet. \nThere will also be a {weather_object["day"]["daily_chance_of_rain"]}% chance of rain\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"API call failed with status code {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"!!!!!An Error occurred!!!!! \n\n\n{ex.Message}");
                }
            }
        }

        static async Task Main(string[] args)
        {
            await GetWeatherData();
            Console.ReadKey();
        }
    }
}
