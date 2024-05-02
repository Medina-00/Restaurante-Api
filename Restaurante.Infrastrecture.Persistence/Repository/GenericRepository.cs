
using Microsoft.EntityFrameworkCore;
using Restaurante.Core.Application.Interfaces;
using Restaurante.Infrastrecture.Persistence.Context;

namespace Restaurante.Infrastrecture.Persistence.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationContext appContext;

        public GenericRepository(ApplicationContext appContext)
        {
            this.appContext = appContext;
        }
        public virtual async Task<T> AddAsync(T t)
        {
            await appContext.Set<T>().AddAsync(t);
            await appContext.SaveChangesAsync();
            return t;
        }

        public virtual async Task UpdateAsync(T t, int id)
        {
            var entity = await appContext.Set<T>().FindAsync(id);
            appContext.Entry(entity).CurrentValues.SetValues(t!);
            await appContext.SaveChangesAsync();

        }

        public virtual async Task DeleteAsync(T t)
        {
            appContext.Set<T>().Remove(t);
            await appContext.SaveChangesAsync();
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await appContext.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await appContext.Set<T>().FindAsync(id);
        }


    }

}

