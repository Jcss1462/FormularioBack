namespace FormularioBack.Dtos
{
    public class ResultadosResumenDto
    {
        public int IdFormulario { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public List<ObtenerResultadosDto> Resultados { get; set; } = new();
    }
}
