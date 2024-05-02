

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurante.Infrastrecture.Identity.Entities;

namespace Restaurante.Infrastrecture.Identity.Context
{
    public class IdentityContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("Identity");
            modelBuilder.Entity<ApplicationUser>(e =>
            {
                e.ToTable(name: "Users");
            });


            modelBuilder.Entity<IdentityRole>(e =>
            {
                e.ToTable(name: "Roles");
            });

            modelBuilder.Entity<IdentityUserRole<String>>(e =>
            {
                e.ToTable(name: "UserRoles");
            });

            modelBuilder.Entity<IdentityUserLogin<String>>(e =>
            {
                e.ToTable(name: "UserLogin");
            });
        }
    }
}
