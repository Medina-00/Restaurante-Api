

namespace Restaurante.Core.Domain.Entities
{
    public class Orden
    {
        public int IdOrden { get; set; }
        public decimal Subtotal { get; set; }
        public string ?EstadoOrden { get; set; }
        public int IdMesa { get; set; }
        public Mesa ?Mesa { get; set; }
        public string? ListaPlato { get; set; }

        public ICollection<Plato>? Platos { get; set; }
    }

}
