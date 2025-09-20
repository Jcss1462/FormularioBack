namespace FormularioBack.Dtos
{
    public class DetallePreguntaDto
    {
        public int IdPregunta { get; set; }
        public string Texto { get; set; } = string.Empty;
        public int? IdOpcionSeleccionada { get; set; }
        public bool EsCorrecta { get; set; }
        public List<DetalleOpcionDto> Opciones { get; set; } = new();
    }
}
