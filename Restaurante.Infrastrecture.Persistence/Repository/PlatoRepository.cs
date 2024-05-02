

using Restaurante.Core.Application.Interfaces.Repository;
using Restaurante.Core.Application.ViewModels.Platos;
using Restaurante.Core.Domain.Entities;
using Restaurante.Infrastrecture.Persistence.Context;
using System;

namespace Restaurante.Infrastrecture.Persistence.Repository
{
    public class PlatoRepository : GenericRepository<Plato> , IPlatoRepository
    {
        private readonly ApplicationContext applicationContext;

        public PlatoRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public override async Task UpdateAsync(Plato t , int id)
        {
            var entity = await applicationContext.Set<Plato>().FindAsync(id);
            entity!.Precio = entity.Precio;
            entity.Categoria = entity.Categoria;
            entity.Nombre = entity.Nombre;
            entity.Personas = entity.Personas;
            entity.ListaIngrediente = t.ListaIngrediente;
            applicationContext.Update(entity);
            await applicationContext.SaveChangesAsync();

        }
    }
}
