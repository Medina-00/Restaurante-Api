

using Restaurante.Core.Application.ViewModels.Ingredientes;
using System.ComponentModel.DataAnnotations;

namespace Restaurante.Core.Application.ViewModels.Platos
{
    public class SavePlatoViewModel
    {

        [Required(ErrorMessage = "Ingrese el nombre del plato")]
        [DataType(DataType.Text)]
        public string ?Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese el precio del plato")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "Ingrese la Cantidad de personas del plato")]
        [DataType(DataType.Text)]
        public int Personas { get; set; }
        [Required(ErrorMessage = "Ingrese la categoria del plato=")]
        [DataType(DataType.Text)]
        public string ?Categoria { get; set; }
        [Required(ErrorMessage = "Ingrese los ingredinetes del plato")]
        [DataType(DataType.Text)]
        public string? ListaIngrediente { get; set; } 
    }
}
