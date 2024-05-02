using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Core.Application.Interfaces.Services;
using Restaurante.Core.Application.ViewModels.Order;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace Restaurante.Api.Controllers
{
    [Authorize(Roles = "Mesero")]
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de Orden")]

    public class OrdenController : BaseApiController
    {
        private readonly IOrdenService ordenService;

        public OrdenController(IOrdenService ordenService)
        {
            this.ordenService = ordenService;
        }

        [HttpGet]
        [SwaggerOperation(
          Summary = "Listado de Las Ordenes",
          Description = "Obtiene todas las Ordenes creadas"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        //esto es para indicar la respuesta por el resultado
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrdenViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult> Get()
        {
            var data = await ordenService.GetAllViewModel();

            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
          Summary = "Orden Por Id",
          Description = "Obtiene la Orden Por El Id"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        //esto es para indicar la respuesta por el resultado
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveOrdenViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Get(int id)
        {
            var data = await ordenService.GetByIdSaveViewModel(id);

            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        [SwaggerOperation(
          Summary = "Crear Orden",
          Description = "Recibe Los Parementro que se Necesita para Crear Una Orden"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(SaveOrdenViewModel saveOrden)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                await ordenService.Add(saveOrden);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut("{id}")]
        [SwaggerOperation(
              Summary = "Editar una Orden",
              Description = "Recibe los parametros necesarios para eliminar una Orden existente. "
                     )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateOrdenViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, UpdateOrdenViewModel saveOrden)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                await ordenService.Update(saveOrden, id);
                return Ok(saveOrden);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
              Summary = "Eliminar una Orden",
              Description = "Recibe los parametros necesarios para eliminar una Orden existente. "
                     )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await ordenService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
