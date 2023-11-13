namespace RecipeAdvisorBackend.ServicesInterfaces
{
    public interface IServiceRecipes
    {
        Task GetRecipe(List<string> ingredients);

        /// <summary>
        /// Gets all the ingredients available from the FoodDB API and saves them to a file.
        /// </summary>
        /// <returns></returns>
        Task GetIngredients();
    }
}
