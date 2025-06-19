using APIEbeer.Services.Cache;
using APIEbeer.Services.Recommendation;
using APIEbeer.Shared.ViewModels.Answers;
using APIEbeer.Shared.ViewModels.JSON;
using APIEbeer.Shared.ViewModels.Recommendation;
using Microsoft.AspNetCore.Mvc;

namespace APIEbeer.Controllers
{
    [ApiController]
    public class RecommendationController(IRecommendationService recommendationService, ICacheService cacheService) : Controller
    {
        private readonly IRecommendationService _recommendationService = recommendationService;
        private readonly ICacheService _cacheService = cacheService;

        [HttpGet("api/recommendation/index")]
        public IActionResult Index([FromQuery] string formId)
        {
            var recommendation = _cacheService.Get<RecommendationViewModel>($"formId:{formId}:RecommendationViewModel");

            if (recommendation == null)
            {
                return BadRequest($"Não foi possível localizar a recomendação {formId}");
            }

            return Ok(recommendation);
        }

        [HttpPost("api/recommendation/generate")]
        public IActionResult GenerateRecommendation([FromBody] AnswersViewModel answers)
        {
            if (answers == null)
                return BadRequest("Não foi possível localizar as respostas enviadas pelo body.");

            var json = _cacheService.Get<JsonViewModel>($"formId:{answers.FormId}:JsonViewModel");
            
            if (json == null)
                return BadRequest($"Não foi possível localizar o JsonViewModel armazenado no cache com o formId: {answers.FormId}.");

            RecommendationViewModel recommendation = _recommendationService.GenerateRecommendation(answers, json.Menu.Categories);

            if (recommendation == null)
                return BadRequest($"Não foi possível gerar a recomendação.");

            _cacheService.Set<RecommendationViewModel>($"formId:{answers.FormId}:RecommendationViewModel", recommendation);

            return Ok();
        }   
    }
}