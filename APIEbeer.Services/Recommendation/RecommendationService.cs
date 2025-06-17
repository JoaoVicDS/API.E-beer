using APIEbeer.Data.Models;
using APIEbeer.Shared.ViewModels;
using APIEbeer.Shared.ViewModels.Answers;
using APIEbeer.Shared.ViewModels.JSON;
using APIEbeer.Shared.ViewModels.Recommendation;

namespace APIEbeer.Services.Recommendation
{
    public class RecommendationService(ResponseOptionsModel responseOption) : IRecommendationService
    {
        private readonly ResponseOptionsModel _responseOption = responseOption;

        public RecommendationViewModel GenerateRecommendation(AnswersViewModel answers, List<CategoryViewModel> categories)
        {
            if (answers == null)
                throw new ArgumentNullException(nameof(answers), "Answers cannot be null.");

            if (categories == null)
                throw new ArgumentNullException(nameof(categories), "Categories cannot be null.");

            var recommendationCategories = new List<RecommendationCategoryViewModel>();

            foreach (var answersCategory in answers.Categories)
            {
                var category = categories.FirstOrDefault(c => c.Name == answersCategory.Name);
                
                if(category == null)
                    throw new ArgumentNullException(nameof(category), $"Category {answersCategory.Name} not found in {categories}.");

                RecommendationCategoryViewModel recommendationCategory = GenerateCategoryRecommendation(
                    answersCategory.Name,
                    answersCategory.SelectedAnswers, 
                    category.Items);

                if (recommendationCategory == null)
                    throw new ArgumentNullException(nameof(recommendationCategory), "Recommendation category cannot be null.");

                recommendationCategories.Add(recommendationCategory);
            }

            var recommendation = new RecommendationViewModel
            {
                Categories = recommendationCategories
            };

            return recommendation;
        }
        private RecommendationCategoryViewModel GenerateCategoryRecommendation(string categoryName, List<SelectedAnswersViewModel> selectedAnswers, List<ItemViewModel> items)
        {
            if (!string.IsNullOrEmpty(categoryName))
                throw new ArgumentException("Category name cannot be null or empty.", nameof(categoryName));

            if (selectedAnswers == null || selectedAnswers.Count == 0)
                throw new ArgumentException("Selected answers cannot be null or empty.", nameof(selectedAnswers));

            if (items == null || items.Count == 0)
                throw new ArgumentException("Items cannot be null or empty.", nameof(items));

            var itemsScored = new Dictionary<string, float>();

            foreach (var answer in selectedAnswers)
            {
                foreach (var item in items)
                {
                    if (!item.Characteristics.TryGetValue(answer.CharacteristicAsked, out var characteristicValue))
                        continue;

                    List<string> options = _responseOption.GetOptionsByCharacteristic(answer.CharacteristicAsked);

                    if (options == null || options.Count == 0)
                        throw new ArgumentException($"No options found for characteristic '{answer.CharacteristicAsked}'");

                    var selectedOptionIndex = options.IndexOf(answer.SelectedOption);

                    if (selectedOptionIndex == -1)
                        throw new ArgumentException($"Selected option '{answer.SelectedOption}' not found for characteristic '{answer.CharacteristicAsked}'");

                    if (characteristicValue == answer.SelectedOption)
                        if (itemsScored.TryGetValue(item.Name, out float value))
                            itemsScored[item.Name] = value + 1;
                        else
                            itemsScored.Add(item.Name, 1);
                    else if (selectedOptionIndex < options.Count - 1 && options[selectedOptionIndex + 1] == characteristicValue)
                        if (itemsScored.TryGetValue(item.Name, out float value))
                            itemsScored[item.Name] = value + 0.5f;
                        else
                            itemsScored.Add(item.Name, 0.5f);
                    else if (selectedOptionIndex > 0 && options[selectedOptionIndex - 1] == characteristicValue)
                        if (itemsScored.TryGetValue(item.Name, out float value))
                            itemsScored[item.Name] = value + 0.5f;
                        else
                            itemsScored.Add(item.Name, 0.5f);
                }
            }

            var bestItem = GetBestItem(itemsScored);

            return new RecommendationCategoryViewModel
            { 
                Name = categoryName,
                Recommendation = bestItem
            };
        }

        private string GetBestItem(Dictionary<string, float> itemsScored)
        {
            if (itemsScored.Count == 0)
                return string.Empty;

            KeyValuePair<string, float> bestItem = itemsScored.Aggregate((x, y) => x.Value > y.Value ? x : y);

            return bestItem.Key;
        }
    }
}
