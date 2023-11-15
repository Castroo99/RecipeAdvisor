using Newtonsoft.Json;

namespace RecipeAdvisorBackend.Model
{
    public class Ingredient
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("synonyms")]
        public List<string> Synonyms { get; set; }
    }
}