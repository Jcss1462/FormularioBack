namespace FormularioBack.Dtos
{
    public class CrearFormularioDto
    {
        public string Nombre { get; set; } = null!;
        public List<PreguntaDto> Preguntas { get; set; } = new();
    }

}
