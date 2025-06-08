using System.Text.Json.Serialization;

namespace APIEbeer.Shared.ViewModels
{
    public class MenuViewModel
    {
        [JsonPropertyName("categories")]
        public required List<CategoryViewModel> Categories { get; set; }
    }
}