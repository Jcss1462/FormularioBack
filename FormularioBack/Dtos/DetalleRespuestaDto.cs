namespace FormularioBack.Dtos
{
    public class DetalleRespuestaDto
    {
        public int IdRespuesta { get; set; }
        public int IdFormulario { get; set; }
        public string NombreFormulario { get; set; } = string.Empty;
        public DateTime? FechaRespuesta { get; set; }
        public List<DetallePreguntaDto> Preguntas { get; set; } = new();
    }
}
