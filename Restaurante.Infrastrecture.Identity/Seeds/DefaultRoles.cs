
using Microsoft.AspNetCore.Identity;
using Restaurante.Core.Application.Enums;
using Restaurante.Infrastrecture.Identity.Entities;
using System.Diagnostics.Metrics;

namespace Restaurante.Infrastrecture.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedsAsync(UserManager<ApplicationUser> userManager , RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Adminitrador.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Mesero.ToString()));

        }
    }
}
