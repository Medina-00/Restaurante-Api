

using Restaurante.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Restaurante.Core.Application.ViewModels.Ingredientes
{
    public class SaveIngredienteViewModel
    {

        [Required(ErrorMessage = "Ingrese el Nombre del Ingrediente")]
        [DataType(DataType.Text)]
        public string? Nombre { get; set; }
       
    }
}
