namespace APIEbeer.Shared.ViewModels
{
    public class ItemViewModel
    {
        public required string Name { get; set; }
        public required Dictionary<string, string> Characteristics { get; set; }
    }
}