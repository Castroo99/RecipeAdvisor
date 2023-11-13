namespace RecipeAdvisorBackend.Model
{
    public class RecipeResponse
    {
        public int from { get; set; }
        public int to { get; set; }
        public int count { get; set; }
        public _Links _links { get; set; }
        public Hit[] hits { get; set; }
    }

    public class _Links
    {
        public Self self { get; set; }
        public Next next { get; set; }
    }

    public class Self
    {
        public string href { get; set; }
        public string title { get; set; }
    }

    public class Hit
    {
        public Recipe recipe { get; set; }
        public _Links1 _links { get; set; }
    }

    public class Recipe
    {
        public string uri { get; set; }
        public string label { get; set; }
        public string image { get; set; }
        public Images images { get; set; }
        public string source { get; set; }
        public string url { get; set; }
        public string shareAs { get; set; }
        public float yield { get; set; }
        public string[] dietLabels { get; set; }
        public string[] healthLabels { get; set; }
        public string[] cautions { get; set; }
        public string[] ingredientLines { get; set; }
        public Ingredients[] ingredients { get; set; }
        public float calories { get; set; }
        public float glycemicIndex { get; set; }
        public float totalCO2Emissions { get; set; }
        public string co2EmissionsClass { get; set; }
        public float totalWeight { get; set; }
        public string[] cuisineType { get; set; }
        public string[] mealType { get; set; }
        public string[] dishType { get; set; }
        public string[] instructions { get; set; }
        public string[] tags { get; set; }
        public string externalId { get; set; }
        public Totalnutrients totalNutrients { get; set; }
        public Totaldaily totalDaily { get; set; }
        public Digest[] digest { get; set; }
    }

    public class Images
    {
        public THUMBNAIL THUMBNAIL { get; set; }
        public SMALL SMALL { get; set; }
        public REGULAR REGULAR { get; set; }
        public LARGE LARGE { get; set; }
    }

    public class THUMBNAIL
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class SMALL
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class REGULAR
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class LARGE
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Totalnutrients
    {
    }

    public class Totaldaily
    {
    }

    public class Ingredients
    {
        public string text { get; set; }
        public float quantity { get; set; }
        public string measure { get; set; }
        public string food { get; set; }
        public float weight { get; set; }
        public string foodId { get; set; }
    }

    public class Digest
    {
        public string label { get; set; }
        public string tag { get; set; }
        public string schemaOrgTag { get; set; }
        public float total { get; set; }
        public bool hasRDI { get; set; }
        public float daily { get; set; }
        public string unit { get; set; }
    }

    public class _Links1
    {
        public Self1 self { get; set; }
        public Next1 next { get; set; }
    }

    public class Self1
    {
        public string href { get; set; }
        public string title { get; set; }
    }

    public class Next1
    {
        public string href { get; set; }
        public string title { get; set; }
    }

}
