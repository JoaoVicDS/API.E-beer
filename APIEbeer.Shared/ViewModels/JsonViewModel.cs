namespace APIEbeer.Shared.ViewModels
{
    public class JsonViewModel
    {
        public required string Restaurant { get; set; }
        public required List<MenuViewModel> Menu { get; set; }
    }
}

// Estrutura JSON
// {
//   "restaurant": "E-beer",
//   "menu": [
//     {
//       "category": "Bebidas",
//       "items": [
//         {
//           "nome": "Cerveja",
//           "caracteristicas": {
//             "amargo": "Moderado",
//             "teor": "5%"
//           }
//         }
//       ]
//     },
//     {
//       "category": "Entradas",
//       "items": [
//         {
//           "nome": "Picanha",
//           "caracteristicas": {
//             "textura": "Macia",
//             "ponto": "Ao ponto"
//           }
//         }
//       ]
//     }
//   ]
// }
