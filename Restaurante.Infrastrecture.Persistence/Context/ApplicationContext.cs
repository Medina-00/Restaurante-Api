using Microsoft.EntityFrameworkCore;
using Restaurante.Core.Domain.Entities;


namespace Restaurante.Infrastrecture.Persistence.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Plato> Platos { get; set; }
        public DbSet<Ingrediente> Ingredientes { get; set; }

        public DbSet<Orden> Ordenes { get; set; }

        public DbSet<Mesa> Mesas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            #region TABLES
            modelBuilder.Entity<Plato>().ToTable(nameof(Platos));
            modelBuilder.Entity<Ingrediente>().ToTable(nameof(Ingredientes));
            modelBuilder.Entity<Orden>().ToTable(nameof(Ordenes));
            modelBuilder.Entity<Mesa>().ToTable(nameof(Mesas));

            #endregion

            #region PRIMARY KEYS
            modelBuilder.Entity<Plato>().HasKey(p => p.IdPlato);
            modelBuilder.Entity<Ingrediente>().HasKey(i => i.IdIngrediente);
            modelBuilder.Entity<Orden>().HasKey(o => o.IdOrden);
            modelBuilder.Entity<Mesa>().HasKey(m => m.IdMesa);

            #endregion


            #region RELACIONES
            modelBuilder.Entity<Orden>()
                .HasOne( m => m.Mesa)
                .WithMany(o => o.Ordenes)
                .HasForeignKey( m => m.IdMesa)
                .OnDelete(DeleteBehavior.Cascade);

           
            modelBuilder.Entity<Orden>()
                .HasMany(o => o.Platos)
                .WithMany(p => p.Ordenes)
                .UsingEntity(j => j.ToTable("OrdenPlato"));


            modelBuilder.Entity<Plato>()
            .HasMany(p => p.Ingredientes)
            .WithMany(i => i.Platos)
            .UsingEntity(j => j.ToTable("PlatoIngrediente"));

            #endregion

        }
    }
}
