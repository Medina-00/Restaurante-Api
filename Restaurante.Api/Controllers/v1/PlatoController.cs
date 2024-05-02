using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Core.Application.Interfaces.Services;
using Restaurante.Core.Application.ViewModels.Platos;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace Restaurante.Api.Controllers.v1
{
    [Authorize(Roles = "Adminitrador")]
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de Platos")]

    public class PlatoController : BaseApiController
    {
        private readonly IPlatoService platoService;

        public PlatoController(IPlatoService platoService)
        {
            this.platoService = platoService;
        }
        [HttpGet]
        [SwaggerOperation(
          Summary = "Listado de Los Platos",
          Description = "Obtiene todos los Platos creadas"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        //esto es para indicar la respuesta por el resultado
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PlatoViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult> Get()
        {
            var data = await platoService.GetAllViewModel();

            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
          Summary = "Plato Por Id",
          Description = "Obtiene el Plato Por El Id"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        //esto es para indicar la respuesta por el resultado
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SavePlatoViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Get(int id)
        {
            var data = await platoService.GetByIdSaveViewModel(id);

            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        [SwaggerOperation(
          Summary = "Crear Plato",
          Description = "Recibe Los Parementro que se Necesita para Crear Un Plato"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(SavePlatoViewModel savePlato)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                await platoService.Add(savePlato);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
              Summary = "Eliminar un Plato",
              Description = "Recibe los parametros necesarios para eliminar un Plato existente. "
                     )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdatePlatoViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, UpdatePlatoViewModel savePlato)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                await platoService.Update(savePlato, id);
                return Ok(savePlato);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

       
    }
}
