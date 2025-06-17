namespace APIEbeer.Shared.ViewModels.Answers
{
    public class AnswersViewModel
    {
        public required string FormId { get; set; }
        public required List<AnswersCategoryViewModel> Categories { get; set; } = [];
    }
}
