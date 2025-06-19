using System.Text.Json.Serialization;

namespace APIEbeer.Shared.ViewModels.JSON
{
    public class CategoryViewModel
    {
        [JsonPropertyName("name")]
        public required string Name { get; set; }

        [JsonPropertyName("items")]
        public required List<ItemViewModel> Items { get; set; }
    }
}
