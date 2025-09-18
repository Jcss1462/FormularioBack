namespace FormularioBack.Dtos
{
    public class EnviarRespuestasFormularioDto
    {
        public int IdFormulario { get; set; }
        public List<EnviarRespuestaPreguntaDto> Respuestas { get; set; } = new();
    }
}
