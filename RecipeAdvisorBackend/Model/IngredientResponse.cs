using Newtonsoft.Json;

namespace RecipeAdvisorBackend.Model
{
    public class IngredientResponse
    {
        [JsonProperty("hints")]
        public List<Hint> Hints { get; set; }
        [JsonProperty("_links")]
        public Links Links { get; set; }
    }

    public class Hint
    {
        [JsonProperty("food")]
        public Food Food { get; set; }
    }

    public class Food
    {
        [JsonProperty("foodId")]
        public string FoodId { get; set; }
        [JsonProperty("label")]
        public string Label { get; set; }
    }

    public class Links
    {
        [JsonProperty("next")]
        public Next Next { get; set; }
    }

    public class Next
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("href")]
        public string Href { get; set; }
    }
}
