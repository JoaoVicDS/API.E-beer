using APIEbeer.Shared.ViewModels.JSON;

namespace APIEbeer.Services.Json
{
    public interface IJsonService
    {
        public (bool IsValid, string? ErrorMessage) IsValidJsonStructure(JsonViewModel? input);
    }
}