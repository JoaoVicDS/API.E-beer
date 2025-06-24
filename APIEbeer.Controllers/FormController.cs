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
    [Route("api/form")]
    public class FormController(IJsonService jsonService, IFormService formService, ICacheService cacheService): Controller
    {
        private readonly IJsonService _jsonService = jsonService;
        private readonly IFormService _formService = formService;
        private readonly ICacheService _cacheService = cacheService;

        
        [HttpGet]
        public IActionResult GetForm([FromQuery] string formId)
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

        [Route("create")]
        [HttpPost]
        public IActionResult CreateForm([FromBody] JsonViewModel model)
        {
            // Checks if the JsonViewModel is valid
            if (model == null)
                return BadRequest("JSON não pode ser nulo ou vazio");

            // Checks if the structure JSON is valid
            var (IsValid, ErrorMessage) = _jsonService.IsValidJsonStructure(model);

            // Checks the result of the ValidateJsonStructure function
            if (!IsValid)
                return BadRequest(ErrorMessage ?? "Erro de validação desconhecido.");

            // Create the dynamic form based on the JSON structure
            FormViewModel form = _formService.CreateDynamicForm(model.Menu);

            // Checks if the form is null or empty
            if (form == null)
                return BadRequest("Não foi possível gerar o formulário a partir do JSON fornecido.");

            // Saves FormViewModel in the cache
            _cacheService.Set<FormViewModel>($"formId:{form.FormId}:FormViewModel", form);

            // Saves JsonViewModel in the cache
            _cacheService.Set<JsonViewModel>($"formId:{form.FormId}:JsonViewModel", model);

            // Redirect to Index
            return RedirectToAction("GetForm", new { form.FormId });

        }
    }

}
