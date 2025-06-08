namespace APIEbeer.Shared.ViewModels
{
    public class CategoryViewModel
    {
        public required string Name { get; set; }
        public required List<ItemViewModel> Items { get; set; }
    }
}
