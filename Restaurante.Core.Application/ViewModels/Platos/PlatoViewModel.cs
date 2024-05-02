
using Restaurante.Core.Application.ViewModels.Ingredientes;
using Restaurante.Core.Application.ViewModels.Order;
using Restaurante.Core.Domain.Entities;

namespace Restaurante.Core.Application.ViewModels.Platos
{
    public class PlatoViewModel
    {
        public int IdPlato { get; set; }
        public string? Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Personas { get; set; }
        public string? Categoria { get; set; }


        public string? ListaIngrediente { get; set; }



    }
}
