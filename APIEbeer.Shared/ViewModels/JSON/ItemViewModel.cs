using System.Text.Json.Serialization;

namespace APIEbeer.Shared.ViewModels.JSON
{
    public class ItemViewModel
    {
        [JsonPropertyName("name")]
        public required string Name { get; set; }

        [JsonPropertyName("characteristics")]
        public required Dictionary<string, string> Characteristics { get; set; }
    }
}