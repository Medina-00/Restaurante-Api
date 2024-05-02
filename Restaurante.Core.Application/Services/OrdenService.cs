

using AutoMapper;
using Restaurante.Core.Application.Enums;
using Restaurante.Core.Application.Interfaces.Repository;
using Restaurante.Core.Application.Interfaces.Services;
using Restaurante.Core.Application.ViewModels.Mesas;
using Restaurante.Core.Application.ViewModels.Order;
using Restaurante.Core.Domain.Entities;

namespace Restaurante.Core.Application.Services
{
    public class OrdenService : GenericService<SaveOrdenViewModel ,UpdateOrdenViewModel, OrdenViewModel, Orden> , IOrdenService
    {
        private readonly IOrdenRepository ordenRepository;
        private readonly IMapper mapper;

        public OrdenService(IOrdenRepository ordenRepository , IMapper mapper) : base(ordenRepository , mapper) 
        {
            this.ordenRepository = ordenRepository;
            this.mapper = mapper;
        }

        public override async Task<SaveOrdenViewModel> Add(SaveOrdenViewModel vm)
        {
            Orden model = mapper.Map<Orden>(vm);
            model.EstadoOrden = EstadoOrden.EnProceso.ToString();
            model = await ordenRepository.AddAsync(model);

            SaveOrdenViewModel saveViewModel = mapper.Map<SaveOrdenViewModel>(model);

            return saveViewModel;
        }
    }
}
