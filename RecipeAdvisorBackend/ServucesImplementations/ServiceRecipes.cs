using RecipeAdvisorBackend.Controllers;
using RecipeAdvisorBackend.ServicesInterfaces;

namespace RecipeAdvisorBackend.ServucesImplementations
{
    public class ServiceRecipes : IServiceRecipes
    {
        private readonly RecipeClient _client = new RecipeClient();
        private readonly IConfiguration _configuration;

        public ServiceRecipes(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task GetRecipe(List<string> ingredients)
        {
            string? apiKey = _configuration.GetValue<string>("ApiConfigs:API_KEY");
            string? appId = _configuration.GetValue<string>("ApiConfigs:APPLICATION_ID");
            string? baseUri = _configuration.GetValue<string>("ApiConfigs:BaseUrl");

            await _client.GetRecipesAsync<string>(ingredients, apiKey, appId, baseUri);
        }
    }
}
