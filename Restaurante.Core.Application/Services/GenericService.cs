

using AutoMapper;
using Restaurante.Core.Application.Interfaces;
using Restaurante.Core.Application.Interfaces.Services;

namespace Restaurante.Core.Application.Services
{
    public class GenericService<SaveViewModel , UpdateViewModel, ViewModel, Model> : IGenericService<SaveViewModel,UpdateViewModel,ViewModel,Model>
        where SaveViewModel : class
        where ViewModel : class
        where Model : class
        where UpdateViewModel : class

    {
        private readonly IGenericRepository<Model> genericRepository;
        private readonly IMapper mapper;

        public GenericService(IGenericRepository<Model> genericRepository , IMapper mapper)
        {
            this.genericRepository = genericRepository;
            this.mapper = mapper;
        }

        public virtual async Task<SaveViewModel> Add(SaveViewModel vm)
        {
            Model model = mapper.Map<Model>(vm);
            model = await genericRepository.AddAsync(model);

            SaveViewModel saveViewModel = mapper.Map<SaveViewModel>(model);

            return saveViewModel;
        }

        public virtual async Task Delete(int id)
        {
            Model model = await genericRepository.GetByIdAsync(id);

            await genericRepository.DeleteAsync(model);
        }

        public virtual async Task<List<ViewModel>> GetAllViewModel()
        {
            var Models = await genericRepository.GetAllAsync();

            return mapper.Map<List<ViewModel>>(Models);
        }

        public virtual async Task<SaveViewModel> GetByIdSaveViewModel(int id)
        {
            Model model = await genericRepository.GetByIdAsync(id);

            SaveViewModel saveView = mapper.Map<SaveViewModel>(model);

            return saveView;
        }

        public virtual async Task Update(UpdateViewModel vm, int id)
        {
            Model model = mapper.Map<Model>(vm);

            await genericRepository.UpdateAsync(model, id);
        }

    }
}
