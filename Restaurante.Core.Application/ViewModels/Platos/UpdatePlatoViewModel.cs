
using Restaurante.Core.Application.ViewModels.Ingredientes;
using Restaurante.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Restaurante.Core.Application.ViewModels.Platos
{
    public class UpdatePlatoViewModel
    {
        public int IdPlato { get; set; }

        public string? ListaIngrediente { get; set; }
    }
}
