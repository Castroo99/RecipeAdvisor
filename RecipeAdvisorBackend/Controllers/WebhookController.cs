using Google.Cloud.Dialogflow.V2;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using RecipeAdvisorBackend.Model;
using RecipeAdvisorBackend.ServicesInterfaces;
using System.Text;

namespace RecipeAdvisorBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebHookController : ControllerBase
    {
        private readonly ILogger<WebHookController> _logger;
        private static readonly JsonParser jsonParser = new JsonParser(JsonParser.Settings.Default.WithIgnoreUnknownFields(true));
        private readonly IServiceRecipes _serviceRecipes;

        public WebHookController(ILogger<WebHookController> logger, IServiceRecipes serviceRecipes)
        {
            _serviceRecipes = serviceRecipes;
            _logger = logger;
        }

        [HttpGet("Status")]
        public IActionResult GetStatus()
        {
            return Ok("Ok");
        }

        [HttpGet("Ingredients")]
        public async Task<IActionResult> GetIngredients()
        {
            try
            {
                await _serviceRecipes.GetIngredients();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting ingredients");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> GetWebhookResponse()
        {
            _logger.LogInformation("Webhook request received");
            try
            {
                string requestJson;
                using (TextReader reader = new StreamReader(Request.Body))
                {
                    requestJson = await reader.ReadToEndAsync();
                }
                WebhookRequest request = jsonParser.Parse<WebhookRequest>(requestJson);
                var context = request.QueryResult.OutputContexts.FirstOrDefault(o => o.ContextName.ContextId == "getdiet-followup");
                var parameters = context.Parameters;
                parameters.Fields.TryGetValue("diet_type", out Value dietTypeValue);
                parameters.Fields.TryGetValue("health_labels", out Value healthLabelsValue);
                parameters.Fields.TryGetValue("meal_type", out Value mealTypeValue);
                parameters.Fields.TryGetValue("ingredients", out Value ingredientsValue);
                List<string> dietTypeList = dietTypeValue.ListValue.Values.Select(v => v.StringValue).ToList();
                List<string> healthLabelsList = healthLabelsValue.ListValue.Values.Select(v => v.StringValue).ToList();
                string mealType = mealTypeValue.ListValue.Values.Select(v => v.StringValue).FirstOrDefault();
                List<string> ingredientsList = ingredientsValue.ListValue.Values.Select(v => v.StringValue).ToList();

                RecipeResponse recipe = await _serviceRecipes.GetRecipe(dietTypeList, healthLabelsList, mealType, ingredientsList);

                var response = new WebhookResponse();

                response.FulfillmentText = $"Here is the recipe {recipe.Hits.FirstOrDefault()?.Recipe?.Label}: \n Ingredients: {string.Join("\n", ingredientsList)}.\n Instructions: {recipe.Hits[0].Recipe.ShareAs}";

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting webhook response");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}