using Microsoft.AspNetCore.Mvc;
using APIEbeer.Shared.ViewModels;
using APIEbeer.Services.Json;


namespace APIEbeer.Controllers
{
    [ApiController]
    public class JsonController (IJsonService jsonService): Controller
    {
        private readonly IJsonService _jsonService = jsonService;
        // Route to receive and validate menuJson
        // POST: api/menu
        [Route("api/menu")]
        [HttpPost]
        public IActionResult ValidateMenuJson([FromBody] JsonViewModel model)
        {
            // Process the menu JSON here
            if (model == null)
            {
                return BadRequest("O objeto JSON não pode ser nulo.");
            }

            var (IsValid, ErrorMessage) = _jsonService.ValidateJsonStructure(model);

            if (!IsValid) {
                return BadRequest(ErrorMessage ?? "Erro de validação desconhecido.");
            }
            // After processing, return sucess response
            // Change to index view later
            return Ok(model);
        }
    }


}
