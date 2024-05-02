
using Microsoft.AspNetCore.Identity;
using Restaurante.Core.Application.Enums;
using Restaurante.Infrastrecture.Identity.Entities;

namespace Restaurante.Infrastrecture.Identity.Seeds
{
    public static class UserAdministrador
    {
        public static async Task SeedsAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser mesero = new();

            mesero.Nombre = "Juan";
            mesero.Apellido = "Castillo";
            mesero.UserName = "Adminitrador01";
            mesero.Email = "Adminitrador01@gamil.com";
            mesero.EmailConfirmed = true;
            mesero.PhoneNumber = "8098765434";
            mesero.PhoneNumberConfirmed = true;

            if (userManager.Users.All(u => u.Id != mesero.Id))
            {
                var user = await userManager.FindByEmailAsync(mesero.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(mesero, "HolaMundo12*");
                    await userManager.AddToRoleAsync(mesero, Roles.Adminitrador.ToString());
                }
            }



        }
    }
}
