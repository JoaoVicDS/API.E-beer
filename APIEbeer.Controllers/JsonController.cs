using Microsoft.AspNetCore.Mvc;
using APIEbeer.Shared.ViewModels;


namespace APIEbeer.Controllers
{
    [ApiController]
    public class JsonController : ControllerBase
    {
        // Route to receive and validate menuJson
        // POST: api/menu
        [Route("api/menu")]
        [HttpPost]
        public IActionResult ValidateMenuJson([FromBody] JsonViewModel model)
        {
            // Process the menu JSON here
            // After processing, return sucess response
            // Change to index view later
            return Ok(model);
        }
    }
}
