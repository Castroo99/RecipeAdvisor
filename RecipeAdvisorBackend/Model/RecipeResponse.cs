using Newtonsoft.Json;

namespace RecipeAdvisorBackend.Model
{
    public class RecipeResponse
    {
        [JsonProperty("from")]
        public int From { get; set; }

        [JsonProperty("to")]
        public int To { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("_links")]
        public Links Links { get; set; }

        [JsonProperty("hits")]
        public Hit[] Hits { get; set; }
    }

    public class Hit
    {
        [JsonProperty("recipe")]
        public Recipe Recipe { get; set; }
    }

    public class Recipe
    {
        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("shareAs")]
        public string ShareAs { get; set; }

        [JsonProperty("yield")]
        public float Yield { get; set; }

        [JsonProperty("dietLabels")]
        public string[] DietLabels { get; set; }

        [JsonProperty("healthLabels")]
        public string[] HealthLabels { get; set; }

        [JsonProperty("cautions")]
        public string[] Cautions { get; set; }

        [JsonProperty("ingredientLines")]
        public string[] IngredientLines { get; set; }

        [JsonProperty("ingredients")]
        public Ingredients[] Ingredients { get; set; }

        [JsonProperty("calories")]
        public float Calories { get; set; }

        [JsonProperty("glycemicIndex")]
        public float GlycemicIndex { get; set; }

        [JsonProperty("cuisineType")]
        public string[] CuisineType { get; set; }

        [JsonProperty("mealType")]
        public string[] MealType { get; set; }

        [JsonProperty("dishType")]
        public string[] DishType { get; set; }

        [JsonProperty("instructions")]
        public string[] Instructions { get; set; }
    }

    public class Ingredients
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("quantity")]
        public float Quantity { get; set; }

        [JsonProperty("measure")]
        public string Measure { get; set; }

        [JsonProperty("food")]
        public string Food { get; set; }

        [JsonProperty("weight")]
        public float Weight { get; set; }

        [JsonProperty("foodId")]
        public string FoodId { get; set; }
    }
}