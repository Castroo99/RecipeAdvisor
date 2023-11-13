using Google.Cloud.Dialogflow.V2;
using Google.Protobuf;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        public async Task<IActionResult> GetWebhookResponse()
        {
            _logger.LogInformation("Webhook request received");
            WebhookRequest request;
            using (var reader = new StreamReader(Request.Body))
            {
                request = jsonParser.Parse<WebhookRequest>(reader);
            }

            var pas = request.QueryResult.Parameters;
            var askingName = pas.Fields.ContainsKey("name") && pas.Fields["name"].ToString().Replace('\"', ' ').Trim().Length > 0;
            var askingAddress = pas.Fields.ContainsKey("address") && pas.Fields["address"].ToString().Replace('\"', ' ').Trim().Length > 0;
            var askingBusinessHour = pas.Fields.ContainsKey("business-hours") && pas.Fields["business-hours"].ToString().Replace('\"', ' ').Trim().Length > 0;
            var response = new WebhookResponse();

            string name = "Jeffson Library", address = "1234 Brentwood Lane, Dallas, TX 12345", businessHour = "8:00 am to 8:00 pm";

            StringBuilder sb = new StringBuilder();

            if (askingName)
            {
                sb.Append("The name of library is: " + name + "; ");
            }

            if (askingAddress)
            {
                sb.Append("The Address of library is: " + address + "; ");
            }

            if (askingBusinessHour)
            {
                sb.Append("The Business Hour of library is: " + businessHour + "; ");
            }

            if (sb.Length == 0)
            {
                sb.Append("Greetings from our Webhook API!");
            }

            response.FulfillmentText = sb.ToString();

            return Ok(response);
        }
    }
}