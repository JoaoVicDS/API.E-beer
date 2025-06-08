namespace APIEbeer.Shared.ViewModels
{
    public class FormQuestionsViewModel
    {
        public required string QuestionText { get; set; }
        public required List<string> Options { get; set; }
    }
}
