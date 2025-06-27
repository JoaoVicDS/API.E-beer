namespace APIEbeer.Shared.ViewModels.Form
{
    public class FormViewModel
    {
        public required string FormId { get; set; }
        public required List<FormCategoryViewModel> Categories { get; set; }
    }
}
