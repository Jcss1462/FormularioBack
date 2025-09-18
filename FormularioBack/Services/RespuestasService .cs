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
    }
}
