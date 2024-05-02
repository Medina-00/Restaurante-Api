

namespace Restaurante.Core.Application.Interfaces.Services
{
    public interface IGenericService<SaveViewModel , UpdateViewModel, ViewModel, Model>
           where SaveViewModel : class
           where ViewModel : class
           where Model : class
        where UpdateViewModel : class

    {
        Task Update(UpdateViewModel vm, int id);

        Task<SaveViewModel> Add(SaveViewModel vm);

        Task Delete(int id);

        Task<SaveViewModel> GetByIdSaveViewModel(int id);

        Task<List<ViewModel>> GetAllViewModel();
    }

    
    
}
