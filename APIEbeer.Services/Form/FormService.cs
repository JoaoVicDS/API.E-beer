using APIEbeer.Data.Models;
using APIEbeer.Shared.ViewModels.Answers;
using APIEbeer.Shared.ViewModels.Form;
using APIEbeer.Shared.ViewModels.JSON;

namespace APIEbeer.Services.Form
{
    public class FormService(QuestionsModel questionsModel, ResponseOptionsModel responseOptionsModel) : IFormService
    {
        // Constructor injection for QuestionsModel and OptionsResponseModel
        private readonly QuestionsModel _questionsModel = questionsModel;
        private readonly ResponseOptionsModel _responseOptionsModel = responseOptionsModel;
        private readonly List<string> _questionsCreated = [];

        // Generates a dynamic form based on the characteristics of the provided model.
        public FormViewModel CreateDynamicForm(MenuViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model), "Model cannot be null.");

            // Generate categories from the model's categories
            var categories = CreateCategories(model.Categories);

            // Check if categories were generated successfully
            if (categories == null)
                throw new ArgumentException("It was not possible to generate the categories");

            var formId = Guid.NewGuid().ToString();

            // Create the form view model with the generated categories
            var form = new FormViewModel
            {
                FormId = formId,
                Categories = categories
            };

            return form;
        }

        private List<FormCategoryViewModel> CreateCategories(List<CategoryViewModel> categories)
        {
            if (categories == null || categories.Count == 0)
                throw new ArgumentException("Categories cannot be null or empty.");

            // Create a list to hold the categories with their questions
            var questionsByCategory = new List<FormCategoryViewModel>();

            // Iterate through each category in the provided categories
            foreach (var category in categories)
            {
                //Create a list to hold all questions for the current category
                var allQuestions = new List<FormQuestionsViewModel>();

                // Iterate through each item in the category
                foreach (var item in category.Items)
                {
                    if (item.Characteristics == null || item.Characteristics.Count == 0)
                        continue; // Skip if no characteristics for this item

                    // Generate questions based on the characteristics of the item
                    var questions = CreateQuestions(item.Characteristics);

                    if (questions == null)
                        continue; // Skip if no questions generated for this item

                    // Add the generated questions to the list for this category
                    allQuestions.AddRange(questions);
                }

                // If there are questions for this category, add it to the list of categories
                if (allQuestions.Count > 0)
                {
                    questionsByCategory.Add(new FormCategoryViewModel
                    {
                        Name = category.Name,
                        Questions = allQuestions
                    });
                }
            }

            return questionsByCategory;
        }

        // Generates a list of questions based on the characteristics provided in the model.
        private List<FormQuestionsViewModel> CreateQuestions(Dictionary<string, string> characteristics)
        {
            if (characteristics == null || characteristics.Count == 0)
                throw new ArgumentException("Characteristics cannot be null or empty.");

            // Create a list to hold the form questions
            var questions = new List<FormQuestionsViewModel>();

            // Use reflection to get the properties of the QuestionsModel
            var questionsProperties = _questionsModel
                .GetType()
                .GetProperties()
                .ToDictionary(p => p.Name, p => p, StringComparer.OrdinalIgnoreCase);

            // Use reflection to get the properties of the OptionsResponseModel
            var optionsProperties = _responseOptionsModel
                .GetType()
                .GetProperties()
                // Create a dictionary from the options properties for easy access
                .ToDictionary(p => p.Name, p => p, StringComparer.OrdinalIgnoreCase);

            // Iterate through the characteristics and match them with the properties of QuestionsModel
            foreach (var characteristic in characteristics)
            {
                // Find the corresponding question property in QuestionsModel based on the characteristic key
                if (!questionsProperties.TryGetValue(characteristic.Key, out var questionProp))
                    continue; // Skip if no matching question property found

                if (_questionsCreated.Contains(questionProp.Name))
                    continue;

                // Get the value of the question property
                var questionText = questionProp.GetValue(_questionsModel)?.ToString();
                if (string.IsNullOrWhiteSpace(questionText))
                    continue; // Skip if question text is null or empty

                // Construct the option property name based on the question property name
                var optionName = $"Options{questionProp.Name}";
                // Check if the options property exists in OptionsResponseModel and save it in optionProp
                if (!optionsProperties.TryGetValue(optionName, out var optionProp))
                    continue; // Skip if no matching options property found

                // Get the value of the options property and try convert it to a List<string>
                var options = optionProp.GetValue(_responseOptionsModel) as List<string>;
                // Ensure options are not null or empty
                if (options == null || options.Count == 0)
                    continue; // Skip if options are null or empty

                // Add the question and its options to the list
                questions.Add(new FormQuestionsViewModel
                {
                    Characteristic = questionProp.Name,
                    Question = questionText,
                    Options = options
                });

                _questionsCreated.Add(questionProp.Name);

            }
            return questions;
        }

        public bool IsValidAnswers(AnswersViewModel answers)
        {
            if (answers == null)
                throw new ArgumentNullException(nameof(answers), "Answers cannot be null.");

            foreach (var category in answers.Categories)
            {
                if(category.SelectedAnswers.Count == 0)
                    throw new ArgumentException($"Category '{category.Name}' has no selected answers.");

                foreach (var answer in category.SelectedAnswers)
                {
                    var characteristicAsked = answer.CharacteristicAsked;

                    if (!_questionsModel.ValidateQuestionByCharacteristic(characteristicAsked ?? string.Empty))
                        throw new ArgumentException($"Invalid characteristic asked: {characteristicAsked} in category '{category.Name}'.");

                    var options = _responseOptionsModel.GetOptionsByCharacteristic(characteristicAsked ?? string.Empty);

                    if (options.Count == 0)
                        throw new ArgumentException($"The characteristic asked: {characteristicAsked} has no response options");

                    if (!options.Contains(answer.SelectedOption))
                        throw new ArgumentException($"One or more selected options are not valid for characteristic '{characteristicAsked}' in category '{category.Name}'.");
                }

            }

            return true;
        }
    }
}
