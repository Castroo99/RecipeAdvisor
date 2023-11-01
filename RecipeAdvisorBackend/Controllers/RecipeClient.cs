using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace RecipeAdvisorBackend.Controllers
{
    public class RecipeClient
    {
        private readonly HttpClient _client;
        private const string JsonContentType = "application/json";

        public RecipeClient(HttpClient? client = null)
        {
            if (client != null)
                _client = client;
            else
                _client = new HttpClient();
        }

        public async Task<T> GetRecipesAsync<T>(List<string> ingredients, string apiKey, string appId, string baseUri)
        {
            string ingredientsQuery = string.Join(", ", ingredients);
            string requestUri = $"{baseUri}?type=public&q={ingredientsQuery}&app_id={appId}&app_key={apiKey}";

            HttpResponseMessage response = await _client.GetAsync(requestUri);
            await EnsureSuccessAsync(response);
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }

        private async Task EnsureSuccessAsync(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Response status code does not indicate success: {(int)response.StatusCode} ({response.StatusCode}). {errorContent}");
            }
        }
    }
}
