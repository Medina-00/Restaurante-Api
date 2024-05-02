

using Microsoft.AspNetCore.Identity;
using Restaurante.Core.Application.Enums;
using Restaurante.Infrastrecture.Identity.Entities;

namespace Restaurante.Infrastrecture.Identity.Seeds
{
    public static class UserMesero
    {
        public static async Task SeedsAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser mesero = new();

            mesero.Nombre = "Pablo";
            mesero.Apellido = "Perez";
            mesero.UserName = "Mesero01";
            mesero.Email = "Mesero01@gamil.com";
            mesero.EmailConfirmed = true;
            mesero.PhoneNumber = "8098765432";
            mesero.PhoneNumberConfirmed = true;

            if (userManager.Users.All(u => u.Id != mesero.Id))
            {
                var user = await userManager.FindByEmailAsync(mesero.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(mesero, "HolaMundo12*");
                    await userManager.AddToRoleAsync(mesero, Roles.Mesero.ToString());
                }
            }



        }
    }
}
