using APIEbeer.Services.Form;   
using APIEbeer.Services.Json;
using APIEbeer.Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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
        public IActionResult Index([FromBody] string json)
        {
            // Parse the JSON string into a JsonViewModel object
            JsonViewModel? model = ParseJson(json);

            if (model == null)
                return BadRequest("JSON não pode ser nulo ou vazio");

            // Check if the JsonViewModel is valid
            var (IsValid, ErrorMessage) = ValidateJsonStructure(model);

            if (!IsValid)
                return BadRequest(ErrorMessage ?? "Erro de validação desconhecido.");
            
            if(model?.Menu == null)
                return BadRequest("O campo 'menu' não pode ser nulo ou vazio.");

            // Pick up the Items list
            var items = model.Menu?
                .SelectMany(m => m.Items)
                .ToList();

            if (items == null)
                return BadRequest("O campo 'items' não pode ser nulo ou vazio.");

            // Create the dynamic form based on the JSON structure
            FormViewModel form = _formService.GenerateDynamicForm(items);

            // Return to the view with the form
            return View(form);
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

        private JsonViewModel? ParseJson(string json)
        {
            // Verify if the input JSON string is null or empty
            if (string.IsNullOrWhiteSpace(json))
                return null;

            return _jsonService.ParseJson(json);
        }
    }

}
