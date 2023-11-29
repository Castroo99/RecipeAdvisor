using Newtonsoft.Json;

namespace RecipeAdvisorBackend.Controllers
{
    public class RecipeHttpClient
    {
        private readonly HttpClient _client;
        private const string JsonContentType = "application/json";

        public RecipeHttpClient(HttpClient? client = null)
        {
            if (client != null)
                _client = client;
            else
                _client = new HttpClient();
        }
        public async Task<T> GetRecipesAsync<T>(List<string> dietType, List<string> healthLabels, string mealType, List<string> ingredients, string apiKey, string appId, string baseUri)
        {
            string ingredientsQuery = string.Join(", ", ingredients);

            var queryParameters = new List<string>
            {
                $"type=public",
                $"q={ingredientsQuery}",
                $"app_id={appId}",
                $"app_key={apiKey}"
            };

            if (dietType != null && dietType.Any())
            {
                foreach (var diet in dietType)
                {
                    if (diet != "none")
                        queryParameters.Add($"diet={Uri.EscapeDataString(diet)}");
                }
            }

            if (healthLabels != null && healthLabels.Any())
            {
                foreach (var healthLabel in healthLabels)
                {
                    queryParameters.Add($"health={Uri.EscapeDataString(healthLabel)}");
                }
            }

            if (!string.IsNullOrEmpty(mealType))
            {
                queryParameters.Add($"mealType={Uri.EscapeDataString(mealType)}");
            }

            string requestUri = $"{baseUri}/recipes/v2?{string.Join("&", queryParameters)}";

            HttpResponseMessage response = await _client.GetAsync(requestUri);
            await EnsureSuccessAsync(response);
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }

        public async Task<T> GetIngredientsAsync<T>(string requestUri)
        {
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