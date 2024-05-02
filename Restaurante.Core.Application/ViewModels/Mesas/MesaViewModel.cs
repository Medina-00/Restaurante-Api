

using Restaurante.Core.Application.ViewModels.Order;
using Restaurante.Core.Domain.Entities;

namespace Restaurante.Core.Application.ViewModels.Mesas
{
    public class MesaViewModel
    {
        public int IdMesa { get; set; }
        public int CantidadPersona { get; set; }
        public string? Descripcion { get; set; }
        public string? EstadoMesa { get; set; }

    }
}
