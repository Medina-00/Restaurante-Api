

using Restaurante.Core.Application.ViewModels.Platos;
using Restaurante.Core.Domain.Entities;

namespace Restaurante.Core.Application.Interfaces.Services
{
    public interface IPlatoService : IGenericService<SavePlatoViewModel, UpdatePlatoViewModel, PlatoViewModel, Plato>

    {
    }
}
