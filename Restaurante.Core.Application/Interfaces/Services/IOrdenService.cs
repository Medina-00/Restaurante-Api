

using Restaurante.Core.Application.ViewModels.Order;
using Restaurante.Core.Domain.Entities;

namespace Restaurante.Core.Application.Interfaces.Services
{
    public interface IOrdenService : IGenericService<SaveOrdenViewModel ,UpdateOrdenViewModel, OrdenViewModel , Orden>
    {
    }
}
