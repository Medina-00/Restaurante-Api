

using AutoMapper;
using Restaurante.Core.Application.Interfaces.Repository;
using Restaurante.Core.Application.Interfaces.Services;
using Restaurante.Core.Application.ViewModels.Platos;
using Restaurante.Core.Domain.Entities;

namespace Restaurante.Core.Application.Services
{
    public class PlatoService : GenericService<SavePlatoViewModel,UpdatePlatoViewModel, PlatoViewModel, Plato> , IPlatoService
    {
        public PlatoService(IPlatoRepository platoRepository, IMapper mapper) : base(platoRepository, mapper) { }
        
    }


}
