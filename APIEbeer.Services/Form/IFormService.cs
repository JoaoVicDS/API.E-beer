using APIEbeer.Shared.ViewModels;

namespace APIEbeer.Services.Form
{
    public interface IFormService
    {
        public FormViewModel GenerateDynamicForm(ItemViewModel model);
    }
}
