using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Core.Application.Interfaces.Services;
using Restaurante.Core.Application.Services;
using Restaurante.Core.Application.ViewModels.Mesas;
using Restaurante.Core.Application.ViewModels.Order;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace Restaurante.Api.Controllers.v1
{
    [Authorize(Roles = "Adminitrador,Mesero")]
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de Mesas")]

    public class MesaController : BaseApiController
    {
        private readonly IMesaService mesaService;
        private readonly IOrdenService ordenService;

        public MesaController(IMesaService mesaService, IOrdenService ordenService)
        {
            this.mesaService = mesaService;
            this.ordenService = ordenService;
        }
        [HttpGet]
        [SwaggerOperation(
          Summary = "Listado de Las Mesas",
          Description = "Obtiene todas las Mesa creadas"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        //esto es para indicar la respuesta por el resultado
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MesaViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Adminitrador,Mesero")]

        public async Task<ActionResult> Get()
        {
            var data = await mesaService.GetAllViewModel();

            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [Authorize(Roles = "Adminitrador,Mesero ")]
        [HttpGet("{id}")]
        [SwaggerOperation(
          Summary = "Mesa Por Id",
          Description = "Obtiene la Mesa Por El Id"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        //esto es para indicar la respuesta por el resultado
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveMesaViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Get(int id)
        {
            var data = await mesaService.GetByIdSaveViewModel(id);

            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [Authorize(Roles = "Adminitrador")]
        [HttpPost]
        [SwaggerOperation(
          Summary = "Crear Mesa",
          Description = "Recibe Los Parementro que se Necesita para Crear Una Mesa"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(SaveMesaViewModel saveMesa)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                await mesaService.Add(saveMesa);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Adminitrador")]
        [HttpPut("{id}")]
        [SwaggerOperation(
              Summary = "Editar una Mesa",
              Description = "Recibe los parametros necesarios para eliminar una Mesa existente. "
                     )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateMesaViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, UpdateMesaViewModel saveMesa)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                await mesaService.Update(saveMesa, id);
                return Ok(saveMesa);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        //EndPoint de Obtener las ordenes por el id de la mesa.
        [Authorize(Roles = "Mesero")]
        [HttpGet("Ordenes/{id}")]
        [SwaggerOperation(
          Summary = "Ordenes Por Id de Mesa",
          Description = "Obtiene las Ordenes Por Id de Mesa"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveOrdenViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetTable(int id)
        {
            var data = await ordenService.GetAllViewModel();
            if (data == null)
            {
                return NotFound();
            }
            data = data.Where(d => d.IdMesa == id).ToList();
            return Ok(data);
        }

        [Authorize(Roles = "Mesero")]
        [HttpPut("{id}/changestatus")]
        [SwaggerOperation(
          Summary = "Cambiar Estado de Mesa",
          Description = "Permite Cambiar el Estado a una mesa"
        )]

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ChangeStatus))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ChangeStatus(int id, [FromBody] ChangeStatus status)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                await mesaService.ChangeStatus(status, id);
                return Ok(status);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



    }
}
