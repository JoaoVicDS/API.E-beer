using APIEbeer.Shared.ViewModels.Answers;
using APIEbeer.Shared.ViewModels.Recommendation;

namespace APIEbeer.Services.Recommendation
{
    public interface IRecommendationService
    {
        public RecommendationViewModel RecomendItem(AnswersViewModel answers);

        public List<RecommendationCategoryViewModel> ScoreByCategory(AnswersViewModel answers);

        public RecommendationCategoryViewModel Score();
    }
}
