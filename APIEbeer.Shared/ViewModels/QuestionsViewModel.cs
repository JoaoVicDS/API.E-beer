namespace APIEbeer.Shared.ViewModels
{
    public class QuestionsViewModel
    {
        public required string QuestionText { get; set; }
        public required List<string> Options { get; set; }
    }
}
