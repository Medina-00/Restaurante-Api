using Restaurante.Core.Application.ViewModels.Mesas;
using Restaurante.Core.Domain.Entities;


namespace Restaurante.Core.Application.Interfaces.Services
{
    public interface IMesaService : IGenericService<SaveMesaViewModel, UpdateMesaViewModel, MesaViewModel, Mesa>
    {
        Task ChangeStatus(ChangeStatus vm, int id);
    }
}
