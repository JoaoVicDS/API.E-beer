using APIEbeer.Data.Models;
using APIEbeer.Shared.ViewModels.Answers;
using APIEbeer.Shared.ViewModels.JSON;
using APIEbeer.Shared.ViewModels.Recommendation;
using APIEbeer.Services.Form;

namespace APIEbeer.Services.Recommendation
{
    public class RecommendationService(ResponseOptionsModel responseOption, IFormService formService) : IRecommendationService
    {
        private readonly ResponseOptionsModel _responseOption = responseOption;
        private readonly IFormService _formService = formService;

        public RecommendationViewModel CreateRecommendation(AnswersViewModel answers, List<CategoryViewModel> categories)
        {
            // Validate input parameters
            if (!_formService.IsValidAnswers(answers))
                throw new ArgumentNullException(nameof(answers), "Answers is not valid.");

            if (categories == null)
                throw new ArgumentNullException(nameof(categories), "Categories cannot be null.");

            // Create a list to hold the recommendation categories
            var recommendationCategories = new List<RecommendationCategoryViewModel>();

            // Iterate through each category in the answers
            foreach (var answersCategory in answers.Categories)
            {
                // Validate the category name
                var category = categories.FirstOrDefault(c => c.Name.Equals(answersCategory.Name, StringComparison.OrdinalIgnoreCase));

                // If the category is not found, throw an exception
                if (category == null)
                    throw new ArgumentException(nameof(category), $"Category {answersCategory.Name} not found in {categories}.");

                // Create a recommendation for the category using the selected answers and items
                RecommendationCategoryViewModel recommendationCategory = CreateCategoryRecommendation(
                    answersCategory.Name,
                    answersCategory.SelectedAnswers, 
                    category.Items);

                // If the recommendation category is null, throw an exception
                if (recommendationCategory == null)
                    throw new ArgumentNullException(nameof(recommendationCategory), "Recommendation category cannot be null.");

                // Add the recommendation category to the list
                recommendationCategories.Add(recommendationCategory);
            }

            // Create the final recommendation view model
            var recommendation = new RecommendationViewModel
            {
                Categories = recommendationCategories
            };

            // Return the recommendation view model
            return recommendation;
        }

        //Generate a recommendation for a specific category based on selected answers and items
        private RecommendationCategoryViewModel CreateCategoryRecommendation(string categoryName, List<SelectedAnswersViewModel> selectedAnswers, List<ItemViewModel> items)
        {
            // Validate input parameters
            if (string.IsNullOrEmpty(categoryName))
                throw new ArgumentException("Category name cannot be null or empty.", nameof(categoryName));

            if (selectedAnswers == null || selectedAnswers.Count == 0)
                throw new ArgumentException("Selected answers cannot be null or empty.", nameof(selectedAnswers));

            if (items == null || items.Count == 0)
                throw new ArgumentException("Items cannot be null or empty.", nameof(items));

            // Create a dictionary to hold the scores for each item
            var itemsScored = new Dictionary<string, float>();

            // Iterate through each selected answer
            foreach (var answer in selectedAnswers)
            {
                // Iterate through each item in the category
                foreach (var item in items)
                {
                    var characteristicKeyLower = char.ToLower(answer.CharacteristicAsked[0]) + answer.CharacteristicAsked[1..];

                    // Validate the item characteristics and create a characteristic value if it exists
                    if (!item.Characteristics.TryGetValue(characteristicKeyLower, out var characteristicValue))
                        continue;

                    // If the characteristic value is null, throw an exception
                    if (characteristicValue == null)
                        throw new ArgumentException(nameof(characteristicValue), $"Characteristic value for '{answer.CharacteristicAsked}' not found.");

                    // Get the options for the characteristic from the response options model
                    List<string> options = _responseOption.GetOptionsByCharacteristic(answer.CharacteristicAsked);

                    // If no options are found, throw an exception
                    if (options == null || options.Count == 0)
                        throw new ArgumentException($"No options found for characteristic '{answer.CharacteristicAsked}'");

                    // Check if the selected option exists in the options list and get its index
                    var selectedOptionIndex = options.IndexOf(answer.SelectedOption);

                    // If the selected option is not found, throw an exception
                    if (selectedOptionIndex == -1)
                        throw new ArgumentException($"Selected option '{answer.SelectedOption}' not found for characteristic '{answer.CharacteristicAsked}'");

                    // Check if the characteristic value matches the selected option or its neighbors
                    if (string.Equals(characteristicValue, answer.SelectedOption, StringComparison.OrdinalIgnoreCase))
                    {
                        // If it matches, score the item
                        itemsScored[item.Name] = itemsScored.TryGetValue(item.Name, out float value) ? value + 1.0f : 1.0f;
                    }
                    // If it matches a neighboring option, score the item with a lower value
                    else if ((selectedOptionIndex < options.Count - 1 && string.Equals(options[selectedOptionIndex + 1], characteristicValue, StringComparison.OrdinalIgnoreCase))
                        || (selectedOptionIndex > 0 && string.Equals(options[selectedOptionIndex - 1], characteristicValue, StringComparison.OrdinalIgnoreCase)))
                    {
                        itemsScored[item.Name] = itemsScored.TryGetValue(item.Name, out float value) ? value + 0.5f : 0.5f;
                    }
                }
            }

            // Get the best item based on the scores
            var bestItem = GetBestItem(itemsScored);

            // Check if the best item is null or empty
            if (string.IsNullOrEmpty(bestItem))
                bestItem = "Não recomendou nada";

            // Create and return the recommendation category view model with the best item
            return new RecommendationCategoryViewModel
            { 
                Name = categoryName,
                Recommendation = bestItem
            };
        }

        // Get the best item from the scored items based on their scores
        private string GetBestItem(Dictionary<string, float> itemsScored)
        {
            // Validate input parameter
            if (itemsScored.Count == 0)
                return string.Empty;

            // Find the item with the highest score
            var bestItem = itemsScored.FirstOrDefault();
            foreach (var item in itemsScored)
            {
                bestItem = item.Value > bestItem.Value ? item : bestItem;
            }

            return bestItem.Key;
        }
    }
}
