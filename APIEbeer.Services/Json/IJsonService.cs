using APIEbeer.Shared.ViewModels;

namespace APIEbeer.Services.Json
{
    public interface IJsonService
    {
        public (bool IsValid, string? ErrorMessage) ValidateJsonStructure(JsonViewModel? input);
    }
}