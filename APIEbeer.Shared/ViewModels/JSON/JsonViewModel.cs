using System.Text.Json.Serialization;

namespace APIEbeer.Shared.ViewModels.JSON
{
    public class JsonViewModel
    {
        [JsonPropertyName("restaurant")]
        public required string Restaurant { get; set; }

        [JsonPropertyName("menu")]
        public required MenuViewModel Menu { get; set; }
    }
}

// Example JSON structure that this model represents:
//{
//    "restaurant": "E-beer",
//    "menu": 
//    {
//        "categories": [
//        {
//            "name": "Bebidas",
//            "items": [
//            {
//                "name": "Cerveja",
//                "characteristics": {
//                    "bitter": "Moderado",
//                    "alcoholContent": "Moderado"
//                }
//            },
//            {
//                "name": "Refrigerante",
//                "characteristics": {
//                    "flavor": "Adocicado",
//                    "volume": "350ml"
//                }
//            }
//        ]
//        },
//        {
//            "name": "Entradas",
//            "items": [
//            {
//                "name": "Picanha",
//                "characteristics": {
//                    "texture": "Leve",
//                    "pointOfMeat": "Ao ponto"
//                    }
//            },
//            {
//                "name": "Batata Frita",
//                "characteristics": {
//                    "temperature": "Quente",
//                    "size": "Médio"
//                }
//            }
//        ]
//      }
//    ]
//  }
//}

//Example future JSON structure that this model represents:
//{
//    "restaurant": "E-beer",
//    "menu": 
//    {
//        "categories": [
//        {
//            "name": "Bebidas",
//            "items": [
//            {
//                "name": "Cerveja",
//                "characteristics": {
//                    "bitter": {
//                          "value": "Moderado",
//                          "options": ["Leve", "Moderado", "Amargo", "Intenso"]
//                    },
//                    "alcoholContent": {
//                          "value": "Moderado",
//                          "options": ["Baixo", "Moderado", "Alto", "Muito alto"]
//                    }
//                }
//            },
//            {
//                "name": "Refrigerante",
//                "characteristics": {
//                    "flavor": "Adocicado",
//                    "volume": "350ml"
//                }
//            }
//        ]
//        }
//      }
//    ]
//  }
//}

