namespace APIEbeer.Shared.ViewModels
{
    public class JsonViewModel
    {
        public required string Restaurant { get; set; }
        public required MenuViewModel Menu { get; set; }
    }
}

// Example JSON structure that this model represents:
//{
//  "restaurant": "E-beer",
//  "menu": {
//    "categories": [
//      {
//        "nome": "Bebidas",
//        "items": [ 
//          {
//            "nome": "Cerveja",
//            "caracteristicas": {
//              "amargo": "Moderado",
//              "teor": "5%"
//            }
//          },
//          {
//            "nome": "Refrigerante",
//            "caracteristicas": {
//              "sabor": "Laranja",
//              "tamanho": "350ml"
//            }
//          }
//        ]
//      },
//      {
//        "nome": "Entradas",
//        "items": [
//          {
//            "nome": "Picanha",
//            "caracteristicas": {
//              "textura": "Macia",
//              "ponto": "Ao ponto"
//            }
//          },
//          {
//            "nome": "Batata Frita",
//            "caracteristicas": {
//              "temperatura": "Quente",
//              "quantidade": "Porção média"
//            }
//          }
//        ]
//      }
//    ]
//  }
//}

