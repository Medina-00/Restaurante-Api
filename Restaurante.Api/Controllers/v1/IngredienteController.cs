using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Core.Application.Interfaces.Services;
using Restaurante.Core.Application.ViewModels.Ingredientes;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace Restaurante.Api.Controllers.v1
{
    [Authorize(Roles = "Adminitrador")]
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de Ingredientes")]

    public class IngredienteController : BaseApiController
    {
        private readonly IIngredientesService ingredientesService;

        public IngredienteController(IIngredientesService ingredientesService)
        {
            this.ingredientesService = ingredientesService;
        }
        
        [HttpGet]
        [SwaggerOperation(
          Summary = "Listado de Los Ingredientes",
          Description = "Obtiene todos los ingredientes creadas"
        )]
        [Consumes(MediaTypeNames.Application.Json)]

        //esto es para indicar la respuesta por el resultado
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(IngredienteViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult> Get()
        {
            var data = await ingredientesService.GetAllViewModel();

            if(data == null )
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
          Summary = "Ingrediente Por Id",
          Description = "Obtiene el Ingrediente Por El Id"
        )]
        [Consumes(MediaTypeNames.Application.Json)]

        //esto es para indicar la respuesta por el resultado
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveIngredienteViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Get(int id)
        {
            var data = await ingredientesService.GetByIdSaveViewModel(id);

            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        [SwaggerOperation(
          Summary = "Crear Ingrediente",
          Description = "Recibe Los Parementro que se Necesita para Crear Un Ingrediente"
        )]
        [Consumes(MediaTypeNames.Application.Json)]

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(SaveIngredienteViewModel saveIngrediente)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                await ingredientesService.Add(saveIngrediente);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        
        [HttpPut("{id}")]
        [SwaggerOperation(
              Summary = "Editar un ingrediente",
              Description = "Recibe los parametros necesarios para Editar un ingrediente existente. " 
                     )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateIngredienteViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, UpdateIngredienteViewModel saveIngrediente)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                await ingredientesService.Update(saveIngrediente, id);
                return Ok(saveIngrediente);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        

    }
}
