

using System.ComponentModel.DataAnnotations;

namespace Restaurante.Core.Application.ViewModels.Ingredientes
{
    public class UpdateIngredienteViewModel
    {
        [Required(ErrorMessage = "Ingrese el Id del Ingrediente")]
        [DataType(DataType.Text)]
        public int IdIngrediente { get; set; }

        [Required(ErrorMessage = "Ingrese el Nombre del Ingrediente")]
        [DataType(DataType.Text)]
        public string? Nombre { get; set; }
    }
}
