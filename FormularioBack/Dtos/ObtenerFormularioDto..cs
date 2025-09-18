namespace FormularioBack.Dtos
{
    public class ObtenerFormularioDto
    {
        public int IdFormulario { get; set; }
        public string? Nombre { get; set; }
        public List<ObtenerPreguntaDto> Preguntas { get; set; } = new();
    }
}
