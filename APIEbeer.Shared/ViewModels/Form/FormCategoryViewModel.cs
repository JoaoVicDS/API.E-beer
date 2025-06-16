namespace APIEbeer.Shared.ViewModels.Form
{
    public class FormCategoryViewModel
    {
        public required string Name { get; set; }
        public required List<FormQuestionsViewModel> Questions { get; set; }
    }
}
