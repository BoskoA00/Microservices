using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using AdsServer.Data;

namespace AdsServer.DataSerivces
{
    public class UserCommandHttp : IUserCommandHttp
    {
        private readonly HttpClient httpClient;

        public UserCommandHttp(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<User?> getUserById(int id)
        {
            //7204-9090
            var response = await httpClient.GetAsync($"https://localhost:7204/userPickUpById/{id}");

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("JSON response: " + jsonResponse);

                    if (string.IsNullOrEmpty(jsonResponse))
                    {
                        Console.WriteLine("Response content is empty.");
                        return null;
                    }

                    User? user = JsonSerializer.Deserialize<User>(jsonResponse);
                    return user;
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"JSON deserialization error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"Failed to fetch user. Status code: {response.StatusCode}");
            }
            return null;
        }
    }
    }
