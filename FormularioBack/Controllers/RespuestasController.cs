using FormularioBack.Dtos;
using FormularioBack.Models;
using FormularioBack.Services;
using Microsoft.AspNetCore.Mvc;

namespace FormularioBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RespuestasController : ControllerBase
    {
        private readonly IRespuestasService _respuestasService;
        
        public RespuestasController(IRespuestasService respuestasService)
        {
            _respuestasService = respuestasService;
        }

        [HttpPost("EnviarRespuestas")]
        public async Task<IActionResult> EnviarRespuestas([FromBody] EnviarRespuestasFormularioDto dto)
        {
            if (dto == null || dto.Respuestas.Count == 0)
                return BadRequest(new { mensaje = "Debe enviar al menos una respuesta" });

            var idRespuesta = await _respuestasService.GuardarRespuestasAsync(dto);

            return Ok(new { mensaje = "Respuestas guardadas correctamente", idRespuesta });
        }

        [HttpGet("Resumen")]
        public async Task<IActionResult> ObtenerResumenFormularios()
        {
            var resumen = await _respuestasService.ObtenerResumenFormulariosAsync();
            return Ok(resumen);
        }


        [HttpGet("ObtenerResultadosFormulario/{formularioId}")]
        public async Task<IActionResult> ObtenerResultadosFormulario(int formularioId)
        {
            return Ok(await _respuestasService.ObtenerResultadosPorFormulario(formularioId));
        }

        [HttpGet("ObtenerDetalleRespuesta/{idRespuesta}")]
        public async Task<ActionResult<DetalleRespuestaDto>> ObtenerDetalleRespuesta(int idRespuesta)
        {
            var resultado = await _respuestasService.ObtenerDetalleRespuesta(idRespuesta);
            if (resultado == null)
                return NotFound($"No existe la respuesta con Id {idRespuesta}");

            return Ok(resultado);
        }
    }
}
