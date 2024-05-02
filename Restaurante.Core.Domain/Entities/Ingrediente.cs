

namespace Restaurante.Core.Domain.Entities
{
    public class Ingrediente
    {
        public int IdIngrediente { get; set; }
        public string ?Nombre { get; set; }

        public ICollection<Plato> ?Platos { get; set; }


    }
}
