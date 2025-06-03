using APIEbeer.Shared.ViewModels;

namespace APIEbeer.Services.Json
{
    public class JsonService : IJsonService
    {
        public (bool IsValid, string? ErrorMessage) ValidateJsonStructure(JsonViewModel? input)
        {
            if (input == null)
                return (false, "Objeto JSON nulo.");

            if (string.IsNullOrWhiteSpace(input.Restaurant))
                return (false, "Campo 'restaurant' é obrigatório.");

            if (input.Menu == null || input.Menu.Count < 0)
                return (false, "Campo 'menu' é obrigatório e não pode ser vazio.");

            foreach (var category in input.Menu)
            {
                if (string.IsNullOrWhiteSpace(category.Category))
                    return (false, "Cada item do menu deve conter a chave 'category'.");

                if (category.Items == null || category.Items.Count < 0)
                    return (false, $"A categoria '{category.Category}' deve conter ao menos um item.");

                foreach (var item in category.Items)
                {
                    if (string.IsNullOrWhiteSpace(item.Name))
                        return (false, $"Cada item deve conter a chave 'nome' na categoria '{category.Category}'.");

                    if (item.Characteristics == null || item.Characteristics.Count == 0)
                        return (false, $"O item '{item.Name}' da categoria '{category.Category}' deve conter a chave 'caracteristicas'.");
                }
            }

            return (true, null); // Estrutura válida
        }
    }
}
