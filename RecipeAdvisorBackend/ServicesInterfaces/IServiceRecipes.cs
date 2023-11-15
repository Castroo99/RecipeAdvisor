using RecipeAdvisorBackend.Model;

namespace RecipeAdvisorBackend.ServicesInterfaces
{
    public interface IServiceRecipes
    {
        /// <summary>
        /// Gets a recipe from the Edamam API and returns it to the webhook.
        /// </summary>
        /// <param name="ingredients">Ingredients of the recip</param>
        /// <returns>The recipe</returns>
        Task<RecipeResponse> GetRecipe(List<string> ingredients);

        /// <summary>
        /// Gets all the ingredients available from the FoodDB API and saves them to a json file.
        /// </summary>
        /// <returns></returns>
        Task GetIngredients();
    }
}