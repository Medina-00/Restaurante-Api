
using AutoMapper;
using Restaurante.Core.Application.Dtos.Request;
using Restaurante.Core.Application.ViewModels.Ingredientes;
using Restaurante.Core.Application.ViewModels.Mesas;
using Restaurante.Core.Application.ViewModels.Order;
using Restaurante.Core.Application.ViewModels.Platos;
using Restaurante.Core.Domain.Entities;

namespace Restaurante.Core.Application.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
           

            #region Mesa
            CreateMap<Mesa, MesaViewModel>()
                .ReverseMap()
                .ForMember(x => x.Ordenes, opt => opt.Ignore());

            CreateMap<Mesa, SaveMesaViewModel>()
                .ReverseMap()
                .ForMember(x => x.IdMesa, opt => opt.Ignore())
                .ForMember(x => x.EstadoMesa, opt => opt.Ignore())
                .ForMember(x => x.Ordenes, opt => opt.Ignore());

            CreateMap<Mesa, UpdateMesaViewModel>()
                .ReverseMap()
                .ForMember(x => x.EstadoMesa, opt => opt.Ignore())
                .ForMember(x => x.Ordenes, opt => opt.Ignore());
            #endregion

            #region Ingredinete


            CreateMap<Ingrediente, IngredienteViewModel>()
                .ReverseMap()
                .ForMember(x => x.Platos, opt => opt.Ignore());

            CreateMap<Ingrediente, SaveIngredienteViewModel>()

                .ReverseMap()
                .ForMember(x => x.IdIngrediente, opt => opt.Ignore())
                .ForMember(x => x.Platos, opt => opt.Ignore());

            CreateMap<Ingrediente, UpdateIngredienteViewModel>()
               .ReverseMap()
               .ForMember(x => x.Platos, opt => opt.Ignore());

            #endregion


            CreateMap<Orden, OrdenViewModel>()

               .ReverseMap()
               .ForMember(x => x.Platos, opt => opt.Ignore())
                .ForMember(x => x.Mesa, opt => opt.Ignore());


            CreateMap<Orden, SaveOrdenViewModel>()
               .ReverseMap()
               .ForMember(x => x.EstadoOrden, opt => opt.Ignore())
               .ForMember(x => x.IdOrden, opt => opt.Ignore())
               .ForMember(x => x.Platos, opt => opt.Ignore())
                .ForMember(x => x.Mesa, opt => opt.Ignore());

            CreateMap<Orden, UpdateOrdenViewModel>()

               .ReverseMap()
               .ForMember(x => x.Subtotal, opt => opt.Ignore())
               .ForMember(x => x.IdMesa, opt => opt.Ignore())
               .ForMember(x => x.EstadoOrden, opt => opt.Ignore())
               .ForMember(x => x.Platos, opt => opt.Ignore())
                .ForMember(x => x.Mesa, opt => opt.Ignore());



            CreateMap<Plato, PlatoViewModel>()

               .ReverseMap()
               .ForMember(x => x.Ordenes, opt => opt.Ignore())
                .ForMember(x => x.Ingredientes, opt => opt.Ignore());

            CreateMap<Plato, SavePlatoViewModel>()

               .ReverseMap()
               .ForMember(x => x.IdPlato, opt => opt.Ignore())
               .ForMember(x => x.Ordenes, opt => opt.Ignore())
                .ForMember(x => x.Ingredientes, opt => opt.Ignore());

            CreateMap<Plato, UpdatePlatoViewModel>()

               .ReverseMap()
               .ForMember(x => x.Personas, opt => opt.Ignore())
               .ForMember(x => x.Nombre, opt => opt.Ignore())
               .ForMember(x => x.Precio, opt => opt.Ignore())
               .ForMember(x => x.Categoria, opt => opt.Ignore())
               .ForMember(x => x.Ordenes, opt => opt.Ignore())
                .ForMember(x => x.Ingredientes, opt => opt.Ignore());


            
        }
    }
}
