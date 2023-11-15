using Google.Cloud.Dialogflow.V2;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
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
            string requestJson;
            using (TextReader reader = new StreamReader(Request.Body))
            {
                requestJson = await reader.ReadToEndAsync();
            }
            var requestParser = new Google.Protobuf.JsonParser(Google.Protobuf.JsonParser.Settings.Default.WithIgnoreUnknownFields(true));
            WebhookRequest request = requestParser.Parse<WebhookRequest>(requestJson);

            var parameters = request.QueryResult.Parameters;
            parameters.Fields.TryGetValue("ingredients", out Value ingredients);
            List<string> ingredientsList = ingredients.ListValue.Values.Select(v => v.StringValue).ToList();

            await _serviceRecipes.GetRecipe(ingredientsList);

            var response = new WebhookResponse();
            StringBuilder sb = new StringBuilder();

            response.FulfillmentText = sb.ToString();

            return Ok(response);
        }
    }
}