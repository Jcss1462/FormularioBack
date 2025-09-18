namespace FormularioBack.Dtos
{
    public class CrearFormularioDto
    {
        public string Nombre { get; set; } = null!;
        public List<CrearPreguntaDto> Preguntas { get; set; } = new();
    }

}
