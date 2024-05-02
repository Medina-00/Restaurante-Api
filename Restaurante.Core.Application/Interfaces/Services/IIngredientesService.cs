
using Restaurante.Core.Application.ViewModels.Ingredientes;
using Restaurante.Core.Domain.Entities;

namespace Restaurante.Core.Application.Interfaces.Services
{
    public interface IIngredientesService : IGenericService<SaveIngredienteViewModel ,UpdateIngredienteViewModel, IngredienteViewModel , Ingrediente>
    {
    }
}
