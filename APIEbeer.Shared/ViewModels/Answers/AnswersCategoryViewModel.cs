namespace APIEbeer.Shared.ViewModels.Answers
{
    public class AnswersCategoryViewModel
    {
        public required string Name { get; set; }
        public required List<SelectedAnswersViewModel> SelectedAnswers { get; set; } = [];
    }
}
