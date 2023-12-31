﻿using Newtonsoft.Json;
using RecipeAdvisorBackend.Controllers;
using RecipeAdvisorBackend.Model;
using RecipeAdvisorBackend.ServicesInterfaces;

namespace RecipeAdvisorBackend.ServucesImplementations
{
    public class EdamamServiceRecipes : IServiceRecipes
    {
        private readonly RecipeHttpClient _client = new RecipeHttpClient();
        private readonly IConfiguration _configuration;
        private readonly string? recipeApiKey;
        private readonly string? recipeAppId;
        private readonly string? foodApiKey;
        private readonly string? foodAppId;
        private readonly string? baseUri;

        public EdamamServiceRecipes(IConfiguration configuration)
        {
            _configuration = configuration;

            recipeApiKey = _configuration.GetValue<string>("ApiConfigs:RECIPE_API_KEY");
            recipeAppId = _configuration.GetValue<string>("ApiConfigs:RECIPE_APPLICATION_ID");
            foodApiKey = _configuration.GetValue<string>("ApiConfigs:FOODDB_API_KEY");
            foodAppId = _configuration.GetValue<string>("ApiConfigs:FOODDB_APPLICATION_ID");
            baseUri = _configuration.GetValue<string>("ApiConfigs:BaseUrl");
        }

        public async Task GetIngredients()
        {
            string requestUri = $"{baseUri}/food-database/v2/parser?app_id={foodAppId}&app_key={foodApiKey}";
            List<Ingredient> totalIngredients = new List<Ingredient>();
            while (!string.IsNullOrEmpty(requestUri))
            {
                var ingredientsResponse = await _client.GetIngredientsAsync<IngredientResponse>(requestUri);

                var ingredients = ingredientsResponse?.Hints?.Select(hint =>
                {
                    string label = hint?.Food?.Label;
                    if (!string.IsNullOrEmpty(label))
                    {
                        int commaIndex = label.IndexOf(",");
                        if (commaIndex > 0)
                        {
                            label = label.Substring(0, commaIndex).Trim();
                        }
                    }
                    return label;
                }).Where(label => !string.IsNullOrEmpty(label));

                totalIngredients.AddRange(ingredients.Select(label => new Ingredient { Value = label, Synonyms = new List<string> { label } }));

                requestUri = ingredientsResponse?.Links?.Next?.Href;

                // To not exceed API request limit
                await Task.Delay(TimeSpan.FromSeconds(5));
            }
            totalIngredients = totalIngredients.DistinctBy(i => i.Value).ToList();

            string json = JsonConvert.SerializeObject(totalIngredients, Formatting.Indented);
            string filePath = "output.json";
            File.WriteAllText(filePath, json);
        }

        public async Task<RecipeResponse> GetRecipe(List<string> dietType, List<string> healthLabels, string mealType, List<string> ingredients)
        {
            return await _client.GetRecipesAsync<RecipeResponse>(dietType, healthLabels, mealType, ingredients, recipeApiKey, recipeAppId, baseUri);
        }
    }
}