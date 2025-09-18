using FormularioBack.Dtos;
using FormularioBack.Services;
using Microsoft.AspNetCore.Mvc;

namespace FormularioBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormulariosController : ControllerBase
    {
        private readonly IFormularioService _formularioService;

        public FormulariosController(IFormularioService formularioService)
        {
            _formularioService = formularioService;
        }

        [HttpPost("CrearFormulario")]
        public async Task<IActionResult> CrearFormulario([FromBody] CrearFormularioDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Nombre))
                return BadRequest("El formulario debe tener un nombre y al menos una pregunta.");

            var formulario = await _formularioService.CrearFormularioAsync(dto);

            return Ok(new { mensaje = "Formulario creado correctamente", id = formulario.IdFormulario });
        }


        [HttpGet("ObtenerPreguntasDeFormularioById/{formularioId}")]
        public async Task<IActionResult> ObtenerPreguntasDeFormularioById(int formularioId)
        {
            var formulario = await _formularioService.ObtenerPreguntasDeFormularioById(formularioId);

            return Ok(formulario);
        }


    }
}
