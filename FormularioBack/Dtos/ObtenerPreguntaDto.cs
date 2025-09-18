namespace FormularioBack.Dtos
{
    public class ObtenerPreguntaDto
    {
        public int IdPregunta { get; set; }
        public string Pregunta { get; set; } = string.Empty;
        public List<ObtenerOpcionDto> Opciones { get; set; } = new();
    }
}
