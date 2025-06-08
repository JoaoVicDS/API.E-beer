using APIEbeer.Services.Form;   
using APIEbeer.Services.Json;
using APIEbeer.Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace APIEbeer.Controllers
{
    [ApiController]
    public class FormController(IJsonService jsonService, IFormService formService): Controller
    {
        private readonly IJsonService _jsonService = jsonService;
        private readonly IFormService _formService = formService;

        // Route to receive and validate menuJson
        // POST: api/menu
        [Route("api/menu")]
        [HttpPost]
        public IActionResult Index([FromBody] JsonViewModel model)
        {
            if (model == null)
                return BadRequest("JSON não pode ser nulo ou vazio");

            // Check if the JsonViewModel is valid
            var (IsValid, ErrorMessage) = ValidateJsonStructure(model);

            if (!IsValid)
                return BadRequest(ErrorMessage ?? "Erro de validação desconhecido.");
            
            if(model?.Menu == null)
                return BadRequest("O campo 'menu' não pode ser nulo ou vazio.");

            // Create the dynamic form based on the JSON structure
            FormViewModel form = _formService.GenerateDynamicForm(model.Menu);

            // Return to the view with the form
            return Ok(form);
        }

        private (bool IsValid, string? ErrorMessage) ValidateJsonStructure(JsonViewModel? model)
        {
            if (model == null)
                return (false, "O model não pode ser nulo.");

            var (IsValid, ErrorMessage) = _jsonService.ValidateJsonStructure(model);

            if (!IsValid)
                return (false, ErrorMessage);

            return (true, null);
        }
    }

}
