using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurante.Core.Application.Interfaces;
using Restaurante.Core.Application.Interfaces.Repository;
using Restaurante.Infrastrecture.Persistence.Context;
using Restaurante.Infrastrecture.Persistence.Repository;

namespace Restaurante.Infrastrecture.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Contexts

            services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));

            #endregion

            #region Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IPlatoRepository, PlatoRepository>();
            services.AddTransient<IIngredienteRepository, IngredienteRepository>();
            services.AddTransient<IOrdenRepository, OrdenRepository>();
            services.AddTransient<IMesaRepository, MesaRepository>();


            #endregion

        }

    }
}
