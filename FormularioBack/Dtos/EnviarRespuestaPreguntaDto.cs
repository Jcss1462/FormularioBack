namespace FormularioBack.Dtos
{
    public class EnviarRespuestaPreguntaDto
    {
        public int IdPregunta { get; set; }
        public int? IdOpcionSeleccionada { get; set; } // null si no respondió
    }

}
