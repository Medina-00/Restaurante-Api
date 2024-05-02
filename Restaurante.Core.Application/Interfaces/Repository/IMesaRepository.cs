using Restaurante.Core.Application.ViewModels.Mesas;
using Restaurante.Core.Domain.Entities;


namespace Restaurante.Core.Application.Interfaces.Repository
{
    public interface IMesaRepository : IGenericRepository<Mesa>
    {
        Task ChangeStatus(ChangeStatus changeStatus, int id);
    }
}
