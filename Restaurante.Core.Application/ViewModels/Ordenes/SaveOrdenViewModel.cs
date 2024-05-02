

using Restaurante.Core.Application.ViewModels.Mesas;
using Restaurante.Core.Application.ViewModels.Platos;
using Restaurante.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Restaurante.Core.Application.ViewModels.Order
{
    public class SaveOrdenViewModel
    {
        [Required(ErrorMessage = "Ingrese el total de la orden")]
        public decimal Subtotal { get; set; }
        [Required(ErrorMessage = "Ingrese el id de la mesa")]
        public int IdMesa { get; set; }
        [Required(ErrorMessage = "Ingrese el total de plato de la orden")]
        [DataType(DataType.Text)]
        public string? ListaPlato { get; set; }
        
    }
}
