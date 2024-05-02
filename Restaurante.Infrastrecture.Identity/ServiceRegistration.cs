

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Restaurante.Core.Application.Dtos.Response;
using Restaurante.Core.Application.Services;
using Restaurante.Core.Domain.Settings;
using Restaurante.Infrastrecture.Identity.Context;
using Restaurante.Infrastrecture.Identity.Entities;
using Restaurante.Infrastrecture.Identity.Services;
using System.Text;

namespace Restaurante.Infrastrecture.Identity
{
    public static class ServiceRegistration
    {
        public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Contexts
           
           
                services.AddDbContext<IdentityContext>(options =>
                {
                    options.EnableSensitiveDataLogging();
                    options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
                    m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
                });
            
            #endregion

            #region Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();


            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                //Esto es para que el token se guarde.
                options.SaveToken = false;
                //Esto es para indicar Cuales Datos Debe traer un token para aceptarlo en la App.
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    //Validar el token del token que esta activo o en seccion
                    ValidateIssuerSigningKey = true,
                    //Validamos El Emisor del Token 
                    ValidateIssuer = true,
                    //Validamos La Audiencia del Token 
                    ValidateAudience = true,
                    //Validamos La Vida del Token , mejor dicho si no ha expirado
                    ValidateLifetime = true,
                    //Esto es para indicar que si ya vencio el token , que no tenga vida.
                    ClockSkew = TimeSpan.Zero,
                    //Aqui traemos el emisor de los settings
                    ValidIssuer = configuration["JWTSettings:Issuer"],
                    //Aqui traemos el Audiencia de los settings
                    ValidAudience = configuration["JWTSettings:Audience"],
                    //Aqui traemos La llave de los settings
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]!))
                };
                //Esto es para Configurar los eventos dependiendo del resultado del token.
                options.Events = new JwtBearerEvents()
                {
                    //Esto es por si la Authenticacion Falla.
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();
                        c.Response.StatusCode = 500;
                        c.Response.ContentType = "text/plain";
                        return c.Response.WriteAsync(c.Exception.ToString());
                    },
                    //Esto es esta intentando entrar a una seccion , pero el token no esta Authorizado y tampoco es Valido.
                    OnChallenge = c =>
                    {
                        c.HandleResponse();
                        c.Response.StatusCode = 401;
                        c.Response.ContentType = "application/json";
                        //aqui convierto en Json la respuesta.
                        var result = JsonConvert.SerializeObject(new JwtResponse { HasError = true, Error = "El Token No es Valido Y No Estas Authorizado!!" });
                        return c.Response.WriteAsync(result);
                    },
                    //Esto es esta intentando entrar a una seccion , pero el token no esta Authorizado.
                    OnForbidden = c =>
                    {
                        c.Response.StatusCode = 403;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new JwtResponse { HasError = true, Error = "Usted no está autorizada para acceder a esta Seccion" });
                        return c.Response.WriteAsync(result);
                    }
                };

            });

            #endregion

            #region Services
            services.AddTransient<IAccountService, AccountService>();
            #endregion
        }

    }
}
