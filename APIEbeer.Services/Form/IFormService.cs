using APIEbeer.Shared.ViewModels;

namespace APIEbeer.Services.Form
{
    public interface IFormService
    {
        public List<string> GenerateDynamicForm(ItemViewModel model);
    }
}
