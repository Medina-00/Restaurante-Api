using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurante.Core.Application.Interfaces.Services;
using Restaurante.Core.Application.Services;
using System.Reflection;

namespace Restaurante.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            #region Services
            services.AddTransient(typeof(IGenericService<,,,>), typeof(GenericService<,,,>));
            services.AddTransient<IIngredientesService, IngredienteService>();
            services.AddTransient<IPlatoService, PlatoService>();
            services.AddTransient<IOrdenService, OrdenService>();
            services.AddTransient<IMesaService, MesaService>();
            #endregion
        }

    }
}
