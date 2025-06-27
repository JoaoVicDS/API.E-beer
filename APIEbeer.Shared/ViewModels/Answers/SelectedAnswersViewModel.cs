using System.Text.Json.Serialization;

namespace APIEbeer.Shared.ViewModels.Answers
{
    public class SelectedAnswersViewModel
    {
        [JsonPropertyName("characteristicAsked")]
        public required string CharacteristicAsked { get; set; }

        [JsonPropertyName("selectedOption")]
        public required string SelectedOption { get; set; }
    }
}
