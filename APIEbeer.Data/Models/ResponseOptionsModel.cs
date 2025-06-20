namespace APIEbeer.Data.Models
{
    public class ResponseOptionsModel
    {
        public List<string> OptionsBitter { get; set; } = new List<string>
        {
            "Leve", "Moderado", "Amargo", "Intenso"
        };
        
        public List<string> OptionsSweet { get; set; } = new List<string>
        {
            "Baixo", "Equilibrado", "Alto"
        };

        public List<string> OptionsAlcoholContent { get; set; } = new List<string>
        {
            "Baixo", "Moderado", "Alto", "Muito alto"
        };

        public List<string> OptionsFlavor { get; set; } = new List<string>
        {
            "Adocicado", "Cítrico", "Torrado", "Grãos", "Salgado", "Herbal", "Defumado"
        };

        public List<string> OptionsTexture { get; set; } = new List<string>
        {
            "Leve", "Cremoso", "Seco", "Encorpado", "Aveludado", "Aromático"
        };

        public List<string> OptionsColor { get; set; } = new List<string>
        {
            "Clara", "Dourada", "Âmbar", "Cobre", "Marrom", "Preta"
        };

        public List<string> OptionsCarbonation { get; set; } = new List<string>
        {
            "Baixa", "Média", "Alta"
        };

        public List<string> OptionsPointOfMeat { get; set; } = new List<string>
        {
            "Mal passado", "Ao ponto", "Bem passado"
        };

        public List<string> OptionsTemperature { get; set; } = new List<string>
        {
            "Frio", "Morno", "Quente"
        };

        public List<string> OptionsSeasoning { get; set; } = new List<string>
        {
            "Sem tempero", "Leve", "Moderado", "Intenso"
        };

        public List<string> OptionsAcidity { get; set; } = new List<string>
        {
            "Baixa", "Moderada", "Alta"
        };

        public List<string> OptionsSauceAndNoSauce { get; set; } = new List<string>
        {
            "Com molho", "Sem molho"
        };

        public List<string> OptionsSpicy { get; set; } = new List<string>
        {
            "Sem ardência", "Leve", "Moderada", "Alta"
        };

        public List<string> OptionsSize { get; set; } = new List<string>
        {
            "Pequeno", "Médio", "Grande"
        };

        public List<string> OptionsVolume { get; set; } = new List<string>
        {
            "269ml", "350ml", "355ml", "473ml", "600ml", "1L"
        };

        public List<string> GetOptionsByCharacteristic(string characteristic)
        {
            var propertyName = $"Options{characteristic}";

            return GetType().GetProperty(propertyName)?.GetValue(this) as List<string> ?? [];
        }
    }
}
