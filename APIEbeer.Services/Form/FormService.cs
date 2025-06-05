using APIEbeer.Data.Models;
using APIEbeer.Shared.ViewModels;

namespace APIEbeer.Services.Form
{
    public class FormService(QuestionsModel questionsModel, ResponseOptionsModel responseOptionsModel) : IFormService
    {
        // Constructor injection for QuestionsModel and OptionsResponseModel
        private readonly QuestionsModel _questionsModel = questionsModel;
        private readonly ResponseOptionsModel _responseOptionsModel = responseOptionsModel;

        // Generates a dynamic form based on the characteristics of the provided model.
        public List<string> GenerateDynamicForm(ItemViewModel model)
        {
            // Validate the input model
            var characteristics = model.Characteristics;
            if (characteristics == null || characteristics.Count < 0)
            {
                throw new ArgumentException("The model must contain characteristics to generate a form.");
            }
            
        }

        private List<QuestionsViewModel> GenerateQuestions(Dictionary<string, string> characteristics)
        {
            // Create a list to hold the form questions
            var questions = new List<QuestionsViewModel>();

            // Use reflection to get the properties of the QuestionsModel
            var questionsProperties = _questionsModel.GetType().GetProperties();

            // Use reflection to get the properties of the OptionsResponseModel
            var optionsProperties = _responseOptionsModel.GetType().GetProperties()
                // Create a dictionary from the options properties for easy access
                .ToDictionary(p => p.Name, p => p);

            // Iterate through the characteristics and match them with the properties of QuestionsModel
            foreach (var characteristic in characteristics)
            {
                // Find the corresponding question property in QuestionsModel based on the characteristic key
                var questionProp = questionsProperties
                    .FirstOrDefault(p => string.Equals(p.Name, characteristic.Key, StringComparison.OrdinalIgnoreCase));
                if (questionProp == null)
                {
                    continue; // Skip if no matching question property found
                }

                // Get the value of the question property
                var questionText = questionProp.GetValue(_questionsModel)?.ToString();
                if (string.IsNullOrWhiteSpace(questionText))
                {
                    continue; // Skip if question text is null or empty
                }

                // Construct the option property name based on the question property name
                var optionName = $"Options{questionProp.Name}";
                // Check if the options property exists in OptionsResponseModel
                if (!optionsProperties.TryGetValue(optionName, out var optionProp))
                {
                    continue; // Skip if no matching options property found
                }

                // Get the value of the options property
                var options = optionProp.GetValue(_responseOptionsModel) as List<string>;
                // Ensure options are not null or empty
                if (options == null || options.Count == 0)
                {
                    continue; // Skip if options are null or empty
                }

                // Add the question and its options to the list
                questions.Add(new QuestionsViewModel
                {
                    QuestionText = questionText,
                    Options = options
                });
            }
            return questions;
        }
    }
}
