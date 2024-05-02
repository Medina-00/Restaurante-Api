

using Restaurante.Core.Application.Interfaces.Repository;
using Restaurante.Core.Domain.Entities;
using Restaurante.Infrastrecture.Persistence.Context;

namespace Restaurante.Infrastrecture.Persistence.Repository
{
    public class IngredienteRepository : GenericRepository<Ingrediente> , IIngredienteRepository
    {
        public IngredienteRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
            
        }
    }
}
