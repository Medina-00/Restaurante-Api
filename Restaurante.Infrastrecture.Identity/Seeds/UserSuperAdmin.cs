
using Microsoft.AspNetCore.Identity;
using Restaurante.Core.Application.Enums;
using Restaurante.Infrastrecture.Identity.Entities;

namespace Restaurante.Infrastrecture.Identity.Seeds
{
    public static class UserSuperAdmin
    {
        public static async Task SeedsAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser mesero = new();

            mesero.Nombre = "Jose";
            mesero.Apellido = "Carrion";
            mesero.UserName = "SuperAdmin01";
            mesero.Email = "SuperAdmin01@gamil.com";
            mesero.EmailConfirmed = true;
            mesero.PhoneNumber = "8098765478";
            mesero.PhoneNumberConfirmed = true;

            if (userManager.Users.All(u => u.Id != mesero.Id))
            {
                var user = await userManager.FindByEmailAsync(mesero.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(mesero, "HolaMundo12*");
                    await userManager.AddToRoleAsync(mesero, Roles.Mesero.ToString());
                    await userManager.AddToRoleAsync(mesero, Roles.Adminitrador.ToString());
                    await userManager.AddToRoleAsync(mesero, Roles.SuperAdmin.ToString());

                }
            }



        }
    }
}
