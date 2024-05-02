using Restaurante.Core.Application.Interfaces.Repository;
using Restaurante.Core.Application.ViewModels.Mesas;
using Restaurante.Core.Domain.Entities;
using Restaurante.Infrastrecture.Persistence.Context;
using System;

namespace Restaurante.Infrastrecture.Persistence.Repository
{
    public class MesaRepository : GenericRepository<Mesa> , IMesaRepository
    {
        private readonly ApplicationContext applicationContext;

        public MesaRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public async Task ChangeStatus(ChangeStatus changeStatus , int id)
        {
            var entity = await applicationContext.Set<Mesa>().FindAsync(id);
            if (entity != null)
            {
                entity.EstadoMesa = changeStatus.EstadoMesa;
                await applicationContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("No se encontró la mesa con el ID proporcionado.");
            }

        }

        public override async Task UpdateAsync(Mesa mesa, int id)
        {
            var entity = await applicationContext.Set<Mesa>().FindAsync(id);
            mesa.EstadoMesa = entity!.EstadoMesa;
            applicationContext.Entry(entity).CurrentValues.SetValues(mesa!);
            await applicationContext.SaveChangesAsync();

        }
    }
}
