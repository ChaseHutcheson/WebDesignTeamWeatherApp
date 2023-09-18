using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace WebDesignTeamWeatherApp
{
    internal class Program
    {
        static async Task GetWeatherData()
        {

            using (HttpClient client = new HttpClient())
            {
                // Specify the correct BaseAddress with protocol and domain
                client.BaseAddress = new Uri("https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&hourly=apparent_temperature,precipitation_probability&current_weather=true&temperature_unit=fahrenheit&windspeed_unit=mph&precipitation_unit=inch&timezone=auto&forecast_days=14");

                try
                {
                    // Build the query string with parameters
    

                    HttpResponseMessage response = await client.GetAsync(client.BaseAddress);

                    if (response.IsSuccessStatusCode)
                    {
                        string response_body = await response.Content.ReadAsStringAsync();

                        var options = new JsonSerializerOptions
                        {
                            WriteIndented = true, // Format the JSON with indentation
                        };
                        JsonDocument jsonDocument = JsonDocument.Parse(response_body);

                        // Serialize the parsed JSON document back to a formatted string
                        string formattedJson = JsonSerializer.Serialize(jsonDocument.RootElement, options);

                        // Print the formatted JSON
                        Console.WriteLine(formattedJson);
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

            Console.ReadKey(); // You can keep this if you want to wait for a key press after displaying the data.
        }
    }
}
