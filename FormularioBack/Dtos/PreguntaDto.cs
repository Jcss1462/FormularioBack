namespace FormularioBack.Dtos
{
    public class PreguntaDto
    {
        public string Texto { get; set; } = null!;
        public List<OpcionDto> Opciones { get; set; } = new();
    }
}
