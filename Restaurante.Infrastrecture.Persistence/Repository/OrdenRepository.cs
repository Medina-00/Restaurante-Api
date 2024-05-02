

using Restaurante.Core.Application.Interfaces.Repository;
using Restaurante.Core.Domain.Entities;
using Restaurante.Infrastrecture.Persistence.Context;

namespace Restaurante.Infrastrecture.Persistence.Repository
{
    public class OrdenRepository : GenericRepository<Orden> , IOrdenRepository
    {
        private readonly ApplicationContext applicationContext;

        public OrdenRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public override async Task UpdateAsync(Orden t, int id)
        {
            var entity = await applicationContext.Set<Orden>().FindAsync(id);

            if (entity == null)
            {
                throw new Exception("Entity not found");
            }

            entity!.Subtotal = entity.Subtotal;
            entity.EstadoOrden = entity.EstadoOrden;
            entity.IdMesa = entity.IdMesa;
            entity.ListaPlato = t.ListaPlato;

            applicationContext.Update(entity);
            await applicationContext.SaveChangesAsync();

        }
    }
}
