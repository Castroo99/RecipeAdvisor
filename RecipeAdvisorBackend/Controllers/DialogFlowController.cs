using Microsoft.AspNetCore.Mvc;

namespace RecipeAdvisorBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DialogFlowController : ControllerBase
    {

        private readonly ILogger<DialogFlowController> _logger;

        public DialogFlowController(ILogger<DialogFlowController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Status")]
        public IActionResult GetStatus()
        {
            return Ok();
        }
    }
}