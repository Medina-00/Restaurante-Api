

using Restaurante.Core.Application.ViewModels.Platos;
using Restaurante.Core.Domain.Entities;

namespace Restaurante.Core.Application.ViewModels.Order
{
    public class OrdenViewModel
    {
        public int IdOrden { get; set; }
        public decimal Subtotal { get; set; }
        public string? EstadoOrden { get; set; }

        public int IdMesa { get; set; }
        public string? ListaPlato { get; set; }
        
    }
}
