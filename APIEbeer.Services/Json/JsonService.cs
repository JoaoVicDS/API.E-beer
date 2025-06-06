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

            foreach (var menu in input.Menu)
            {
                if (string.IsNullOrWhiteSpace(menu.Category))
                    return (false, "Cada item do menu deve conter a chave 'category'.");

                if (menu.Items == null || menu.Items.Count < 0)
                    return (false, $"A categoria '{menu.Category}' deve conter ao menos um item.");

                foreach (var item in menu.Items)
                {
                    if (string.IsNullOrWhiteSpace(item.Name))
                        return (false, $"Cada item deve conter a chave 'nome' na categoria '{menu.Category}'.");

                    if (item.Characteristics == null || item.Characteristics.Count == 0)
                        return (false, $"O item '{item.Name}' da categoria '{menu.Category}' deve conter a chave 'caracteristicas'.");
                }
            }

            return (true, null); // Estrutura válida
        }

        public JsonViewModel? ParseJson(string json)
        {
            try
            {
                // Deserialize the JSON string into a JsonViewModel object
                return System.Text.Json.JsonSerializer.Deserialize<JsonViewModel>(json);
            }
            catch (System.Text.Json.JsonException ex)
            {
                // Handle JSON parsing errors
                throw new ArgumentException("Erro ao analisar o JSON: " + ex.Message, nameof(json));
            }
        }
    }
}
