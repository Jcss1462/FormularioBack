using FormularioBack.Context;
using FormularioBack.Dtos;
using FormularioBack.Models;

namespace FormularioBack.Services
{
    public interface IRespuestasService
    {
        Task<int> GuardarRespuestasAsync(EnviarRespuestasFormularioDto dto);
    }


    public class RespuestasService : IRespuestasService
    {
        private readonly FormularioDbContext _context;

        public RespuestasService(FormularioDbContext context)
        {
            _context = context;
        }

        public async Task<int> GuardarRespuestasAsync(EnviarRespuestasFormularioDto dto)
        {
            // 1. Crear respuesta general (encabezado)
            var respuesta = new Respuesta
            {
                IdFormulario = dto.IdFormulario,
                FechaRespuesta = DateTime.Now
            };

            _context.Respuestas.Add(respuesta);
            await _context.SaveChangesAsync();

            // 2. Crear detalle de respuestas
            var detalle = dto.Respuestas.Select(r => new RespuestasPregunta
            {
                IdRespuesta = respuesta.IdRespuesta,
                IdPregunta = r.IdPregunta,
                IdOpcionSeleccionada = r.IdOpcionSeleccionada
            }).ToList();

            _context.RespuestasPreguntas.AddRange(detalle);
            await _context.SaveChangesAsync();

            return respuesta.IdRespuesta; // Devuelvo el id generado
        }
    }
}
