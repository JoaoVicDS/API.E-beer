using APIEbeer.Data.Models;
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

        // Route to visualize the form
        [HttpGet("/formulario")]
        public IActionResult Visualizar()
        {
            var questionsModel = new QuestionsModel();

            var questionsList = new List<string>
            {
                questionsModel.Bitter,
                questionsModel.Sweeet,
                questionsModel.AlcoholContent,
                questionsModel.Flavor,
                questionsModel.Texture,
                questionsModel.Color,
                questionsModel.Carbonation,
                questionsModel.PointOfMeat,
                questionsModel.Temperature,
                questionsModel.Seasoning,
                questionsModel.Acidity,
                questionsModel.SauceAndNoSauce,
                questionsModel.Spicy,
                questionsModel.Size,
                questionsModel.Volume
            };

            // Carrega ou simula o JSON
            var mock = new JsonViewModel
            {
                Restaurant = "E-beer",
                Menu = new MenuViewModel
                {
                    Categories = new List<CategoryViewModel>
            {
                new CategoryViewModel
                {
                    Name = "Bebidas",
                    Items = new List<ItemViewModel>
                    {
                        new ItemViewModel
                        {
                            Name = "Cerveja",
                            Characteristics = new Dictionary<string, string>
                            {
                                { "Amargor", "Moderado" },
                                { "Teor", "5%" }
                            }
                        }
                    }
                }
            }
                }
            };

            var (valid, error) = _jsonService.ValidateJsonStructure(mock);
            if (!valid) return BadRequest(error);

            ViewData["RestaurantName"] = mock.Restaurant;
            ViewBag.QuestionsList = questionsList;

            System.Diagnostics.Debug.WriteLine("Questions carregadas:");
            foreach (var q in questionsList)
            {
                System.Diagnostics.Debug.WriteLine(q);
            }

            var form = _formService.GenerateDynamicForm(mock.Menu);
            return View("Formulario", form); 
        }
    }

}
