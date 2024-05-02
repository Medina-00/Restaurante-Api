using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Core.Application.Dtos.Request;
using Restaurante.Core.Application.Services;
using Restaurante.Infrastrecture.Identity.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using System.Security.Claims;

namespace Restaurante.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Mantenimiento de Cuentas(Usarios)")]

    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }
        [HttpPost("Login")]
        [SwaggerOperation(
         Summary = "Login de usuario",
         Description = "Autentica un usuario en el sistema y le retorna un JWT"
     )]
        [Consumes(MediaTypeNames.Application.Json)]

        public async Task<IActionResult> Login(AuthenticationRequest request)
        {
            return Ok(await accountService.AuthenticateAsync(request));
        }

        [Authorize]
        [HttpPost("Regitrar")]
        [SwaggerOperation(
            Summary = "Creacion de usuario basico",
            Description = "Recibe los parametros necesarios para crear un usuario con el rol basico"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> Registrarse(RegisterRequest request)
        {
            // Aquí obtienes el UserName del usuario autenticado
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            //aqui lo paso y hagos las configuraciones de crecion dependiendo en rol de usurio, en el accountService
            return Ok(await accountService.RegisterBasicUserAsync(request , userId!));
        }

    }
}
