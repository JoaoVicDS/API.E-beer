using Microsoft.AspNetCore.Mvc;

namespace APIEbeer.Controllers
{
    public class JsonController : ControllerBase
    {
        // Route to receive and validate menuJson
        [Route("api/menu")]
        [HttpPost]
        public IActionResult ValidateMenuJson([FromBody] string menuJson)
        {
            // Process the menu JSON here
            // After processing, return sucess response
            // Change to index view later
            return Ok(menuJson);
        }
    }
}
