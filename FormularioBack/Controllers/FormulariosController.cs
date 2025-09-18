using FormularioBack.Dtos;
using FormularioBack.Services;
using Microsoft.AspNetCore.Mvc;

namespace FormularioBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormulariosController : ControllerBase
    {
        private readonly FormularioService _service;

        public FormulariosController(FormularioService service)
        {
            _service = service;
        }

        [HttpPost("CrearFormulario")]
        public async Task<IActionResult> CrearFormulario([FromBody] CrearFormularioDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Nombre))
                return BadRequest("El formulario debe tener un nombre y al menos una pregunta.");

            var formulario = await _service.CrearFormularioAsync(dto);

            return Ok(new { mensaje = "Formulario creado correctamente", id = formulario.IdFormulario });
        }


        [HttpGet("ObtenerPreguntasDeFormularioById/{formularioId}")]
        public async Task<IActionResult> ObtenerPreguntasDeFormularioById(int formularioId)
        {
            var preguntas = await _service.ObtenerPreguntasDeFormularioById(formularioId);

            return Ok(preguntas);
        }


    }
}
