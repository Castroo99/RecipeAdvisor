using RecipeAdvisorBackend.Model;

namespace RecipeAdvisorBackend.ServicesInterfaces
{
    public interface IServiceRecipes
    {
        /// <summary>
        /// Gets a recipe from the Edamam API and returns it to the webhook.
        /// </summary>
        /// <param name="dietType">List of diet types followed by the user</param>
        /// <param name="healthLabels">List of allergies or health labels of the user</param>
        /// <param name="mealType">Meal type that the user wants to make</param>
        /// <param name="ingredients">Ingredients of the recipe</param>
        /// <returns>The recipe</returns>
        Task<RecipeResponse> GetRecipe(List<string> dietType, List<string> healthLabels, string mealType, List<string> ingredients);

        /// <summary>
        /// Gets all the ingredients available from the FoodDB API and saves them to a json file.
        /// </summary>
        /// <returns></returns>
        Task GetIngredients();
    }
}