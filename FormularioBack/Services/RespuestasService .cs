using FormularioBack.Context;
using FormularioBack.Dtos;
using FormularioBack.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace FormularioBack.Services
{
    public interface IRespuestasService
    {
        Task<int> GuardarRespuestasAsync(EnviarRespuestasFormularioDto dto);
        Task<List<FormularioResumenDto>> ObtenerResumenFormulariosAsync();

        Task<List<ObtenerResultadosDto>> ObtenerResultadosPorFormulario(int idFormulario);

        Task<DetalleRespuestaDto?> ObtenerDetalleRespuesta(int idRespuesta);
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

        public async Task<List<FormularioResumenDto>> ObtenerResumenFormulariosAsync()
        {
            // Traigo solo lo necesario desde EF
            var formulariosDb = await _context.Formularios
                .Include(f => f.FormularioHasPregunta)
                .Include(f => f.Respuesta)
                    .ThenInclude(r => r.RespuestasPregunta)
                        .ThenInclude(rp => rp.IdPreguntaNavigation)
                            .ThenInclude(p => p.Opciones)
                .ToListAsync();


            // Ahora calculo en memoria
            var formularios = formulariosDb.Select(f => new FormularioResumenDto
            {
                IdFormulario = f.IdFormulario,
                Nombre = f.Nombre,
                CantidadRespuestas = f.Respuesta.Count,
                PuntajePromedio = f.Respuesta.Any()
                    ? f.Respuesta
                        .Select(r =>
                        {
                            //En caso de que no halla preguntas retorno 0, para evtar division por 0
                            if (r.IdFormularioNavigation.FormularioHasPregunta.Count == 0)
                                return 0;

                            var correctas = r.RespuestasPregunta.Count(rp =>
                                rp.IdOpcionSeleccionada != null &&
                                rp.IdPreguntaNavigation.Opciones.Any(o => o.Correcta && o.IdOpcion == rp.IdOpcionSeleccionada)
                            );

                            return (double)correctas / r.IdFormularioNavigation.FormularioHasPregunta.Count * 100;
                        })
                        .Average()
                    : 0
            }).ToList();

            return formularios;
        }

        public async Task<List<ObtenerResultadosDto>> ObtenerResultadosPorFormulario(int idFormulario)
        {

            List<ObtenerResultadosDto> resultados = await _context.Respuestas
                .Where(r => r.IdFormulario == idFormulario)
                .Select(r => new ObtenerResultadosDto
                {
                    IdResultado = r.IdRespuesta,
                    CatidadRespuestasCorrectas = r.RespuestasPregunta.Count(rp =>
                        rp.IdOpcionSeleccionadaNavigation != null &&
                       rp.IdOpcionSeleccionadaNavigation.Correcta
                    ),
                    CatidadPreguntas = r.IdFormularioNavigation.FormularioHasPregunta.Count
                })
                .ToListAsync();

            return resultados;

        }

        public async Task<DetalleRespuestaDto?> ObtenerDetalleRespuesta(int idRespuesta)
        {
            var respuesta = await _context.Respuestas
            // Incluimos el formulario asociado a la respuesta
            .Include(r => r.IdFormularioNavigation)
                .ThenInclude(f => f.FormularioHasPregunta)
                    .ThenInclude(fhp => fhp.IdPreguntaNavigation)
                        .ThenInclude(p => p.Opciones)

            // Incluimos las preguntas y opciones que el usuario contestó
            .Include(r => r.RespuestasPregunta)
                .ThenInclude(rp => rp.IdPreguntaNavigation)
                    .ThenInclude(p => p.Opciones)

            .FirstOrDefaultAsync(r => r.IdRespuesta == idRespuesta);

            if (respuesta == null) return null;

            var detalleRespuesta = new DetalleRespuestaDto
            {
                IdRespuesta = respuesta.IdRespuesta,
                IdFormulario = respuesta.IdFormulario,
                NombreFormulario = respuesta.IdFormularioNavigation.Nombre,
                FechaRespuesta = respuesta.FechaRespuesta,
                Preguntas = respuesta.IdFormularioNavigation.FormularioHasPregunta
                .Select(fhp =>
                {
                    Pregunta pregunta = fhp.IdPreguntaNavigation;

                    // Buscar si el usuario respondió esta pregunta
                    RespuestasPregunta? respuestaPregunta = respuesta.RespuestasPregunta
                        .FirstOrDefault(rp => rp.IdPregunta == fhp.IdPregunta);

                    return new DetallePreguntaDto
                    {
                        IdPregunta = pregunta.IdPregunta,
                        Texto = pregunta.Pregunta1,
                        IdOpcionSeleccionada = respuestaPregunta?.IdOpcionSeleccionada,
                        EsCorrecta = respuestaPregunta?.IdOpcionSeleccionada != null
                            ? pregunta.Opciones.Any(o => o.IdOpcion == respuestaPregunta.IdOpcionSeleccionada && o.Correcta)
                            : false, // false si no contestó
                        Opciones = pregunta.Opciones.Select(o => new DetalleOpcionDto
                        {
                            IdOpcion = o.IdOpcion,
                            Texto = o.Texto,
                            Correcta = o.Correcta
                        }).ToList()
                    };
                })
                .ToList()
            };

            return detalleRespuesta;
        }

    }
}
