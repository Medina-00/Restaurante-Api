using Microsoft.AspNetCore.Mvc;

namespace Restaurante.Api.Controllers
{
   
        [ApiController]
        [Route("api/v{version:apiVersion}/[controller]")]
        public abstract class BaseApiController : ControllerBase
        {

        }

   
}
