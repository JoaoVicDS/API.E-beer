using APIEbeer.Services.Form;
using APIEbeer.Services.Json;
using APIEbeer.Services.Cache;
using Microsoft.AspNetCore.Mvc;
using APIEbeer.Shared.ViewModels.Form;
using APIEbeer.Shared.ViewModels.JSON;
using APIEbeer.Shared.ViewModels.Answers;

namespace APIEbeer.Controllers
{
    [ApiController]
    public class FormController(IJsonService jsonService, IFormService formService, ICacheService cacheService): Controller
    {
        private readonly IJsonService _jsonService = jsonService;
        private readonly IFormService _formService = formService;
        private readonly ICacheService _cacheService = cacheService;

        // Route to return to Index View with form
        // GET: api/form?formId={formId}
        [HttpGet("api/form")]
        public IActionResult Index([FromQuery] string formId)
        {
            // Getting the FormViewModel from cache
            var form = _cacheService.Get<FormViewModel>($"formId:{formId}:FormViewModel");

            // Checks if form is null
            if (form == null)
            {
                return BadRequest($"Não foi possível localizar o formulário {formId}");
            }

            // Returns the JSON of the form
            return Ok(form);
        }

        // Route to generate the form
        // POST: api/menu
        [Route("api/menu")]
        [HttpPost]
        public IActionResult GenerateForm([FromBody] JsonViewModel model)
        {
            // Checks if the JsonViewModel is valid
            if (model == null)
                return BadRequest("JSON não pode ser nulo ou vazio");

            // Checks if the structure JSON is valid
            var (IsValid, ErrorMessage) = ValidateJsonStructure(model);

            // Checks the result of the ValidateJsonStructure function
            if (!IsValid)
                return BadRequest(ErrorMessage ?? "Erro de validação desconhecido.");

            // Generate a unique form ID
            var formId = Guid.NewGuid().ToString();

            // Checks if the formId is null or empty , if yes then creates a custom formId
            if (string.IsNullOrEmpty(formId))
                formId = $"{model.Restaurant}{model.Menu.Categories.Count}"; 

            // Saves JsonViewModel in the cache
            _cacheService.Set<JsonViewModel>($"formId:{formId}:JsonViewModel", model);

            // Checks if the Menu is null or empty
            if (model?.Menu == null)
                return BadRequest("O campo 'menu' não pode ser nulo ou vazio.");

            // Create the dynamic form based on the JSON structure
            FormViewModel form = _formService.GenerateDynamicForm(model.Menu);

            // Checks if the form is null or empty
            if (form == null)
                return BadRequest("Não foi possível gerar o formulário a partir do JSON fornecido.");

            // Saves FormViewModel in the cache
            _cacheService.Set<FormViewModel>($"formId:{formId}:FormViewModel", form);

            // Redirect to Index
            return RedirectToAction("Index", new { formId });

        }

        [HttpPost("api/form/submit")]
        public IActionResult ValidateAnswers([FromBody] AnswersViewModel answers)
        {
            if (_formService.ValidateAnswers(answers))
                return BadRequest("As respostas recebidas pelo formulário estão incorretas");

            return RedirectToAction("RecommendItem", "RecommendationController", new { answers });
        }

        private (bool IsValid, string? ErrorMessage) ValidateJsonStructure(JsonViewModel? model)
        {
            var (IsValid, ErrorMessage) = _jsonService.ValidateJsonStructure(model);

            if (!IsValid)
                return (false, ErrorMessage);

            return (true, null);
        }
    }

}
