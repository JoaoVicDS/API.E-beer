using System.Text.Json.Serialization;

namespace APIEbeer.Shared.ViewModels.Answers
{
    public class AnswersCategoryViewModel
    {
        [JsonPropertyName("name")]
        public required string Name { get; set; }

        [JsonPropertyName("selectedAnswers")]
        public required List<SelectedAnswersViewModel> SelectedAnswers { get; set; } = [];
    }
}
