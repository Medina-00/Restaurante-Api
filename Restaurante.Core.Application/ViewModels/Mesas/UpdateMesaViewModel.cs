

using Restaurante.Core.Application.ViewModels.Order;
using Restaurante.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Restaurante.Core.Application.ViewModels.Mesas
{
    public class UpdateMesaViewModel
    {
        public int IdMesa { get; set; }
        [Required(ErrorMessage = "Ingrese la cantidad de persona")]
        public int CantidadPersona { get; set; }
        [Required(ErrorMessage = "Ingrese La descripcion de la mesa")]
        [DataType(DataType.Text)]
        public string? Descripcion { get; set; }
        
    }
}
