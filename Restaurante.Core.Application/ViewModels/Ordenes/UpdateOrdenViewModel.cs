


using System.ComponentModel.DataAnnotations;

namespace Restaurante.Core.Application.ViewModels.Order
{
    public class UpdateOrdenViewModel
    {
        [Required(ErrorMessage = "Ingrese el id de la orden")]
        public int IdOrden { get; set; }
        [Required(ErrorMessage = "Ingrese el total de plato de la orden")]
        [DataType(DataType.Text)]
        public string? ListaPlato { get; set; }
        
    }
}
