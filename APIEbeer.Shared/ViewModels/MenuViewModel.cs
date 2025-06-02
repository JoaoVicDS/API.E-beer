namespace APIEbeer.Shared.ViewModels
{
    public class MenuViewModel
    {
        public required string Category { get; set; }
        public required List<ItemViewModel> Items { get; set; }
    }
}