namespace RecipeAdvisorBackend.ServicesInterfaces
{
    public interface IServiceRecipes
    {
        Task GetRecipe(List<string> ingredients);
    }
}
