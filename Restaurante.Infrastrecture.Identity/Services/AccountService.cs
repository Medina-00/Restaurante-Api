

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Restaurante.Core.Application.Dtos.Request;
using Restaurante.Core.Application.Dtos.Response;
using Restaurante.Core.Application.Enums;
using Restaurante.Core.Application.Services;
using Restaurante.Core.Domain.Settings;
using Restaurante.Infrastrecture.Identity.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Restaurante.Infrastrecture.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly JWTSettings jWTSettings;

        public AccountService(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            IOptions<JWTSettings> jWTSettings)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.jWTSettings = jWTSettings.Value;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            AuthenticationResponse response = new();

            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No Accounts registered with {request.Email}";
                return response;
            }

            var result = await signInManager.PasswordSignInAsync(user.UserName!, request.Password, true, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Credenciales Incorrectas {request.Email}";
                return response;
            }

            JwtSecurityToken jwtSecurityToken = await GenerarJWToken(user);


            response.Id = user.Id;
            response.Email = user.Email!;
            response.UserName = user.UserName!;

            var rolesList = await userManager.GetRolesAsync(user).ConfigureAwait(false);

            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            var refreshToke = GenerarRefreshToken();
            response.RefreshToken = refreshToke.Token;
            return response;
        }
        public async Task SignOutAsync()
        {
            await signInManager.SignOutAsync();
        }
        public async Task<RegisterResponse> RegisterBasicUserAsync(RegisterRequest request , string userId)
        {
            RegisterResponse response = new();

            var NombreUsuarioExistente = await userManager.FindByNameAsync(request.UserName);
            if (NombreUsuarioExistente != null)
            {
                response.HasError = true;
                response.Error = $"Ya Este Nombre de Usuario Esta En Uso.";
                return response;
            }

            var EmailExistente = await userManager.FindByEmailAsync(request.Email);
            if (EmailExistente != null)
            {
                response.HasError = true;
                response.Error = $"Ya existe Este Email Esta en Uso.";
                return response;
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                Nombre = request.FirstName,
                Apellido = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.Phone,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            

            var result = await userManager.FindByNameAsync(userId);
            var rol = await userManager.GetRolesAsync(result!);
            if (result != null)
            {
                if (rol.Contains("Administrador"))
                {
                    if(request.Rol == "Administrador")
                    {
                        await userManager.CreateAsync(user, request.Password);
                        await userManager.AddToRoleAsync(user, Roles.Adminitrador.ToString());
                    }
                    else if(request.Rol == "Mesero")
                    {
                        await userManager.CreateAsync(user, request.Password);
                        await userManager.AddToRoleAsync(user, Roles.Mesero.ToString());
                    }
                    
                    
                    
                }
                else if (rol.Contains("Mesero") )
                {
                   if (request.Rol == "Mesero")
                   {
                        await userManager.CreateAsync(user, request.Password);
                        await userManager.AddToRoleAsync(user, Roles.Mesero.ToString());
                    }
                    else
                    {
                        response.HasError = true;
                        response.Error = $"Estas Intetentando crear un Administrador y no tienes permiso para esto , o Ingrese bien el Rol de Mesero";
                        return response;
                    }

                }


            }
            else
            {
                response.HasError = true;
                response.Error = $"Ocurrio Un Error Mientra Se Creaba El Usuario.";
                return response;
            }

            return response;
        }

        #region JWT
        private async Task<JwtSecurityToken> GenerarJWToken(ApplicationUser user)
        {
            var userClaims = await userManager.GetClaimsAsync(user);
            var roles = await userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            //AQUI AGREGOS LOS ROLES DEL USUARIO A UNA LISTA
            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }


            //AQUI CREAMOS UN CLAIMS 
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email!),
                new Claim("Id", user.Id)
            }
            // aqui hago union entre los claims creados y los claims que trae el usuario
            .Union(userClaims)
            .Union(roleClaims);

            var LlaveSimetricaSeguridad = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jWTSettings.Key));
            var Credenciales = new SigningCredentials(LlaveSimetricaSeguridad, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: jWTSettings.Issuer,
                audience: jWTSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jWTSettings.DurationInMinutes),
                signingCredentials: Credenciales);

            return jwtSecurityToken;
        }

        private RefreshToken GenerarRefreshToken()
        {
            return new RefreshToken
            {
                //aqui genero el token.
                Token = RandomTokenString(),
                //aqui le doy la vigencia al token.
                Expires = DateTime.UtcNow.AddDays(5),
                Created = DateTime.UtcNow
            };
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var ramdomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(ramdomBytes);

            //aqui convierto y le doy formato al token.
            return BitConverter.ToString(ramdomBytes).Replace("-", "");
        }



        #endregion
    }
}
