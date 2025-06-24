using APIEbeer.Shared.ViewModels.JSON;

namespace APIEbeer.Services.Json
{
    public class JsonService : IJsonService
    {
        public (bool IsValid, string? ErrorMessage) IsValidJsonStructure(JsonViewModel? input)
        {
            if (input == null)
                return (false, "Objeto JSON nulo.");

            if (string.IsNullOrWhiteSpace(input.Restaurant))
                return (false, "Campo 'restaurant' é obrigatório.");

            if (input.Menu == null)
                return (false, "Campo 'menu' é obrigatório e não pode ser vazio.");

            foreach (var category in input.Menu.Categories)
            {
                if (category == null)
                    return (false, "O menu deve conter a chave 'category'.");

                if (category.Items == null || category.Items.Count < 0)
                    return (false, $"A categoria '{category.Name}' deve conter ao menos um item.");

                foreach (var item in category.Items)
                {
                    if (string.IsNullOrWhiteSpace(item.Name))
                        return (false, $"Cada item deve conter a chave 'name' na categoria '{category.Name}'.");

                    if (item.Characteristics == null || item.Characteristics.Count == 0)
                        return (false, $"O item '{item.Name}' da categoria '{category.Name}' deve conter a chave 'characteristics'.");
                }
            }

            return (true, null); // Estrutura válida
        }
    }
}
