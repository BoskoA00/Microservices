using Microsoft.AspNetCore.Http;
using System.Text.Json;
using UserServer.Data;

namespace UserServer.DataServices
{
    public class AdsHttpClient : IAdsHttpClient
    {
        private readonly HttpClient _httpClient;

        public AdsHttpClient(HttpClient hc)
        {
            _httpClient = hc;
        }


        public async Task<List<Ad>?> DeleteAdsWithUser(int userId)
        {
            //7034-8080
            var httpResponse = await _httpClient.DeleteAsync($"https://localhost:7034/byUser/{userId}"
);
            try
            {
                var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
                List<Ad>? ads = JsonSerializer.Deserialize<List<Ad>>(jsonResponse);
                return ads;
            }
            catch (Exception ex)
            {
                Console.Write("5:"+ex.Message);
                return null;
            }
        }

        public async Task<List<Ad>?> GetAdsByUser(int userId)
        {
            //7034-8080
            var httpResponse = await _httpClient.GetAsync($"https://localhost:7034/byUser/{userId}");

            try
            {
                var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
                Console.WriteLine($"JSON Response: {jsonResponse}");

                if (string.IsNullOrEmpty(jsonResponse))
                {
                    Console.WriteLine("Response content is empty.");
                    return null;
                }

                List<Ad>? ads = JsonSerializer.Deserialize<List<Ad>?>(jsonResponse);
                return ads;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during deserialization: {ex.Message}");
                return null;
            }
        }

    }
}

