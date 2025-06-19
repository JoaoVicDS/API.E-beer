using APIEbeer.Shared.ViewModels.Answers;
using APIEbeer.Shared.ViewModels.Form;
using APIEbeer.Shared.ViewModels.JSON;

namespace APIEbeer.Services.Form
{
    public interface IFormService
    {
        public FormViewModel GenerateDynamicForm(MenuViewModel model);
        public bool ValidateAnswers(AnswersViewModel answers);
    }
}
