using System.Text.Json.Serialization;   

namespace APIEbeer.Shared.ViewModels.Answers
{
    public class AnswersViewModel
    {
        [JsonPropertyName("formId")]
        public required string FormId { get; set; }

        [JsonPropertyName("categories")]
        public required List<AnswersCategoryViewModel> Categories { get; set; } = [];
    }
}
