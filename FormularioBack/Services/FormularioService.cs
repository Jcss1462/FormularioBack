using FormularioBack.Context;
using FormularioBack.Dtos;
using FormularioBack.Models;
using Microsoft.EntityFrameworkCore;

namespace FormularioBack.Services
{
    public class FormularioService
    {
        
        private readonly FormularioDbContext _context;

        public FormularioService(FormularioDbContext context)
        {
            _context = context;
        }

        public async Task<Formulario> CrearFormularioAsync(CrearFormularioDto dto)
        {
            // 1. Crear formulario
            var formulario = new Formulario
            {
                Nombre = dto.Nombre
            };

            _context.Formularios.Add(formulario);
            await _context.SaveChangesAsync(); // para que tenga Id

            // 2. Insertar preguntas y opciones
            foreach (var preguntaDto in dto.Preguntas)
            {
                var pregunta = new Pregunta
                {
                    Pregunta1 = preguntaDto.Texto // OJO: EF suele renombrar a Pregunta1
                };

                _context.Preguntas.Add(pregunta);
                await _context.SaveChangesAsync();

                // Relación con formulario
                var fhp = new FormularioHasPregunta
                {
                    IdFormulario = formulario.IdFormulario,
                    IdPregunta = pregunta.IdPregunta
                };
                _context.FormularioHasPreguntas.Add(fhp);

                // Opciones
                foreach (var opcionDto in preguntaDto.Opciones)
                {
                    var opcion = new Opcione
                    {
                        IdPregunta = pregunta.IdPregunta,
                        Texto = opcionDto.Texto,
                        Correcta = opcionDto.Correcta
                    };
                    _context.Opciones.Add(opcion);
                }

                await _context.SaveChangesAsync();
            }

            return formulario;
        }


        public async Task<ObtenerPreguntasFormularioDto> ObtenerPreguntasDeFormularioById(int formularioId)
        {
            ObtenerPreguntasFormularioDto? preguntas = await _context.Preguntas
                .Where(p => p.FormularioHasPregunta.Any(f => f.IdFormulario == formularioId))
                .Select(p => new ObtenerPreguntasFormularioDto
                {
                    IdFormulario = formularioId,
                    IdPregunta = p.IdPregunta,
                    Pregunta = p.Pregunta1,
                    Opciones = p.Opciones.Select(o => new ObtenerOpcionDto
                    {
                        IdOpcion = o.IdOpcion,
                        Texto = o.Texto
                    }).ToList()
                }).FirstOrDefaultAsync();

            if (preguntas == null) {
                throw new Exception("No se encontro el formulario con Id:" + formularioId);
            }

            return preguntas;
        }


    }
}
