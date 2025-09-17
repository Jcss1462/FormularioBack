using FormularioBack.Context;
using FormularioBack.Dtos;
using FormularioBack.Models;

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
    
    
    }
}
