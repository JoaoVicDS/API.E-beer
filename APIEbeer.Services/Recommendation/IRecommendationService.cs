using APIEbeer.Shared.ViewModels.Answers;
using APIEbeer.Shared.ViewModels.Recommendation;
using APIEbeer.Shared.ViewModels.JSON;

namespace APIEbeer.Services.Recommendation
{
    public interface IRecommendationService
    {
        RecommendationViewModel CreateRecommendation(AnswersViewModel answers, List<CategoryViewModel> categories);
    }
}
