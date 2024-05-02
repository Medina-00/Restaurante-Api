
namespace Restaurante.Core.Domain.Entities
{
    public class Plato
    {
        public int IdPlato { get; set; }
        public string ?Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Personas { get; set; }
        public string?Categoria { get; set; }
        
        public string? ListaIngrediente { get; set; }
    
        public List<Ingrediente> ?Ingredientes { get; set; }

         public List<Orden>? Ordenes { get; set; }



    }
}
