using AutoMapper;
using Restaurante.Core.Application.Enums;
using Restaurante.Core.Application.Interfaces;
using Restaurante.Core.Application.Interfaces.Repository;
using Restaurante.Core.Application.Interfaces.Services;
using Restaurante.Core.Application.ViewModels.Mesas;
using Restaurante.Core.Domain.Entities;


namespace Restaurante.Core.Application.Services
{
    public class MesaService : GenericService<SaveMesaViewModel, UpdateMesaViewModel, MesaViewModel, Mesa>, IMesaService

    {
        private readonly IMesaRepository mesaRepository;
        private readonly IMapper mapper;

        public MesaService(IMesaRepository mesaRepository , IMapper mapper) : base(mesaRepository , mapper)
        {
            this.mesaRepository = mesaRepository;
            this.mapper = mapper;
        }

        public override async Task<SaveMesaViewModel> Add(SaveMesaViewModel vm)
        {
            Mesa model = mapper.Map<Mesa>(vm);
            model.EstadoMesa = EstadoMesa.Disponible.ToString();
            model = await mesaRepository.AddAsync(model);

            SaveMesaViewModel saveViewModel = mapper.Map<SaveMesaViewModel>(model);

            return saveViewModel;
        }

        public async Task ChangeStatus(ChangeStatus vm, int id)
        {


            await mesaRepository.ChangeStatus(vm, id);
        }

        
    }
}
