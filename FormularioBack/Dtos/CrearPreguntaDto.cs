namespace FormularioBack.Dtos
{
    public class CrearPreguntaDto
    {
        public string Texto { get; set; } = null!;
        public List<CrearOpcionDto> Opciones { get; set; } = new();
    }
}
