namespace APIEbeer.Shared.ViewModels.Recommendation
{
    public class RecommendationCategoryViewModel
    {
        public required string Name { get; set; }
        public required List<RecommendationItemViewModel> Items { get; set; }
    }
}
