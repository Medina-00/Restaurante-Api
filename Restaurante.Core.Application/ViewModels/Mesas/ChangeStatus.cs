

using System.ComponentModel.DataAnnotations;

namespace Restaurante.Core.Application.ViewModels.Mesas
{
    public class ChangeStatus
    {
        [Required(ErrorMessage = "Ingrese el Estado de la Mesa")]
        [DataType(DataType.Text)]
        public string? EstadoMesa { get; set; }
    }
}
