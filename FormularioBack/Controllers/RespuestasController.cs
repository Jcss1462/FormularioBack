using FormularioBack.Dtos;
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
    }
}
