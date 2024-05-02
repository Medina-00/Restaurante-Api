
using AutoMapper;
using Restaurante.Core.Application.Interfaces.Repository;
using Restaurante.Core.Application.Interfaces.Services;
using Restaurante.Core.Application.ViewModels.Ingredientes;
using Restaurante.Core.Application.ViewModels.Order;
using Restaurante.Core.Domain.Entities;

namespace Restaurante.Core.Application.Services
{
    public class IngredienteService : GenericService<SaveIngredienteViewModel ,UpdateIngredienteViewModel, IngredienteViewModel ,Ingrediente >, IIngredientesService
    {
        public IngredienteService(IIngredienteRepository ingredienteRepository , IMapper mapper) : base(ingredienteRepository , mapper)
        {
            
        }
    }
}
