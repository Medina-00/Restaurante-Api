
namespace Restaurante.Core.Domain.Entities
{
    public class Mesa
    {
        public int IdMesa { get; set; }
        public int CantidadPersona { get; set; }
        public string ?Descripcion { get; set; }
        public string ?EstadoMesa { get; set; }

        public List<Orden> ?Ordenes { get; set; }
    }
}
